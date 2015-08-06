using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RegressionService.Resources;
using RegressionService.Utilities.ServiceAgent;
using Newtonsoft.Json;

using WiM.TimeSeries;
using WiM.Resources;

namespace RegressionService.Resources
{
    public class FlowAnywhere : RegressionModel
    {
        #region "Properties"
        [JsonIgnore]
        public Double? BasinArea_sqMi { get; private set; }

        public Double Const1 { get; private set; }
        public Double Const2 { get; private set; }
        public Double Const3 { get; private set; }
        public Double? AreaRato { get; private set; }
        

        public override String Warnings
        {
            get
            {
                return "";
            }
        }
        #endregion
        #region "Collections & Dictionaries"

        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        
        public FlowAnywhere()
        {
            this.ModelType = "FLA";
            this.Description = "Flow Anywhere Model";
        }
        
        public FlowAnywhere(RegressionModel myBase, string StateID):this()
        {
            StateCode = StateID;
            StartDate = myBase.StartDate;
            EndDate = myBase.EndDate;
            NWIS_Station_ID = myBase.NWIS_Station_ID;
            Region = (myBase.Region != null) ? myBase.Region : myBase.Regions.FirstOrDefault().Region;
            Parameters = (myBase.Parameters != null && myBase.Parameters.Count > 0) ? myBase.Parameters : myBase.Regions.FirstOrDefault().Parameters;
            LoadParamters(myBase.Parameters);
            
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
            try
            {
                //load refGage flows
                ReferanceGage.LoadRecord(StartDate.Value, EndDate.Value);              
                
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }

        public override Boolean Execute()
        {
            try
            {
                if (ReferanceGage == null && ReferanceGage.Discharge == null) throw new Exception("Referance gage or gage discharge is invalid");

                this.EstimatedFlow = new FlowTimeSeries("Flow Anywhere Estimates", "Estimates computed using modified drainage area method");

                foreach (TimeSeriesObservation observ in ReferanceGage.Discharge.Observations)
                {
                    var val = observ.Value.HasValue ? this.Const1 * Math.Pow(this.AreaRato.Value, Const2) * Math.Pow(observ.Value.Value, Const3) : 0;

                    this.EstimatedFlow.Add(observ.Date, val, observ.DataCode);                    
                }//next observ

                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }
        
        #endregion
        #region "Helper Methods"
        protected override Boolean Init()
        {
            Boolean isOK = false;
            try
            {
                if (!IsValid()) throw new Exception("One or more paramters are invalid");
                if (!initParameters()) throw new Exception("Failed to load equation");
                if (!initReferenceGage()) throw new Exception("Failed to load reference gage... select another reference gage");
                if(BasinArea_sqMi.HasValue && ReferanceGage != null && ReferanceGage.DrainageArea_sqMI.HasValue)
                    this.AreaRato = this.BasinArea_sqMi / ReferanceGage.DrainageArea_sqMI;
                if (!AreaRato.HasValue) throw new Exception("Failed to calulate area ratio");

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
            Boolean Isok = false;
            Double? DAval = null;
            try 
	        {	        
		        if (!base.IsValid()) return false;

                Isok = (Convert.ToInt32(this.Region) < 4 && Convert.ToInt32(this.Region) > 0);

                Isok = IsValid(ParameterEnum.e_basinArea, out DAval);
                if (Isok) this.BasinArea_sqMi = DAval;


                return Isok;
            }
	        catch (Exception e)
	        {
                Message = "error validating parameters";
                return false;
	        }
        }

        protected override bool initParameters()
        {
            FlowAnywhereServiceAgent sa = null;
            try
            {
                sa = new FlowAnywhereServiceAgent();

                FlowAnywhereServiceAgent.regionEquation l = sa.GetRegionEquations(StateCode, Convert.ToInt32(Region));
                this.Const1 = l.constant1;
                this.Const2 = l.constant2;
                this.Const3 = l.constant3;
                              
                return true;
            }
            catch (Exception)
            {
                Message = "error loading parameters";
                return false;
            }
        }

        protected override void LoadParamters(List<Parameter> pList)
        {
            this.Parameters = new List<Parameter>();
            //get parameters from disk
            RegressionServiceAgent sa = new RegressionServiceAgent();
            this.Parameters = sa.GetParameterList(this.StateCode, this.ModelType, this.Region);
            //sync up parameters
            this.Parameters.ForEach(p => p.value = pList.FirstOrDefault(par => string.Equals(p.code, par.code)).value);
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
        
    }// end class
}//end namespace