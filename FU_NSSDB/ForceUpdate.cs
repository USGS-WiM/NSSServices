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
// For 64bit MS Access connection using odbc https://mrojas.ghost.io/msaccess-in-dotnetcore/
// must download and install https://www.microsoft.com/en-us/download/details.aspx?id=13255 for some reason, even if you have access installed.

#region "Comments"
//10.31.2016 jkn - Created
#endregion

#region "Imports"
using FU_NSSDB.Resources;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedDB.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private NSSDbOps NSSDBOps { get; set; }


        #endregion
        #region Constructors
        public ForceUpdate(string dbusername, string dbpassword, string accessdb)
        {
            //for 64bit driver add (*.mdb, *.accdb) options
            SSDBConnectionstring = string.Format(@"Driver={{Microsoft Access Driver (*.mdb)}};dbq={0}", accessdb);
            NSSDBConnectionstring = string.Format("Server=test.c69uuui2tzs0.us-east-1.rds.amazonaws.com; database={0}; UID={1}; password={2}", "StatsDB", dbusername, dbpassword);

            init();
        }
        #endregion
        #region Methods
        public bool VerifyLists()
        {
            List<string> DBUnitAbbr = this.unittypeList.Select(k => k.Abbreviation.Trim()).ToList();
            List<string> DBVariableList = this.variableTypeList.Select(vt => vt.Code.Trim().ToUpper()).ToList();
            List<string> DBStatisticGroupList = this.statisticGroupTypeList.Select(vt => vt.Code.Trim().ToUpper()).ToList();
            List<string> DBregressionList = this.regressionTypeList.Select(vt => vt.Code.Trim().ToUpper()).ToList();

            List<string> ssdbUnitAbbr = null;
            List<NSSVariableType> ssdbDBVariableList = null;
            List<NSSStatisticGroupType> ssdbStatisticGroupList = null;
            List<NSSRegressionType> ssdbRegressionList = null;

            using (var ssdb = new NSSDbOps(SSDBConnectionstring, NSSDbOps.ConnectionType.e_access))
            {
                ssdbUnitAbbr = ssdb.GetItems<FUString>(NSSDbOps.SQLType.e_unittype).Select(f => f.Value.Trim()).ToList();
                ssdbDBVariableList = ssdb.GetItems<NSSVariableType>(NSSDbOps.SQLType.e_variabletype).ToList();
                ssdbStatisticGroupList = ssdb.GetItems<NSSStatisticGroupType>(NSSDbOps.SQLType.e_statisticgrouptype).ToList();
                ssdbRegressionList = ssdb.GetItems<NSSRegressionType>(NSSDbOps.SQLType.e_regressiontype).ToList();
            }//end using

            var diffUnits = ssdbUnitAbbr.Except(DBUnitAbbr).ToList();
            var diffVariable = ssdbDBVariableList.Where(v => !DBVariableList.Contains(v.Code.Trim().ToUpper())).ToList();
            var diffSG = ssdbStatisticGroupList.Where(sg => !DBStatisticGroupList.Contains(sg.Code.Trim().ToUpper())).ToList();
            var diffRegList = ssdbRegressionList.Where(r => !DBregressionList.Contains(r.Code.Trim().ToUpper())).ToList();

            if (diffVariable.Count > 0) createUpdateList(diffVariable);
            if (diffRegList.Count > 0) createUpdateList(diffRegList);
            if (diffSG.Count > 0) createUpdateList(diffSG);

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

                using (var ssdb = new NSSDbOps(SSDBConnectionstring, NSSDbOps.ConnectionType.e_access))
                {
                    var list = ssdb.GetItems<FURegion>(NSSDbOps.SQLType.e_region).Where(r => r.Code != "XX" || r.oldID != 10047).ToList();
                    foreach (var region in ssdb.GetItems<FURegion>(NSSDbOps.SQLType.e_region).Where(r => r.Code != "XX").ToList())
                    {
                        //remove extra TN
                        if (region.oldID == 10047) continue;

                        if (!PostRegion(region)) continue;
                        //get regressionregion
                        var regRegList = ssdb.GetItems<FURegressionRegion>(NSSDbOps.SQLType.e_regressionregion, region.oldID.ToString("00")).Where(r => !string.Equals(r.Name, "Undefined", StringComparison.OrdinalIgnoreCase)).ToList();
                        sm(region.Name + ":regions" + regRegList.Count());
                        foreach (var regReg in regRegList)
                        {
                            if (!PostRegressionRegion(regReg, region.ID)) continue;
                            var equList = ssdb.GetItems<FUEquation>(NSSDbOps.SQLType.e_equation, regReg.oldID).ToList();
                            foreach (var equ in equList)
                            {
                                equ.PredictionInterval.CovarianceMatrix = JsonConvert.SerializeObject(ssdb.GetItems<FUCovariance>(NSSDbOps.SQLType.e_equationcovariance, equ.oldID).GroupBy(e => e.Row, e => e, (key, g) => g.OrderBy(c => c.Column).Select(x => x.Value.ToString())).Select(c => c.ToList()).ToList());
                                PostEquation(equ, regReg.ID);

                                var varList = ssdb.GetItems<FUVariable>(NSSDbOps.SQLType.e_variables, regReg.oldID).Where(v => equ.Expression.Contains(v.VariableType.Code)).ToList();
                                foreach (var variable in varList)
                                {
                                    PostVariable(variable, equ.ID);
                                }//next variable

                            }//next equation
                        }//next regressionregions
                        sm("Finished region " + region.Name);
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

            this.NSSDBOps = new NSSDbOps(NSSDBConnectionstring, NSSDbOps.ConnectionType.e_postgresql, false);

            statisticGroupTypeList = NSSDBOps.GetItems<NSSStatisticGroupType>(NSSDbOps.SQLType.e_getstatisticgroups).ToList<StatisticGroupType>();
            variableTypeList = NSSDBOps.GetItems<NSSVariableType>(NSSDbOps.SQLType.e_getvariabletypes).ToList<VariableType>();
            unittypeList = NSSDBOps.GetItems<NSSUnitType>(NSSDbOps.SQLType.e_getunittypes).ToList<UnitType>();
            regressionTypeList = NSSDBOps.GetItems<NSSRegressionType>(NSSDbOps.SQLType.e_getregressiontypes).ToList<RegressionType>();

        }
        private bool PostRegion(FURegion region)
        {
            try
            {
                region.ID = NSSDBOps.AddItem(NSSDbOps.SQLType.e_region, new object[] { region.Name, region.Code });
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
                var citationlist = NSSDBOps.GetItems<NSSCitation>(NSSDbOps.SQLType.e_getcitation).Where(c => string.Equals(c.CitationURL.Trim(), regRegion.Citation.CitationURL.Trim(), StringComparison.OrdinalIgnoreCase) &&
                                                                                                        string.Equals(c.Author.Trim(), regRegion.Citation.Author.Trim(), StringComparison.OrdinalIgnoreCase) &&
                                                                                                        string.Equals(c.Title.Trim(), regRegion.Citation.Title.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();

                if (citationlist.Count < 1)
                    regRegion.CitationID = NSSDBOps.AddItem(NSSDbOps.SQLType.e_postcitation, new object[] { regRegion.Citation.Title.Trim(), regRegion.Citation.Author.Trim(), regRegion.Citation.CitationURL.Trim() });
                else
                    regRegion.CitationID = citationlist.FirstOrDefault().ID;

                // check if regression region exists before continuing (useful if you need to redo equations)
                var regRegionList = NSSDBOps.GetItems<NSSRegressionRegion>(NSSDbOps.SQLType.e_getregressionregions).Where(r => string.Equals(r.Code.Trim(), regRegion.Code.Trim(), StringComparison.OrdinalIgnoreCase) &&
                                                                                                        string.Equals(r.Name.Trim(), regRegion.Name.Trim(), StringComparison.OrdinalIgnoreCase) &&
                                                                                                        string.Equals(r.Description.Trim(), regRegion.Description.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                if (regRegionList.Count == 1)
                {
                    regRegion.ID = regRegionList.FirstOrDefault().ID;
                    return true;
                }
                regRegion.ID = NSSDBOps.AddItem(NSSDbOps.SQLType.e_regressionregion, new object[] { regRegion.Name.Trim(), regRegion.Code.Trim(), regRegion.Description.Trim(), regRegion.CitationID });
                //RegionRegressionRegion
                NSSDBOps.AddItem(NSSDbOps.SQLType.e_regionregressionregion, new object[] { regionID, regRegion.ID });
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
                    equation.PredictionIntervalID = NSSDBOps.AddItem(NSSDbOps.SQLType.e_predictioninterval, new object[] { equation.PredictionInterval.BiasCorrectionFactor, equation.PredictionInterval.Student_T_Statistic, equation.PredictionInterval.Variance, equation.PredictionInterval.XIRowVector, equation.PredictionInterval.CovarianceMatrix });

                var unit1 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbreviation, equation.UnitType.Abbreviation, StringComparison.OrdinalIgnoreCase));
                var unit2 = this.unittypeList.FirstOrDefault(u => string.Equals(u.Abbreviation, equation.otherunit.Abbreviation, StringComparison.OrdinalIgnoreCase));
                var regType = this.regressionTypeList.FirstOrDefault(rt => string.Equals(rt.Code, equation.RegressionType.Code, StringComparison.OrdinalIgnoreCase));
                var statGrp = this.statisticGroupTypeList.FirstOrDefault(sg => string.Equals(sg.Code, equation.StatisticGroupType.Code, StringComparison.OrdinalIgnoreCase));
                var predID = (equation.PredictionIntervalID.HasValue) ? equation.PredictionIntervalID.ToString() : "null";
                //equation
                equation.ID = NSSDBOps.AddItem(NSSDbOps.SQLType.e_equation, new object[] { regressionRegionID, predID, unit1.ID, equation.Expression, equation.DA_Exponent, equation.OrderIndex, regType.ID, statGrp.ID, equation.EquivalentYears });

                //equationerrors
                foreach (var eer in equation.EquationErrors)
                {
                    NSSDBOps.AddItem(NSSDbOps.SQLType.e_equationerror, new object[] { equation.ID, eer.ErrorTypeID, eer.Value });
                }//next equation error

                //equation units
                NSSDBOps.AddItem(NSSDbOps.SQLType.e_equationunitypes, new object[] { equation.ID, unit1.ID });
                if (unit1.ID != unit2.ID)
                    NSSDBOps.AddItem(NSSDbOps.SQLType.e_equationunitypes, new object[] { equation.ID, unit2.ID });

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
                variable.ID = NSSDBOps.AddItem(NSSDbOps.SQLType.e_variables, new object[] { equationID, varType.ID, unit1.ID, variable.MinValue, variable.MaxValue });

                //variableUnits
                NSSDBOps.AddItem(NSSDbOps.SQLType.e_variableunitypes, new object[] { variable.ID, unit1.ID });
                if (unit1.ID != unit2.ID)
                    NSSDBOps.AddItem(NSSDbOps.SQLType.e_variableunitypes, new object[] { variable.ID, unit2.ID });

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
        private void createUpdateList<T>(List<T> diffList)
        {
            string tableName = "";
            List<string> updateList = new List<string>();
            string insertStmnt = @"INSERT INTO {0} VALUES ({1});";

            switch (typeof(T).Name)
            {
                case "NSSVariableType":
                    tableName = @"""shared"".""VariableType""(""Name"",""Code"",""Description"")";
                    updateList = diffList.Cast<NSSVariableType>()
                        .Select(t =>
                            String.Format(insertStmnt, tableName, String.Join(',', new List<string>() { $"'{t.Name}'", $"'{t.Code}'", $"'{t.Description}'" }))).ToList();
                    break;
                case "NSSStatisticGroupType":
                    tableName = @"""shared"".""StatisticGroupType""(""Name"",""Code"")";
                    updateList = diffList.Cast<NSSStatisticGroupType>()
                        .Select(t =>
                            String.Format(insertStmnt, tableName, String.Join(',', new List<string>() { $"'{t.Name}'", $"'{t.Code}'" }))).ToList();
                    break;
                case "NSSRegressionType":
                    tableName = @"""shared"".""RegressionType""(""Name"",""Code"",""Description"")";
                    updateList = diffList.Cast<NSSRegressionType>()
                        .Select(t =>
                            String.Format(insertStmnt, tableName, String.Join(',', new List<string>() { $"'{t.Name}'", $"'{t.Code}'", $"'{t.Description}'" }))).ToList();
                    break;

                default:
                    return;
            }


            using (TextWriter tw = new StreamWriter("..\\"+typeof(T).Name + ".sql"))
            {
                foreach (var s in updateList)
                    tw.WriteLine(s);
            }


        }
        #endregion

    }
}