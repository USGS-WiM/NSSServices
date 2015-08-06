using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WiM.Resources;

using Newtonsoft.Json;
using RegressionService.Utilities.ServiceAgent;
using WiM.Utilities;

using WiM.TimeSeries;

namespace RegressionService.Resources
{
    public class FDCTM : RegressionModel
    {
        #region "Properties"
        [JsonIgnore]
        public Double? BasinArea_sqMi { get; private set; }
        [JsonIgnore]
        public Double? MeanAnnualPrecip { get; set; }
        [JsonIgnore]
        public Double? PercentSoilAreaB { get; set; }
        [JsonIgnore]
        public Double? PercentSoilAreaC { get; set; }
        [JsonIgnore]
        public Double? PercentSoilAreaD { get; set; }
        [JsonIgnore]
        public Double? RelativeStreamDensity { get; set; }
        [JsonIgnore]
        public Double? HydrographSeparationAnalysis { get; set; }
        [JsonIgnore]
        public Double? StreamVariablityIndex { get; set; }

        

        public override String Warnings 
        { 
            get
            {
                return "";
            }
        }
        #endregion
        #region "Collections & Dictionaries"
        [JsonIgnore]
        private Dictionary<Double, TimeSeriesObservation> FDCTMExceedanceTimeseries { get; set; }
        [JsonIgnore]
        public List<String> ReferanceGageIDList { get; set; }
        private SortedDictionary<Double, Double> _exceedanceProbabilities;
        public SortedDictionary<Double, Double> ExceedanceProbabilities
        {
            get
            {
                return _exceedanceProbabilities;
            }
        }
        public void AddExceedance(Double probability, Double exceedance)
        {
            try
            {
                if (!_exceedanceProbabilities.ContainsKey(probability))
                    _exceedanceProbabilities.Add(probability, exceedance);
            }
            catch (Exception)
            {

            }
        }
        public void Remove(Double probability)
        {
            if (_exceedanceProbabilities.ContainsKey(probability))
                _exceedanceProbabilities.Remove(probability);
        }
        public void Clear()
        {
            _exceedanceProbabilities.Clear();
        }
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public FDCTM():base()
        {
            this.ModelType = "FDCTM";
            this.Description = "Flow Duration Curve Transfer Method Model";
            _exceedanceProbabilities = new SortedDictionary<double, double>();
        }
        public FDCTM(RegressionModel mybase, String stateID):this()
        {
            
            StateCode = stateID;
            StartDate = mybase.StartDate;
            EndDate = mybase.EndDate;
            NWIS_Station_ID = mybase.NWIS_Station_ID;
            LoadParamters(mybase.Parameters);

            this.CanLoad = Init();
        }
        #endregion
        #region IDisposable Support
        // Track whether Dispose has been called.
        private bool disposed = false;

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                base.Dispose(disposing);
                if (disposing)
                {

                    // TODO:Dispose managed resources here.
                    //ie component.Dispose();

                }//EndIF

                // TODO:Call the appropriate methods to clean up
                // unmanaged resources here.
                //ComRelease(Extent);

                // Note disposing has been done.
                disposed = true;


            }//EndIf
        }//End Dispose
        #endregion
        #endregion
        #region "Methods"
        public override Boolean Load()
        {
            //load refGage flows
            ReferanceGage.LoadFullRecord();
            transferFlowDuration(ReferanceGage.GetExceedanceProbability());
            return true;
        }
        public override Boolean Execute()
        {
            if (FDCTMExceedanceTimeseries == null || FDCTMExceedanceTimeseries.Count < 1) throw new Exception("Referance gage or gage discharge is invalid");


            this.EstimatedFlow = new FlowTimeSeries("Flow Duration Curve Transfer Method Estimates", "Estimates computed using Flow Duration Transfer Method");
            //order results and limit
            var tseries = FDCTMExceedanceTimeseries.OrderBy(ts => ts.Value.Date).Where(ts => ts.Value.Date.Date >= StartDate.Value.Date && ts.Value.Date.Date <= EndDate.Value.Date);

            foreach (var observ in tseries)
            {
                if (observ.Value.Value.HasValue) EstimatedFlow.Add(observ.Value.Date, observ.Value.Value.Value);
            }//next observ

            ReferanceGage.LimitDischarge(StartDate.Value, EndDate.Value);

            return true;
        }
        #endregion
        #region "Helper Methods"
        protected override Boolean Init()
        {
            try
            {
                if (!IsValid())
                {
                    this.Message = "One or more of the parameters are invalid";
                    return false;
                }
                initParameters();
                initReferenceGage();
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }

        protected override Boolean IsValid()
        {
            Double? val = null;
            try
            {
                if (!base.IsValid()) return false;

                Boolean basinOK = IsValid(ParameterEnum.e_basinArea, out val);
                if (basinOK) this.BasinArea_sqMi = val;

                Boolean precipOK = IsValid(ParameterEnum.e_meanannualprecip, out val);
                if (precipOK) this.MeanAnnualPrecip = val;

                Boolean soilBOK = IsValid(ParameterEnum.e_percentsoilarea_b, out val);
                if (soilBOK) this.PercentSoilAreaB = val;

                Boolean soilCOK = IsValid(ParameterEnum.e_percentsoilarea_c, out val);
                if (soilCOK) this.PercentSoilAreaC = val;

                Boolean soilDOK = IsValid(ParameterEnum.e_percentsoilarea_d, out val);
                if (soilDOK) this.PercentSoilAreaD = val;

                Boolean hyseoOK = IsValid(ParameterEnum.e_hydrographseparationanalysis, out val);
                if (hyseoOK) this.HydrographSeparationAnalysis = val;

                Boolean rsdOK = IsValid(ParameterEnum.e_relativestreamdensity, out val);
                if (rsdOK) this.RelativeStreamDensity = val;

                Boolean strvarOK = IsValid(ParameterEnum.e_streamvariablityindex, out val);
                if (strvarOK) this.StreamVariablityIndex = val;
             


                return (basinOK & precipOK & soilBOK & soilCOK & soilDOK & hyseoOK & rsdOK & strvarOK);
            }
            catch (Exception e)
            {
                Message = "error validating parameters";
                return false;
            }
        }

        protected override bool initParameters()
        {
            FDCTMServiceAgent sa = null;
            try
            {
                sa = new FDCTMServiceAgent();

                FDCTMServiceAgent.FDCTMStruc l = sa.GetRegionEquations(StateCode);

                l.regressions.ForEach(r => AddExceedance(r.Probability, parseEquation(r.Equation)));

                 return true;
            }
            catch (Exception)
            {
                Message = "error loading parameters";
                return false;
            }
        }

        protected Double parseEquation(String equation)
        {
            //parses the equation to calculate exceedance.
            ExpressionOps expr = new ExpressionOps(equation, this.Parameters.ToDictionary(k=>k.code,v=>v.value ));

            if(expr.IsValid)               
                return expr.Value;

            return -999;
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
                    Int32 numberOfItems = ExceedanceProbabilities.Count()-1;

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

                    exc1 = ExceedanceProbabilities.FirstOrDefault(o=>o.Key == points.a);
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
            catch (Exception ex)
            {
                
                throw;
            }
        }
        #endregion        
        #region Hypermedia Overrided Methods
        public override void LoadLinks(string baseURI, linkType ltype)
        {
            switch (ltype)
            {
                case linkType.e_individual:
                    //add related object links
                    //contacts/{contactId}/project
                    this.LINKS.Add(SetLinkResource(baseURI, "reports", refType.GET, string.Format("reports?state={0}", this.StateCode)));

                    break;

                default:
                    break;
            }



        }//end LoadReferences
      #endregion
        #region Enums

        #endregion
    }
}//end namespace