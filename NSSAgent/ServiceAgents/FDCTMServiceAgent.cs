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
                
                if (this.EndDate.Value <= item.Date || item.Date <= this.StartDate.Value.AddDays(-1)) continue;

                // what will happen if there's nothing above/below or an NaN number? 
                // TODO: Pete says we'll need to use linear interpolation (y = mx + b), to be discussed when he has time

                // if the discharge is equal to a published curve value, use the probability from the curve (80, 90, etc.)
                double? probQ; double? Qs;

                var Q = item.Value;

                var equalQ = PublishedFDC.Where(p => p.Value == Q).FirstOrDefault();
                var equalQs = PublishedFDC.Where(p => p.Value == Q).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).Keys;
                if (equalQs.Count > 0)
                {
                    if (equalQs.Count > 1)
                    {
                        // fix for 0 flows
                        var firstKey = equalQs.First();
                        var lastKey = equalQs.Last();
                        // find the midpoint of the probabilities with 0 flow
                        var middleKey = (firstKey + lastKey) / 2;
                        // find nearest regression value to the midpoint
                        probQ = ExceedanceProbabilities.OrderBy(p => Math.Abs(middleKey - p.Key)).FirstOrDefault().Key;
                    } else
                    {
                        probQ = equalQs.First();
                    }
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

                // if the PROBQ is equal to a probability in the regression equations, use the equation value
                // this includes the fix for zero flows (it is assigned a probability that exists in the regression equations)
                var equalProbQ = ExceedanceProbabilities.Where(p => p.Key == probQ).FirstOrDefault();
                if (equalProbQ.Key != 0)
                {
                    Qs = equalProbQ.Value;
                } else
                {
                    var regUpper = ExceedanceProbabilities.SkipWhile(p => (p.Key * 100) < probQ)?.FirstOrDefault();
                    var regLower = ExceedanceProbabilities.TakeWhile(p => (p.Key * 100) < probQ)?.LastOrDefault();
                    var EXCREGlower = regLower?.Key * 100;
                    var EXCREGupper = regUpper?.Key * 100;
                    var QREGlower = regLower?.Value;
                    var QREGupper = regUpper?.Value;

                    Qs = QREGlower + (probQ - EXCREGupper) / (EXCREGlower - EXCREGupper) * (QREGupper - QREGlower);
                }
                FDCTMExceedanceTimeseries.Add(key, new TimeSeriesObservation(item.Date, Qs));
                key++;

            }//next item
        }

        protected Double getRegressionFlowAtExceedance(Double exceedanceValue)
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