//------------------------------------------------------------------------------
//----- ForceUpdate -------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2016 WiM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//             
// 
//   purpose: Forces and update to Streamstats from an access SSDB
//          
//discussion:
//

#region "Comments"
//10.31.2016 jkn - Created
#endregion

#region "Imports"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FU_NSSDB.Resources;
using Newtonsoft.Json;
using NSSDB;
#endregion

namespace FU_NSSDB
{
    public class ForceUpdate
    {
        #region properties
        private List<string> _message = new List<string>();
        public List<string> Messages
        {
            get { return _message; }
        }
        public List<StatisticGroupType> statisticGroupTypeList { get; private set; }
        public List<VariableType> variableTypeList { get; private set; }
        public List<UnitType> unittypeList { get; private set; }
        public List<RegressionType> regressionTypeList { get; private set; }


        private string SSDBConnectionstring = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\WiM\Projects\NSS\DB\StreamStatsDB_2018-11-26.mdb");
        private string NSSDBConnectionstring = string.Format("Server=nsstest.c69uuui2tzs0.us-east-1.rds.amazonaws.com; database={0}; UID={1}; password={2}", "nss", ConfigurationManager.AppSettings["dbuser"], ConfigurationManager.AppSettings["dbpassword"]);
        private dbOps NSSDBOps { get; set; }


        #endregion
        #region Constructors
        public ForceUpdate()
        {
            init();
        }
        #endregion
        #region Methods
        public void VerifyLists()
        {
            List<string> DBUnitAbbr = this.unittypeList.Select(k => k.Abbr.Trim()).ToList();
            List<string> DBVariableList = this.variableTypeList.Select(vt => vt.Code.Trim()).ToList();
            List<string> DBStatisticGroupList = this.statisticGroupTypeList.Select(vt => vt.Code.Trim()).ToList();
            List<string> DBregressionList = this.regressionTypeList.Select(vt => vt.Code.Trim()).ToList();

            List<string> ssdbUnitAbbr = null;
            List<string> ssdbDBVariableList = null;
            List<string> ssdbStatisticGroupList = null;
            List<string> ssdbRegressionList = null;

            using (var ssdb = new dbOps(SSDBConnectionstring, dbOps.ConnectionType.e_access))
            {                
                ssdbUnitAbbr = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_unittype).Select(f=>f.Value.Trim()).ToList();
                ssdbDBVariableList = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_variabletype).Select(f => f.Value.Trim()).ToList();
                ssdbStatisticGroupList = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_statisticgrouptype).Select(f => f.Value.Trim()).ToList();
                ssdbRegressionList = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_regressiontype).Select(f => f.Value.Trim()).ToList();
            }//end using
            
            var diffUnits = ssdbUnitAbbr.Except(DBUnitAbbr).ToList();
            var diffVariable = ssdbDBVariableList.Except(DBVariableList).ToList();
            var diffSG = ssdbStatisticGroupList.Except(DBStatisticGroupList).ToList();
            var diffRegList = ssdbRegressionList.Except(DBregressionList).ToList();


        }
        public void Load() {
            //if (!System.Diagnostics.Debugger.IsAttached) throw new Exception("Must be ran in debug mode");
            try
            {
                System.Diagnostics.Debugger.Break();
                //-------1---------2---------3---------4---------5---------6---------7---------8
                //       This method will erase the database and force an update from/ to the connected databases items above
                //       If this is not the desired outcome please exit code now.
                //-------+---------+---------+---------+---------+---------+---------+---------+
                System.Diagnostics.Debugger.Break();

                //connect to SSDB
                sm("Starting migration " + DateTime.Today.ToShortDateString());
                this.NSSDBOps = new dbOps(NSSDBConnectionstring, dbOps.ConnectionType.e_mysql, true);
                using (var ssdb = new dbOps(SSDBConnectionstring, dbOps.ConnectionType.e_access))
                {
                    var list = ssdb.GetDBItems<FURegion>(dbOps.SQLType.e_region).Where(r => r.Code != "XX" || r.oldID != 10047).ToList();
                    foreach (var region in ssdb.GetDBItems<FURegion>(dbOps.SQLType.e_region).Where(r => r.Code != "XX").ToList())
                    {
                        //remove extra TN
                        if (region.oldID == 10047) continue;

                        if(!PostRegion(region)) continue;
                        //get regressionregion
                        var regRegList = ssdb.GetDBItems<FURegressionRegion>(dbOps.SQLType.e_regressionregion, region.oldID.ToString("00")).Where(r => !string.Equals(r.Name, "Undefined", StringComparison.OrdinalIgnoreCase)).ToList();
                        System.Diagnostics.Debug.WriteLine(region.Name+":regions"+regRegList.Count());
                        foreach (var regReg in regRegList)
                        {
                            if (!PostRegressionRegion(regReg, region.ID)) continue;
                            var equList = ssdb.GetDBItems<FUEquation>(dbOps.SQLType.e_equation, regReg.oldID).ToList();
                            foreach (var equ in equList )
                            {
                                equ.PredictionInterval.CovarianceMatrix = JsonConvert.SerializeObject(ssdb.GetDBItems<FUCovariance>(dbOps.SQLType.e_equationcovariance, equ.oldID).GroupBy(e => e.Row, e => e, (key, g) => g.OrderBy(c => c.Column).Select(x => x.Value.ToString())).Select(c => c.ToList()).ToList());
                                PostEquation(equ, regReg.ID);

                                var varList = ssdb.GetDBItems<FUVariable>(dbOps.SQLType.e_variables, regReg.oldID).Where(v => equ.Equation1.Contains(v.VariableType.Code)).ToList();
                                foreach (var variable in varList) {
                                    PostVariable(variable, equ.ID);
                                }//next variable

                            }//next equation
                        }//next regressionregions
                        System.Diagnostics.Debug.WriteLine("Finished region " + region.Name);
                        sm("Finished.");                        
                    }//next region 
                }//end using                   
            }
            catch (Exception ex)
            {
                throw new Exception("ForceUpdate Load exception", ex);
            }
            finally
            {
                if (this.NSSDBOps != null){ this.NSSDBOps.Dispose(); this.NSSDBOps = null;}
            }
        }

        #endregion
        #region HelperMethods
        private void init() {
            //uses EF to make the connection
            string connectionString = "metadata=res://*/NSSEntityModel.csdl|res://*/NSSEntityModel.ssdl|res://*/NSSEntityModel.msl;provider=MySql.Data.MySqlClient;provider connection string=';server=nsstest.c69uuui2tzs0.us-east-1.rds.amazonaws.com;user id={0};PASSWORD={1};database=nss';";
            using (nssEntities context = new nssEntities(String.Format(connectionString, ConfigurationManager.AppSettings["dbuser"], ConfigurationManager.AppSettings["dbpassword"])))
            {
                statisticGroupTypeList = context.StatisticGroupTypes.ToList();
                variableTypeList = context.VariableTypes.ToList();
                unittypeList = context.UnitTypes.ToList();
                regressionTypeList = context.RegressionTypes.ToList();
            }//end using
            

        }
        private bool PostRegion(FURegion region) {
            try
            {
                region.ID = NSSDBOps.AddItem(dbOps.SQLType.e_region, new object[] { region.Name, region.Code });
                if (region.ID < 1) throw new Exception("region ID came back < 0 ");
                return true;
            }
            catch (Exception ex)
            {
                sm("Failed to post region " + region.Code +" " + ex.Message);
                return false;
            }
        }
        private bool PostRegressionRegion(FURegressionRegion regRegion, Int32 regionID)
        {  
            try
            {
                //citation
                //check if citation exists already before adding
                //regRegion.Citation.Title.Trim(), regRegion.Citation.Author.Trim(), regRegion.Citation.CitationURL.Trim()
                var citationlist = NSSDBOps.GetDBItems<NSSCitation>(dbOps.SQLType.e_getcitation).Where(c => string.Equals(c.CitationURL.Trim(),regRegion.Citation.CitationURL.Trim(),StringComparison.OrdinalIgnoreCase)&&
                                                                                                        string.Equals(c.Author.Trim(), regRegion.Citation.Author.Trim(), StringComparison.OrdinalIgnoreCase)&&
                                                                                                        string.Equals(c.Title.Trim(), regRegion.Citation.Title.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();

                if (citationlist.Count < 1)
                    regRegion.CitationID = NSSDBOps.AddItem(dbOps.SQLType.e_postcitation, new object[] { regRegion.Citation.Title.Trim(), regRegion.Citation.Author.Trim(), regRegion.Citation.CitationURL.Trim() });
                else
                    regRegion.CitationID = citationlist.FirstOrDefault().ID;
                //regressionregion
                regRegion.ID = NSSDBOps.AddItem(dbOps.SQLType.e_regressionregion, new object[] { regRegion.Name.Trim(), regRegion.Code.Trim(), regRegion.Description.Trim(), regRegion.CitationID});
                //RegionRegressionRegion
                var RRRID = NSSDBOps.AddItem(dbOps.SQLType.e_regionregressionregion, new object[] { regionID, regRegion.ID });
                if (regRegion.CitationID < 1 || regRegion.ID < 1 || RRRID < 1) throw new Exception("Error posting Regression region; Citation " + regRegion.CitationID + " Regression region " + regRegion.ID + " RegRegreReg " + RRRID);
                return true;
            }
            catch (Exception ex)
            {
                sm("Failed to post region " + regRegion.Code +" " +ex.Message);
                return false;
            }
        }
        private bool PostEquation(FUEquation equation, Int32 regressionRegionID)
        { 
            try
            {
                //PredictionInterval
                if (equation.PredictionInterval.CovarianceMatrix == "[]") equation.PredictionInterval.CovarianceMatrix = null;

                if((equation.PredictionInterval.BiasCorrectionFactor.HasValue && equation.PredictionInterval.BiasCorrectionFactor > 0) || 
                   (equation.PredictionInterval.Student_T_Statistic.HasValue && equation.PredictionInterval.Student_T_Statistic > 0) || 
                   (equation.PredictionInterval.Variance.HasValue && equation.PredictionInterval.Variance >0)  || 
                   !String.IsNullOrEmpty(equation.PredictionInterval.XIRowVector) || !String.IsNullOrEmpty(equation.PredictionInterval.CovarianceMatrix))
                    equation.PredictionIntervalID = NSSDBOps.AddItem(dbOps.SQLType.e_predictioninterval, new object[] { equation.PredictionInterval.BiasCorrectionFactor, equation.PredictionInterval.Student_T_Statistic, equation.PredictionInterval.Variance, equation.PredictionInterval.XIRowVector, equation.PredictionInterval.CovarianceMatrix });
                
                var unit1 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbr, equation.UnitType.Abbr, StringComparison.OrdinalIgnoreCase));
                var unit2 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbr, equation.otherunit.Abbr, StringComparison.OrdinalIgnoreCase));
                var regType = this.regressionTypeList.FirstOrDefault(rt => string.Equals(rt.Code, equation.RegressionType.Code, StringComparison.OrdinalIgnoreCase));
                var statGrp = this.statisticGroupTypeList.FirstOrDefault(sg => string.Equals(sg.Code, equation.StatisticGroupType.Code, StringComparison.OrdinalIgnoreCase));
                var predID = (equation.PredictionIntervalID.HasValue) ? equation.PredictionIntervalID.ToString() : "null";
                //equation
                equation.ID = NSSDBOps.AddItem(dbOps.SQLType.e_equation, new object[] {regressionRegionID, predID, unit1.ID, equation.Equation1,equation.DA_Exponent, equation.OrderIndex,regType.ID, statGrp.ID,equation.EquivalentYears});

                //equationerrors
                foreach (var eer in equation.EquationErrors)
                {
                    NSSDBOps.AddItem(dbOps.SQLType.e_equationerror, new object[] { equation.ID, eer.ErrorTypeID, eer.Value });
                }//next equation error

                //equation units
                var equUntyps1ID = NSSDBOps.AddItem(dbOps.SQLType.e_equationunitypes, new object[] { equation.ID, unit1.ID });
                if(unit1.ID != unit2.ID)
                    NSSDBOps.AddItem(dbOps.SQLType.e_equationunitypes, new object[] { equation.ID, unit2.ID });



                //if (region.ID < 1) throw new Exception("region ID came back < 0 ");
                return true;
            }
            catch (Exception ex)
            {
                sm("Failed to post equation " + equation.oldID +" "+ex.Message);
                return false;
            }
        }
        private bool PostVariable(FUVariable variable, Int32 equationID)
        {            
            try
            {
                //Variable
                var varType = this.variableTypeList.FirstOrDefault(v => string.Equals(v.Code, variable.VariableType.Code, StringComparison.OrdinalIgnoreCase));
                var unit1 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbr.Trim(), variable.UnitType.Abbr.Trim(), StringComparison.OrdinalIgnoreCase));
                var unit2 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbr.Trim(), variable.otherunit.Abbr.Trim(), StringComparison.OrdinalIgnoreCase));
                variable.ID = NSSDBOps.AddItem(dbOps.SQLType.e_variables, new object[] { equationID, varType.ID, unit1.ID,variable.MinValue,variable.MaxValue });
                
                //variableUnits
                var equUntyps1ID = NSSDBOps.AddItem(dbOps.SQLType.e_variableunitypes, new object[] { variable.ID, unit1.ID });
                if (unit1.ID != unit2.ID)
                    NSSDBOps.AddItem(dbOps.SQLType.e_variableunitypes, new object[] { variable.ID, unit2.ID });

                return true;
            }
            catch (Exception ex)
            {
                sm("Failed to post equation " + variable.oldID + " " + ex.Message);
                return false;
            }
        }
        private void sm(string msg)
        {
            this._message.Add(msg);
        }
        #endregion

    }   
}
