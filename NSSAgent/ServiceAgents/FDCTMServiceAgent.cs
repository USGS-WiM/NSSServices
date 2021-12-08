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

using MathNet.Numerics.Distributions;

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
        public Tuple<SortedDictionary<Double, Double>, Double> PublishedFDC { get; private set; }
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public FDCTMServiceAgent(Extension qppqExtension, SortedDictionary<Double, Double> ExceedanceProbabilities, NWISResource nwisResource, IDictionary<object, object> messages = null, Tuple<SortedDictionary<Double, Double>,double> PublishedFDC = null)
        {

            this._messages = messages != null ? messages : new Dictionary<object, object>();
            _nwisResource = nwisResource;
            this.isInitialized = false;
            this.Parameters = qppqExtension.Parameters;
            Result = new QPPQResult();
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
                getFlowsFromPublishedDuration(((QPPQResult)Result).ReferanceGage.Discharge);

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
                    else if (observ.Value.Value == null) ((QPPQResult)Result).EstimatedFlow.Add(observ.Value);
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
        protected void getFlowsFromPublishedDuration(FlowTimeSeries TS)
        {

            try
            {
                FDCTMExceedanceTimeseries = new Dictionary<Double, TimeSeriesObservation>();
                var observations = TS.Observations;
                var key = 0;
                foreach (var item in observations)
                {
                    if (this.EndDate.Value <= item.Date || item.Date <= this.StartDate.Value.AddDays(-1)) continue;
                    if (item.Value == null)
                    {
                        // send null if no daily flow measured
                        FDCTMExceedanceTimeseries.Add(key, new TimeSeriesObservation(item.Date, null));
                        key++;
                        continue;
                    }

                    double? probQ; double? Qs;

                    // if flow value is negative, change it to 0
                    var Q = item.Value > 0 ? item.Value : 0;

                    // find any published flow durations where value is equal to Q
                    var equalQs = PublishedFDC.Item1.Where(p => p.Value == Q).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).Keys;
                    Console.WriteLine(equalQs);
                    if (equalQs.Count > 0)
                    {
                        // if there are multiple published FDC values equal to Q, find the midpoint of probabilities
                        // usually this is just for when values are 0
                        if (equalQs.Count > 1)
                        {
                            // if Q = 0
                            if (item.Value == 0)
                            {
                                probQ = PublishedFDC.Item2;
                            } 
                            else
                            {
                                var firstKey = equalQs.First();
                                var lastKey = equalQs.Last();
                                // assign probQ the midpoint of the probabilities
                                probQ = (firstKey + lastKey) / 2 * 100;
                            }
                            
                        }
                        else
                        {
                            // if the discharge is equal to a published curve value, use the probability from the curve (80, 90, etc.)
                            probQ = equalQs.First() * 100;
                        }
                    }
                    else
                    {
                        KeyValuePair<double, double>? upper; KeyValuePair<double, double>? lower;
                        if (PublishedFDC.Item1.LastOrDefault().Value > Q)
                        {
                            // if Q is less than the lowest duration value
                            Int32 numberOfItems = PublishedFDC.Item1.Count() - 1;

                            upper = PublishedFDC.Item1.ElementAt(numberOfItems - 1);
                            lower = PublishedFDC.Item1.ElementAt(numberOfItems);
                        }
                        else if (PublishedFDC.Item1.FirstOrDefault().Value < Q)
                        {
                            // if Q is greater than the highest duration value
                            upper = PublishedFDC.Item1.ElementAt(0);
                            lower = PublishedFDC.Item1.ElementAt(1);
                        }
                        else
                        {
                            //traverses the keys using the fact that they are ordered and compares 
                            //all two keys following each other in order
                            var points = PublishedFDC.Item1.Values.Zip(PublishedFDC.Item1.Values.Skip(1),
                                          (a, b) => new { a, b })
                                            .Where(x => x.a >= Q && x.b <= Q)
                                            .FirstOrDefault();

                            upper = PublishedFDC.Item1.FirstOrDefault(o => o.Value == points.a);
                            lower = PublishedFDC.Item1.FirstOrDefault(o => o.Value == points.b);
                        }
                        // get published FDC items above and below the measured flow
                        var EXClower = Convert.ToDouble(lower?.Key * 100);
                        var EXCupper = Convert.ToDouble(upper?.Key * 100);
                        var Qlower = Convert.ToDouble(lower?.Value);
                        var Qupper = Convert.ToDouble(upper?.Value);
                        if (Qlower == 0) Qlower = 0.001;

                        // compute probability of Q
                        probQ = Normal.InvCDF(0,1,Normal.CDF(0,1,EXCupper/100) - (Normal.CDF(0,1,Convert.ToDouble(Q)) - Normal.CDF(0,1,Qupper)) / (Normal.CDF(0,1,Qlower) - Normal.CDF(0,1,Qupper)) * (Normal.CDF(0,1,EXCupper/100) - Normal.CDF(0,1,EXClower/100))) * 100;
                    }

                    // if the PROBQ is equal to a probability in the regression equations, use the regression value
                    var equalProbQ = ExceedanceProbabilities.Where(p => (p.Key * 100) == probQ).FirstOrDefault();
                    if (equalProbQ.Key != 0)
                    {
                        Qs = equalProbQ.Value;
                    }
                    else
                    {
                        KeyValuePair<double, double>? regUpper; KeyValuePair<double, double>? regLower;
                        if ((ExceedanceProbabilities.LastOrDefault().Key * 100) < probQ)
                        {
                            // if Q is greater than the highest probability
                            Int32 numberOfItems = ExceedanceProbabilities.Count() - 1;

                            regUpper = ExceedanceProbabilities.ElementAt(numberOfItems);
                            regLower = ExceedanceProbabilities.ElementAt(numberOfItems - 1);
                        }
                        else if ((ExceedanceProbabilities.FirstOrDefault().Key * 100) > probQ)
                        {
                            // if Q is less than the lowest probability
                            regUpper = ExceedanceProbabilities.ElementAt(1);
                            regLower = ExceedanceProbabilities.ElementAt(0);
                        }
                        else
                        {
                            //traverses the keys using the fact that they are ordered and compares 
                            //all two keys following each other in order
                            var points = ExceedanceProbabilities.Keys.Zip(ExceedanceProbabilities.Keys.Skip(1),
                                          (a, b) => new { a, b })
                                            .Where(x => (x.a * 100) <= probQ && (x.b * 100) >= probQ)
                                            .FirstOrDefault();

                            regUpper = ExceedanceProbabilities.FirstOrDefault(o => o.Key == points.a);
                            regLower = ExceedanceProbabilities.FirstOrDefault(o => o.Key == points.b);
                        }
                        // get regression probabilities above and below probQ
                        var EXCREGlower = Convert.ToDouble(regLower?.Key * 100);
                        var EXCREGupper = Convert.ToDouble(regUpper?.Key * 100);
                        var QREGlower = Convert.ToDouble(regLower?.Value);
                        var QREGupper = Convert.ToDouble(regUpper?.Value);
                        if (QREGlower == 0) QREGlower = 0.001;

                        // compute estimated flow
                        Qs = Normal.InvCDF(0,1,Normal.CDF(0,1,QREGupper) - (Normal.CDF(0,1,Convert.ToDouble(probQ/100)) - Normal.CDF(0,1,EXCREGupper/100)) / (Normal.CDF(0,1,EXCREGlower/100) - Normal.CDF(0,1,EXCREGupper/100)) * (Normal.CDF(0,1,QREGupper) - Normal.CDF(0,1,QREGlower)));
                    }
                    FDCTMExceedanceTimeseries.Add(key, new TimeSeriesObservation(item.Date, Qs));
                    key++;
                }//next item
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