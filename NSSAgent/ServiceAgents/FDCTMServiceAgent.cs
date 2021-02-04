//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WIM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   The service agent is responsible for initiating the service call, 
//              capturing the data that's returned and forwarding the data back to 
//              the requestor.
//
//discussion:   delegated hunting and gathering responsibilities.   
//
//    

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Threading;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WIM.Resources.TimeSeries;
using NSSAgent.Resources;
using WIM.Utilities;
using WIM.Resources;
using WIM.Utilities.Resources;

namespace NSSAgent.ServiceAgents
{
    public class FDCTMServiceAgent : ExtensionServiceAgentBase
    {
        #region "Properties"   
        private IDictionary<object, object> _messages { get; set; }
        private NWISResource _nwisResource { get; set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        #endregion
        #region "Collections & Dictionaries"
        private Dictionary<Double, TimeSeriesObservation> FDCTMExceedanceTimeseries { get; set; }
        public SortedDictionary<Double, Double> ExceedanceProbabilities
        {
            get
            {
                return ((QPPQResult)Result).ExceedanceProbabilities;
            }
        }
        public List<ExtensionParameter> Parameters { get; private set; }
        public bool usePublishedFlows { get; private set; }
        public SortedDictionary<Double, Double> PublishedFDC { get; private set; }
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public FDCTMServiceAgent(Extension qppqExtension, SortedDictionary<Double, Double> ExceedanceProbabilities, NWISResource nwisResource, IDictionary<object, object> messages = null, bool usePublishedFlows = false, SortedDictionary<Double, Double> PublishedFDC = null)
        {

            this._messages = messages != null ? messages : new Dictionary<object, object>();
            _nwisResource = nwisResource;
            this.isInitialized = false;
            this.Parameters = qppqExtension.Parameters;
            Result = new QPPQResult();
            this.usePublishedFlows = usePublishedFlows;
            ((QPPQResult)Result).ExceedanceProbabilities = ExceedanceProbabilities;
            this.PublishedFDC = PublishedFDC;

            this.isInitialized = init();
        }
        #endregion
        #endregion
        #region "Methods"
        public override Boolean init()
        {
            try
            {
                //load refGage flows
                var sid = Parameters.FirstOrDefault(i => String.Equals(i.Code, "SID", StringComparison.OrdinalIgnoreCase));
                var sdate = Parameters.FirstOrDefault(i => String.Equals(i.Code, "sdate", StringComparison.OrdinalIgnoreCase));
                var edate = Parameters.FirstOrDefault(i => String.Equals(i.Code, "edate", StringComparison.OrdinalIgnoreCase));

                if (sid == null || sdate == null || edate == null) throw new Exception("One or more input params are null");
                this.StartDate = Convert.ToDateTime(sdate.Value);
                this.EndDate = Convert.ToDateTime(edate.Value);

                ((QPPQResult)Result).ReferanceGage = Station.NWISStation(sid.Value, _nwisResource);

                if (!((QPPQResult)Result).ReferanceGage.LoadFullRecord()) throw new Exception("Failed to load reference gage ");
                if (this.usePublishedFlows)
                {
                    getFlowsFromPublishedDuration(((QPPQResult)Result).ReferanceGage.Discharge);

                } else
                {
                    transferFlowDuration(((QPPQResult)Result).ReferanceGage.GetExceedanceProbability());
                }

                return true;
            }
            catch (Exception ex)
            {
                sm($"Failed to initialize FDCTMService Agent {ex.Message}", MessageType.error);
                return false;
            }
        }
        public override Boolean Execute()
        {
            try
            {
                if (FDCTMExceedanceTimeseries == null || FDCTMExceedanceTimeseries.Count < 1) throw new Exception("Referance gage or gage discharge is invalid");

                ((QPPQResult)Result).EstimatedFlow = new FlowTimeSeries("Flow Duration Curve Transfer Method Estimates", "Estimates computed using Flow Duration Transfer Method");
                //order results and limit
                var tseries = FDCTMExceedanceTimeseries.OrderBy(ts => ts.Value.Date).Where(ts => ts.Value.Date.Date >= StartDate.Value.Date && ts.Value.Date.Date <= EndDate.Value.Date);

                foreach (var observ in tseries)
                {
                    if (observ.Value.Value.HasValue) ((QPPQResult)Result).EstimatedFlow.Add(observ.Value.Date, observ.Value.Value.Value);
                }//next observ

                ((QPPQResult)Result).ReferanceGage.LimitDischarge(StartDate.Value, EndDate.Value);

                return true;
            }
            catch (Exception ex)
            {
                sm($"Failed to Execute FDCTMService Agent {ex.Message}", MessageType.error);
                return false;
            }
        }
        #endregion
        #region "Helper Methods"
        protected override Boolean IsValid()
        {

            Boolean Isok = false;
            try
            {
                //ensure props are within acceptiable ranges
                Isok = (this.StartDate.HasValue && StartDate.Value.Year > 1900 &&
                        EndDate.HasValue && EndDate.Value.Year > 1900);

                return (Isok);
            }
            catch (Exception)
            {
                //Message = "error validating parameters";
                return false;
            }
        }
        protected void transferFlowDuration(IDictionary<Double, TimeSeriesObservation> FD)
        {
            FDCTMExceedanceTimeseries = new Dictionary<Double, TimeSeriesObservation>();
            //loop through FD, from gage, and extend/sync dates w/ regressional equations
            foreach (var item in FD)
            {
                FDCTMExceedanceTimeseries.Add(item.Key, new TimeSeriesObservation(item.Value.Date, getRegressionFlowAtExceedance(item.Key)));
                // change this to get probability then flow for published
            }//next item 


        }
        protected void getFlowsFromPublishedDuration(FlowTimeSeries TS)
        {
            FDCTMExceedanceTimeseries = new Dictionary<Double, TimeSeriesObservation>();
            var observations = TS.Observations;
            var key = 0;
            foreach (var item in observations)
            {
                
                if (this.EndDate.Value <= item.Date || item.Date <= this.StartDate.Value.AddDays(-1)) continue; // TODO: does this need to be fixed elsewhere instead?

                // if the discharge is equal to a published curve value, use the probability from the curve (80, 90, etc.)
                double? probQ; double? Qs;

                var Q = item.Value;

                var equalQ = PublishedFDC.Where(p => p.Value == Q).FirstOrDefault();
                if (equalQ.Value != null)
                {
                    probQ = equalQ.Value;
                } else
                {
                    var upper = PublishedFDC.TakeWhile(p => Q <= p.Value)?.LastOrDefault();
                    var lower = PublishedFDC.SkipWhile(p => Q < p.Value)?.FirstOrDefault();
                    var EXClower = lower?.Key * 100;
                    var EXCupper = upper?.Key * 100;
                    var Qlower = lower?.Value;
                    var Qupper = upper?.Value;

                    probQ = EXClower + (Q - Qlower) / (Qupper - Qlower) * (EXCupper - EXClower);
                }


                // what will happen if there's nothing above/below or an NaN number?
                // also, we have to do the fix for zero flows


                // if the PROBQ is equal to a probability in the regression equations, use the equation value
                var equalProbQ = ExceedanceProbabilities.Where(p => p.Value == probQ).FirstOrDefault();
                if (equalProbQ.Value != null)
                {
                    Qs = equalProbQ.Value;
                } else
                {
                    var regUpper = ExceedanceProbabilities.TakeWhile(p => Q <= p.Value)?.LastOrDefault();
                    var regLower = ExceedanceProbabilities.SkipWhile(p => Q < p.Value)?.FirstOrDefault();
                    var EXCREGlower = regLower?.Key * 100; // get closest item less than
                    var EXCREGupper = regUpper?.Key * 100; // get closest item greater than
                    var QREGlower = regLower?.Value;
                    var QREGupper = regUpper?.Value;

                    // NA (send as NA or some sort of null)

                    //var Qs = QREGlower + (probQ - QREGlower) / (QREGupper - QREGlower) * (EXCREGupper - EXCREGlower); // this is not computing correctly
                    Qs = QREGlower + (probQ - EXCREGupper) / (EXCREGlower - EXCREGupper) * (QREGupper - QREGlower);
                }
                // =Y3+(T3-V3)/(W3-V3)*(X3-Y3) // from spreadsheet
                // = Qb + (probQ - proba)/(probb - proba) * (Qa - Qb)
                FDCTMExceedanceTimeseries.Add(key, new TimeSeriesObservation(item.Date, Qs));
                key++;

            }//next item
        }

        protected Double getRegressionFlowAtExceedance(Double exceedanceValue)
        //pmm: this assumes that the exceedance value of the daily discharge is known, which when using published flow we will need to solve
        //pmm: elseIf computing flow durations for the index gage
        //pmm: Using ALLDaily(), sort,rank, and compute probabilities using r/(N+1) or the prebuilt function, 
        //pmm: the probabilities computed in the previous function are prob(Q) which can be added to ALLDaily() as a new column
        //pmm: solve the streamflow values for the standarddard exceedance probabilities of 0.01, .02, etc to print in report
        //pmm: loop through the sorted dictionary to find the two values that are on either side of a standard probability. 
        //pmm: so if your standard probabiity is .01 you may have probabilities of .009976 (EXClower) and .0103845 (EXCupper). Each of those will have a discharge value Qlower and Qupper
        //pmm: for computedexcprob(.01) Q(0.1)=Qlower + (.01-EXClower)/(EXCupper-EXClower)*(Qupper-Qlower)
        {
            KeyValuePair<Double, Double> exc1, exc2;
            Double? interpolatedVal;
            Double result = -999;
            try
            {
                //check if val exists in list
                if (ExceedanceProbabilities.ContainsKey(exceedanceValue))
                    return ExceedanceProbabilities[exceedanceValue];

                //check if the value is below the first item
                if (ExceedanceProbabilities.FirstOrDefault().Key > exceedanceValue)
                {
                    //interpolate below exceedance based the  log value
                    exc1 = ExceedanceProbabilities.ElementAt(0);
                    exc2 = ExceedanceProbabilities.ElementAt(1);
                }//end if
                else if (ExceedanceProbabilities.LastOrDefault().Key < exceedanceValue)
                {
                    // past end of exceedance list
                    Int32 numberOfItems = ExceedanceProbabilities.Count() - 1;

                    exc1 = ExceedanceProbabilities.ElementAt(numberOfItems - 1);
                    exc2 = ExceedanceProbabilities.ElementAt(numberOfItems);
                }
                else
                {
                    //traverses the keys using the fact that they are ordered and compares 
                    //all two keys following each other in order
                    var points = ExceedanceProbabilities.Keys.Zip(ExceedanceProbabilities.Keys.Skip(1),
                                  (a, b) => new { a, b })
                                    .Where(x => x.a <= exceedanceValue && x.b >= exceedanceValue)
                                    .FirstOrDefault();

                    exc1 = ExceedanceProbabilities.FirstOrDefault(o => o.Key == points.a);
                    exc2 = ExceedanceProbabilities.FirstOrDefault(o => o.Key == points.b);

                }//end if

                interpolatedVal = MathOps.LinearInterpolate(Math.Log(exc1.Key), Math.Log(exc2.Key), Math.Log(exc1.Value), Math.Log(exc2.Value), Math.Log(exceedanceValue));
                if (interpolatedVal.HasValue)
                {
                    result = Math.Exp(interpolatedVal.Value);
                }
                else
                {
                    //last ditch effort
                    Double lastDitch;
                    // use the smallest or largest value in the set which ever is
                    // closer to valueX
                    if (Math.Abs(ExceedanceProbabilities.FirstOrDefault().Key - exceedanceValue) < Math.Abs(exceedanceValue - ExceedanceProbabilities.LastOrDefault().Key))
                    {
                        lastDitch = ExceedanceProbabilities.FirstOrDefault().Key;
                    }
                    else
                    {
                        lastDitch = ExceedanceProbabilities.LastOrDefault().Key;
                    }//end if

                    result = getRegressionFlowAtExceedance(lastDitch);

                }//end if

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void sm(string msg, MessageType type = MessageType.info)
        {
            sm(new Message() { msg = msg, type = type });
        }
        private void sm(Message msg)
        {
            //wim_msgs comes from WIM.Standard/blob/staging/Services/Middleware/X-Messages.cs
            //manually added to avoid including libr in project.
            if (!this._messages.ContainsKey("wim_msgs"))
                this._messages["wim_msgs"] = new List<Message>();

            ((List<Message>)this._messages["wim_msgs"]).Add(msg);
        }
        #endregion
        #region Structures
        public struct FDCTMStruc
        {
            public List<regionEquation> regressions { get; set; }
        }
        public struct regionEquation
        {
            public Double Probability { get; set; }
            public String Equation { get; set; }
        }

        #endregion
    }//end class

}//end namespace