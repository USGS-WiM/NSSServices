using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using RegressionService.Utilities.ServiceAgent;
using WiM.TimeSeries;
using WiM.Resources;

using Newtonsoft.Json;

namespace RegressionService.Resources
{
    public class RegressionModel:HypermediaEntity
    {
        #region Properties
        [JsonIgnore]
        public Boolean CanLoad { get; protected set; }
        
        public virtual String StateCode { get; set; }

        public virtual String ModelType { get; set; }
        public String Description { get; set; }
        public String Message { get; set; }
        public virtual String Warnings 
        { 
            get
            {
                return "";
            }
        }

        //required parameters
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Parameter> Parameters { get; set; }

        //optional
        public String Region { get; set; }
        public String NWIS_Station_ID { get; set; }
        public List<RegionList> Regions { get; set; }

        //generated parameters
        public Station ReferanceGage { get; set; }
        public FlowTimeSeries EstimatedFlow { get; set; }

        #endregion
        #region "Constructor and IDisposable Support"
        public RegressionModel()
        {
        }
        #region Constructors
        #endregion
        #region IDisposable Support
        // Track whether Dispose has been called.
        private bool disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        } //End Dispose

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
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
        #region "Base Methods"
        protected virtual Boolean Init()
        {
            throw new NotImplementedException();
        }

        public virtual Boolean Load()
        {
            throw new NotImplementedException();
        }

        public virtual Boolean Execute()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region "Helper Methods"
        protected virtual void LoadParamters(List<Parameter> pList){
            this.Parameters = new List<Parameter>();
            //get parameters from disk
            RegressionServiceAgent sa = new RegressionServiceAgent();
            this.Parameters = sa.GetParameterList(this.StateCode, this.ModelType);
            //sync up parameters
            this.Parameters.ForEach(p => p.value = pList.FirstOrDefault(par => string.Equals(p.code, par.code)).value);                
        }
        protected virtual Boolean IsValid()
        {
            Boolean Isok = false;
            try
            {
                //ensure props are within acceptiable ranges
                Isok = (this.StartDate.HasValue && StartDate.Value.Year > 1900 &&
                        EndDate.HasValue && EndDate.Value.Year > 1900);

                return Isok;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected Boolean IsValid(ParameterEnum param, out Double? val)
        {
            Boolean Isok = false;
            val = null;
            try 
	        {	
                val = getParameterValue(getParameterName(param));
                Isok = (val != null && val.HasValue && val >= 0);

                return Isok;
            }
            catch (Exception e)
            {
                Message = "error validating parameters " + param.ToString();
                return false;
            }
        }
        
        protected Double? getParameterValue(string parameterName)
        {
            try
            {
                return Parameters.FirstOrDefault(p => String.Equals(p.name, parameterName,
                                            StringComparison.OrdinalIgnoreCase)).value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected virtual Boolean initParameters(){ throw new NotImplementedException();}

        protected virtual Boolean initReferenceGage()
        {
            StationServiceAgent sa = null;
            try
            {
                if (String.IsNullOrEmpty(this.NWIS_Station_ID)) throw new Exception("No nwis gage ID");
                sa = new StationServiceAgent(ConfigurationManager.AppSettings["nwis"]);

                this.ReferanceGage = sa.GetNWISStation(this.NWIS_Station_ID);     

                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;

                return false;
            }
            finally
            {
                sa = null;
            }
        }
        
        protected String getParameterName(ParameterEnum p)
        {
            switch (p)
            {
                case ParameterEnum.e_basinArea:
                    return "Drainage Area";
                case ParameterEnum.e_meanannualprecip:
                    return "Mean Annual Precip";
                case ParameterEnum.e_percentsoilarea_b:
                    return "Percent Area Soil Type B";
                case ParameterEnum.e_percentsoilarea_c:
                    return "Percent Area Soil Type C";
                case ParameterEnum.e_percentsoilarea_d:
                    return "Percent Area Soil Type D";
                case ParameterEnum.e_relativestreamdensity:
                    return "Relative Stream Density";
                case ParameterEnum.e_hydrographseparationanalysis:
                    return "Hydrograph Separation Analysis";
                case ParameterEnum.e_streamvariablityindex:
                    return "Stream Variability Index";
                default:
                    throw new Exception("No enum specified");
            }//end switch
        }

        #endregion
        #region Structures
        public struct RegionList
        {
            public List<Parameter> Parameters { get; set; }
            public String Region { get; set; }
        }
        #endregion
        #region Hypermedia Overrided Methods
        public override void LoadLinks(string baseURI, linkType ltype)
        {
            switch (ltype)
            {
                case linkType.e_group:
                    //self
                    this.LINKS.Add(GetLinkResource(baseURI, "self", refType.GET));

                    break;

                case linkType.e_individual:
                    //add related object links
                    //contacts/{contactId}/project
                    this.LINKS.Add(SetLinkResource(baseURI, "reports", refType.GET, string.Format("reports?state={0}", this.StateCode)));
                    //contacts/{contactId}/organization
                    this.LINKS.Add(SetLinkResource(baseURI, "estimate", refType.GET, string.Format("models/{0}/estimate?state={1}", this.ModelType, this.StateCode)));

                    break;

                default:
                    break;
            }



        }//end LoadReferences
        protected override string getRelativeURI(refType rType)
        {
            string uriString = "";
            switch (rType)
            {
                case refType.GET:
                    uriString = string.Format("models/{0}/def?state={1}", this.ModelType, this.StateCode);
                    break;

                case refType.POST:
                case refType.PUT:
                case refType.DELETE:
                    break;

            }
            return uriString;
        }
        #endregion
        #region Enumerations
        protected enum ParameterEnum
        {
            e_basinArea,
            e_percentsoilarea_b,
            e_percentsoilarea_c,
            e_percentsoilarea_d,
            e_relativestreamdensity,
            e_hydrographseparationanalysis,
            e_streamvariablityindex,
            e_meanannualprecip
        }
        #endregion
    }
}//end namespace