//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

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

using WiM.Resources.TimeSeries;
using NSSAgent.Resources;
using WiM.Utilities;

namespace NSSAgent.ServiceAgents
{
    public class FDCTMServiceAgent:ExtensionServiceAgentBase
    {
        #region "Properties"       
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
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public FDCTMServiceAgent(Extension qppqExtension, SortedDictionary<Double, Double> ExceedanceProbabilities)
        {
            this.isInitialized = false;
            this.Parameters = qppqExtension.Parameters;
            Result = new QPPQResult();
            ((QPPQResult)Result).ExceedanceProbabilities = ExceedanceProbabilities;

            this.isInitialized = init();
        }
        #endregion
        #endregion
        #region "Methods"
        public override Boolean init() {
            try
            {
                //load refGage flows
                var sid = Parameters.FirstOrDefault(i => String.Equals(i.Code, "SID",StringComparison.OrdinalIgnoreCase));
                var sdate = Parameters.FirstOrDefault(i => String.Equals(i.Code, "sdate", StringComparison.OrdinalIgnoreCase));
                var edate = Parameters.FirstOrDefault(i => String.Equals(i.Code, "edate", StringComparison.OrdinalIgnoreCase));

                if (sid == null || sdate == null || edate == null) { sm(WiM.Resources.MessageType.error, "one or more input params are null"); return false; }
                this.StartDate = Convert.ToDateTime(sdate.Value);
                this.EndDate = Convert.ToDateTime(edate.Value);

                ((QPPQResult)Result).ReferanceGage = Station.NWISStation(sid.Value);
                if (!((QPPQResult)Result).ReferanceGage.LoadFullRecord()) { sm(WiM.Resources.MessageType.error, "Failed to load refrance gage "); return false; }
                transferFlowDuration(((QPPQResult)Result).ReferanceGage.GetExceedanceProbability());
                
                return true;
            }
            catch (Exception ex)
            {
                sm(WiM.Resources.MessageType.error, "Failed to initialize FDCTMService Agent "+ex.Message);
                return false;
            }
        }
        public override Boolean Execute() {
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
                sm(WiM.Resources.MessageType.error, "Failed to Execute FDCTMService Agent " + ex.Message);
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
            //loop threw FD, from gage, and extend/sync dates w/ regressional equations
            foreach (var item in FD)
            {
                FDCTMExceedanceTimeseries.Add(item.Key, new TimeSeriesObservation(item.Value.Date, getRegressionFlowAtExceedance(item.Key)));
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