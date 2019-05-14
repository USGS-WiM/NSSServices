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
//Access connection using odbc https://mrojas.ghost.io/msaccess-in-dotnetcore/
// must download and install https://www.microsoft.com/en-us/download/details.aspx?id=13255 for some reason, even if you have access installed.

#region "Comments"
//10.31.2016 jkn - Created
#endregion

#region "Imports"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FU_NSSDB.Resources;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NSSDB;
using SharedDB.Resources;
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


        private string SSDBConnectionstring;
        private string NSSDBConnectionstring;
        private dbOps NSSDBOps { get; set; }


        #endregion
        #region Constructors
        public ForceUpdate(string dbusername, string dbpassword, string accessdb)
        {
            SSDBConnectionstring = string.Format(@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};dbq={0}", accessdb);
            NSSDBConnectionstring = string.Format("Server=test.c69uuui2tzs0.us-east-1.rds.amazonaws.com; database={0}; UID={1}; password={2}", "StatsDB", dbusername, dbpassword);

            init();
        }
        #endregion
        #region Methods
        public bool VerifyLists()
        {
            List<string> DBUnitAbbr = this.unittypeList.Select(k => k.Abbreviation.Trim()).ToList();
            List<string> DBVariableList = this.variableTypeList.Select(vt => vt.Code.Trim()).ToList();
            List<string> DBStatisticGroupList = this.statisticGroupTypeList.Select(vt => vt.Code.Trim()).ToList();
            List<string> DBregressionList = this.regressionTypeList.Select(vt => vt.Code.Trim()).ToList();

            List<string> ssdbUnitAbbr = null;
            List<string> ssdbDBVariableList = null;
            List<string> ssdbStatisticGroupList = null;
            List<string> ssdbRegressionList = null;

            using (var ssdb = new dbOps(SSDBConnectionstring, dbOps.ConnectionType.e_access))
            {
                ssdbUnitAbbr = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_unittype).Select(f => f.Value.Trim()).ToList();
                ssdbDBVariableList = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_variabletype).Select(f => f.Value.Trim()).ToList();
                ssdbStatisticGroupList = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_statisticgrouptype).Select(f => f.Value.Trim()).ToList();
                ssdbRegressionList = ssdb.GetDBItems<FUString>(dbOps.SQLType.e_regressiontype).Select(f => f.Value.Trim()).ToList();
            }//end using

            var diffUnits = ssdbUnitAbbr.Except(DBUnitAbbr).ToList();
            var diffVariable = ssdbDBVariableList.Except(DBVariableList).ToList();
            var diffSG = ssdbStatisticGroupList.Except(DBStatisticGroupList).ToList();
            var diffRegList = ssdbRegressionList.Except(DBregressionList).ToList();

            return diffUnits.Count < 2 && diffVariable.Count < 1 && diffSG.Count < 1 && diffRegList.Count < 1;

        }
        public void Load()
        {
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
                this.NSSDBOps.ResetTables();

                using (var ssdb = new dbOps(SSDBConnectionstring, dbOps.ConnectionType.e_access))
                {
                    var list = ssdb.GetDBItems<FURegion>(dbOps.SQLType.e_region).Where(r => r.Code != "XX" || r.oldID != 10047).ToList();
                    foreach (var region in ssdb.GetDBItems<FURegion>(dbOps.SQLType.e_region).Where(r => r.Code != "XX").ToList())
                    {
                        //remove extra TN
                        if (region.oldID == 10047) continue;

                        if (!PostRegion(region)) continue;
                        //get regressionregion
                        var regRegList = ssdb.GetDBItems<FURegressionRegion>(dbOps.SQLType.e_regressionregion, region.oldID.ToString("00")).Where(r => !string.Equals(r.Name, "Undefined", StringComparison.OrdinalIgnoreCase)).ToList();
                        System.Diagnostics.Debug.WriteLine(region.Name + ":regions" + regRegList.Count());
                        foreach (var regReg in regRegList)
                        {
                            if (!PostRegressionRegion(regReg, region.ID)) continue;
                            var equList = ssdb.GetDBItems<FUEquation>(dbOps.SQLType.e_equation, regReg.oldID).ToList();
                            foreach (var equ in equList)
                            {
                                equ.PredictionInterval.CovarianceMatrix = JsonConvert.SerializeObject(ssdb.GetDBItems<FUCovariance>(dbOps.SQLType.e_equationcovariance, equ.oldID).GroupBy(e => e.Row, e => e, (key, g) => g.OrderBy(c => c.Column).Select(x => x.Value.ToString())).Select(c => c.ToList()).ToList());
                                PostEquation(equ, regReg.ID);

                                var varList = ssdb.GetDBItems<FUVariable>(dbOps.SQLType.e_variables, regReg.oldID).Where(v => equ.Expression.Contains(v.VariableType.Code)).ToList();
                                foreach (var variable in varList)
                                {
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
                if (this.NSSDBOps != null) { this.NSSDBOps.Dispose(); this.NSSDBOps = null; }
            }
        }
        public void LoadSqlFiles(string SqlFileDirectory)
        {
            DirectoryInfo di = new DirectoryInfo(SqlFileDirectory);
            FileInfo[] rgFiles = di.GetFiles("*.sql");
            foreach (FileInfo fi in rgFiles)
            {
                sm(fi.Name);
                NSSDBOps.ExecuteSql(fi);
            }//next file              
        }
        #endregion
        #region HelperMethods
        private void init()
        {

            this.NSSDBOps = new dbOps(NSSDBConnectionstring, dbOps.ConnectionType.e_postgresql, false);

            statisticGroupTypeList = NSSDBOps.GetDBItems<NSSStatisticGroupType>(dbOps.SQLType.e_getstatisticgroups).ToList<StatisticGroupType>();
            variableTypeList = NSSDBOps.GetDBItems<NSSVariableType>(dbOps.SQLType.e_getvariabletypes).ToList<VariableType>();
            unittypeList = NSSDBOps.GetDBItems<NSSUnitType>(dbOps.SQLType.e_getunittypes).ToList<UnitType>();
            regressionTypeList = NSSDBOps.GetDBItems<NSSRegressionType>(dbOps.SQLType.e_getregressiontypes).ToList<RegressionType>();

        }
        private bool PostRegion(FURegion region)
        {
            try
            {
                region.ID = NSSDBOps.AddItem(dbOps.SQLType.e_region, new object[] { region.Name, region.Code });
                if (region.ID < 1) throw new Exception("region ID came back < 0 ");
                return true;
            }
            catch (Exception ex)
            {
                sm("Failed to post region " + region.Code + " " + ex.Message);
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
                var citationlist = NSSDBOps.GetDBItems<NSSCitation>(dbOps.SQLType.e_getcitation).Where(c => string.Equals(c.CitationURL.Trim(), regRegion.Citation.CitationURL.Trim(), StringComparison.OrdinalIgnoreCase) &&
                                                                                                        string.Equals(c.Author.Trim(), regRegion.Citation.Author.Trim(), StringComparison.OrdinalIgnoreCase) &&
                                                                                                        string.Equals(c.Title.Trim(), regRegion.Citation.Title.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();

                if (citationlist.Count < 1)
                    regRegion.CitationID = NSSDBOps.AddItem(dbOps.SQLType.e_postcitation, new object[] { regRegion.Citation.Title.Trim(), regRegion.Citation.Author.Trim(), regRegion.Citation.CitationURL.Trim() });
                else
                    regRegion.CitationID = citationlist.FirstOrDefault().ID;
                //regressionregion
                regRegion.ID = NSSDBOps.AddItem(dbOps.SQLType.e_regressionregion, new object[] { regRegion.Name.Trim(), regRegion.Code.Trim(), regRegion.Description.Trim(), regRegion.CitationID });
                //RegionRegressionRegion
                NSSDBOps.AddItem(dbOps.SQLType.e_regionregressionregion, new object[] { regionID, regRegion.ID });
                if (regRegion.CitationID < 1 || regRegion.ID < 1) throw new Exception("Error posting Regression region; Citation " + regRegion.CitationID + " Regression region " + regRegion.ID);
                return true;
            }
            catch (Exception ex)
            {
                sm("Failed to post region " + regRegion.Code + " " + ex.Message);
                return false;
            }
        }
        private bool PostEquation(FUEquation equation, Int32 regressionRegionID)
        {
            try
            {
                //PredictionInterval
                if (equation.PredictionInterval.CovarianceMatrix == "[]") equation.PredictionInterval.CovarianceMatrix = null;

                if ((equation.PredictionInterval.BiasCorrectionFactor.HasValue && equation.PredictionInterval.BiasCorrectionFactor > 0) ||
                   (equation.PredictionInterval.Student_T_Statistic.HasValue && equation.PredictionInterval.Student_T_Statistic > 0) ||
                   (equation.PredictionInterval.Variance.HasValue && equation.PredictionInterval.Variance > 0) ||
                   !String.IsNullOrEmpty(equation.PredictionInterval.XIRowVector) || !String.IsNullOrEmpty(equation.PredictionInterval.CovarianceMatrix))
                    equation.PredictionIntervalID = NSSDBOps.AddItem(dbOps.SQLType.e_predictioninterval, new object[] { equation.PredictionInterval.BiasCorrectionFactor, equation.PredictionInterval.Student_T_Statistic, equation.PredictionInterval.Variance, equation.PredictionInterval.XIRowVector, equation.PredictionInterval.CovarianceMatrix });

                var unit1 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbreviation, equation.UnitType.Abbreviation, StringComparison.OrdinalIgnoreCase));
                var unit2 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbreviation, equation.otherunit.Abbreviation, StringComparison.OrdinalIgnoreCase));
                var regType = this.regressionTypeList.FirstOrDefault(rt => string.Equals(rt.Code, equation.RegressionType.Code, StringComparison.OrdinalIgnoreCase));
                var statGrp = this.statisticGroupTypeList.FirstOrDefault(sg => string.Equals(sg.Code, equation.StatisticGroupType.Code, StringComparison.OrdinalIgnoreCase));
                var predID = (equation.PredictionIntervalID.HasValue) ? equation.PredictionIntervalID.ToString() : "null";
                //equation
                equation.ID = NSSDBOps.AddItem(dbOps.SQLType.e_equation, new object[] { regressionRegionID, predID, unit1.ID, equation.Expression, equation.DA_Exponent, equation.OrderIndex, regType.ID, statGrp.ID, equation.EquivalentYears });

                //equationerrors
                foreach (var eer in equation.EquationErrors)
                {
                    NSSDBOps.AddItem(dbOps.SQLType.e_equationerror, new object[] { equation.ID, eer.ErrorTypeID, eer.Value });
                }//next equation error

                //equation units
                NSSDBOps.AddItem(dbOps.SQLType.e_equationunitypes, new object[] { equation.ID, unit1.ID });
                if (unit1.ID != unit2.ID)
                    NSSDBOps.AddItem(dbOps.SQLType.e_equationunitypes, new object[] { equation.ID, unit2.ID });

                //if (region.ID < 1) throw new Exception("region ID came back < 0 ");
                return true;
            }
            catch (Exception ex)
            {
                sm("Failed to post equation " + equation.oldID + " " + ex.Message);
                return false;
            }
        }
        private bool PostVariable(FUVariable variable, Int32 equationID)
        {
            try
            {
                //Variable
                var varType = this.variableTypeList.FirstOrDefault(v => string.Equals(v.Code, variable.VariableType.Code, StringComparison.OrdinalIgnoreCase));
                var unit1 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbreviation.Trim(), variable.UnitType.Abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
                var unit2 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbreviation.Trim(), variable.otherunit.Abbreviation.Trim(), StringComparison.OrdinalIgnoreCase));
                variable.ID = NSSDBOps.AddItem(dbOps.SQLType.e_variables, new object[] { equationID, varType.ID, unit1.ID, variable.MinValue, variable.MaxValue });

                //variableUnits
                NSSDBOps.AddItem(dbOps.SQLType.e_variableunitypes, new object[] { variable.ID, unit1.ID });
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
            System.Diagnostics.Debug.WriteLine(msg);
            Console.WriteLine(msg);
            this._message.Add(msg);
        }
        #endregion

    }
}