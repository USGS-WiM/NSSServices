using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WiM.Resources;

using Newtonsoft.Json;
using NSSService.Utilities.ServiceAgent;
using WiM.Utilities;

using WiM.TimeSeries;

namespace NSSService.Resources
{
    public class QRegression : RegressionModel
    {
        #region "Properties"
      
        public override String Warnings 
        { 
            get
            {
                return "";
            }
        }
        #endregion
        #region "Collections & Dictionaries"
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
        public QRegression()
        {
            this.ModelType = "Q";
            this.Description = "Discharge estimates based on regression equations";
            _exceedanceProbabilities = new SortedDictionary<double, double>();
        }
        public QRegression(RegressionModel mybase, String stateID)
            : this()
        {
            Description = mybase.Description;
            ModelType = mybase.ModelType;
            StateCode = stateID;
            Region = (mybase.Region != null) ? mybase.Region :null;
            Parameters = (mybase.Parameters != null && mybase.Parameters.Count > 1) ? mybase.Parameters : mybase.Regions.FirstOrDefault().Parameters;
            
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
            return true;
        }
        public override Boolean Execute()
        {
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
                return (true);
            }
            catch (Exception e)
            {
                Message = "error validating parameters";
                return false;
            }
        }

        protected override bool initParameters()
        {
            QRegressionServiceAgent sa = null;
            try
            {
                sa = new QRegressionServiceAgent(this.Region);

                QRegressionServiceAgent.QRegressionStruc l = sa.GetRegionEquations(StateCode, ModelType);

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
        #region Enums

        #endregion
    }
}//end namespace