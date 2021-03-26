//------------------------------------------------------------------------------
//----- postgresqldbOps -------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2015 WiM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//             
// 
//   purpose: Manage databases, provides retrieval/creation/update/deletion
//          
//discussion:
//

#region "Comments"
//02.09.2015 jkn - Created
#endregion

#region "Imports"
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Common;
using System.Data.Odbc;
using Npgsql;
using WIM.Utilities;
using WIM.Utilities.Extensions;


#endregion

namespace FU_NSSDB
{
    public class NSSDbOps : dbOps
    {
        #region "Fields"
        public ConnectionType connectionType { get; private set; }
        #endregion
        #region Constructors
        public NSSDbOps(string pSQLconnstring, ConnectionType pConnectionType, bool doResetTables=false)
            :base()
        {
            this.connectionType = pConnectionType;
            init(pSQLconnstring);
            if (doResetTables) this.ResetTables();
        }
        #endregion        
        #region "Methods"
        public IEnumerable<T> GetItems<T>(SQLType type, params object[] args)
        {
            string sql = string.Empty;
            try
            {
                sql = string.Format(getSQL(type),args);
                return base.GetItems<T>(sql);                
            }
            catch (Exception ex)
            {
                this.sm(ex.Message);
                throw ex;
            }           
        }
        public bool Update(SQLType type, Int32 pkID, Object[] args)
        {

            string sql = string.Empty;
            try
            {
                sql = String.Format(getSQL(type), pkID, args);
                return base.Update(sql);
            }
            catch (Exception ex)
            {
                this.sm(ex.Message);
                return false;
            }            
        }
        public Int32 AddItem(SQLType type, Object[] args) {
            string sql = string.Empty;
            try
            {
                args = args.Select(a => a == null ? "null" : a).ToArray();
                sql = String.Format(getSQL(type), args);
                return base.AddItem(sql);
                   
            }
            catch (Exception ex)
            {
                this.sm(ex.Message);
                return -1;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public bool ResetTables()
        {
            string sql = string.Empty;
            try
            {
                sql += @"TRUNCATE TABLE ""shared"".""Regions"" RESTART IDENTITY CASCADE;
                         TRUNCATE TABLE ""RegionRegressionRegions"" RESTART IDENTITY CASCADE;";
                sql += @"TRUNCATE TABLE ""RegressionRegions"" RESTART IDENTITY CASCADE;
                         TRUNCATE TABLE ""Citations"" RESTART IDENTITY CASCADE;";
                sql += @"TRUNCATE TABLE ""Equations"" RESTART IDENTITY CASCADE;
                         TRUNCATE TABLE ""EquationUnitTypes"" RESTART IDENTITY CASCADE;
                         TRUNCATE TABLE ""PredictionIntervals"" RESTART IDENTITY CASCADE;
                         TRUNCATE TABLE ""EquationErrors"" RESTART IDENTITY CASCADE;";
                sql += @"TRUNCATE TABLE ""Variables"" RESTART IDENTITY CASCADE;
                         TRUNCATE TABLE ""VariableUnitTypes"" RESTART IDENTITY CASCADE;";

                ExecuteSql(sql);                

                return true;
            }
            catch (Exception ex)
            {
                this.sm(ex.Message);
                return false;
                throw ex;
            }
        }
        #endregion
        #region "Helper Methods"
        protected override DbCommand getCommand(string sql) {
            switch (this.connectionType)
            {
                case ConnectionType.e_access:
                    return new OdbcCommand(sql, (OdbcConnection)this.connection);
                case ConnectionType.e_postgresql:
                    return new NpgsqlCommand(sql, (NpgsqlConnection)this.connection);
                //case ConnectionType.e_mysql:
                    //return new MySqlCommand(sql, (MySqlConnection)this.connection);
                default:
                    return null;
            }
        }
        private string getSQL(SQLType type)
        {            
            switch (this.connectionType)
            {
                case ConnectionType.e_access:
                    return getAccessSql(type);
                case ConnectionType.e_postgresql:
                case ConnectionType.e_mysql:
                    return getMySqlSql(type);
                default:
                    return "";
            }            
        }
        private string getAccessSql(SQLType type) {
            string results = string.Empty;
            switch (type)
            {
                case SQLType.e_region:
                    results = @"SELECT State as Name, ST as Code, StateCode as ID FROM States;";
                    break;
                case SQLType.e_regressionregion:
                    results = @"SELECT r.RegionID, r.RegionName, ds.DataSourceID, ds.Citation, ds.CitationURL FROM Regions r
                                LEFT JOIN DataSource ds on (ds.DataSourceID = r.DataSourceID)
                                WHERE StateCode = '{0}';";
                    break;
                case SQLType.e_equation:
                    results = @"SELECT dv.RegionID, dv.DepVarID, dv.Equation, dv.ExpDA,dv.EquivYears,dv.OrderIndex, u.English, u.EnglishAbbrev, u.UnitID, u.MetricAbbrev,
                                       dv.XiVector, dv.BCF, dv.t, dv.Variance, sl.StatisticTypeCode, sl.StatisticTypeID, sl.StatLabel,
                                       dv.StdErr, dv.EstErr, dv.PreErr, dv.PercentCorrect

                                FROM ((DepVars dv
                                INNER JOIN StatLabel sl on (dv.StatisticLabelID = sl.StatisticLabelID))
                                INNER JOIN Units u on (sl.UnitID = u.UnitID)) 
                             
                                WHERE RegionID = {0};";
                    break;
                case SQLType.e_equationcovariance:
                    results = @"SELECT * FROM Covariance 
                                WHERE DepVarID = {0};";
                    break;
                case SQLType.e_variables:
                    results = @"SELECT p.ParmID, p.RegionID, p.Parameter, p.Parm, p.Min, p.Max, p.UnitID, 
                                sl.StatLabel, sl.StatisticTypeCode, 
                                u.English, u.EnglishAbbrev, u.MetricAbbrev

                                FROM (([Parameters] p
                                LEFT JOIN StatLabel sl ON ( p.StatisticLabelID = sl.StatisticLabelID))
                                LEFT JOIN Units u ON (u.UnitID = p.UnitID))
                                WHERE RegionID = {0};";
                    break;

                case SQLType.e_regressiontype:
                    results = @"SELECT DISTINCT (0-1) as ID, sl.StatLabel as Code, sl.Definition as Description, sl.StatisticLabel as Name
                                FROM (DepVars s
                                LEFT JOIN StatLabel sl on (s.StatisticLabelID = sl.StatisticLabelID))
                                LEFT JOIN StatType st on (sl.statisticTypeID = st.StatisticTypeID)
                                WHERE st.DefType = 'FS';";
                    break;
                case SQLType.e_unittype:
                    results = @"SELECT DISTINCT MetricAbbrev FROM Units UNION SELECT EnglishAbbrev FROM Units";
                    break;
                case SQLType.e_variabletype:
                    results = @"SELECT DISTINCT (0-1) as ID, sl.StatLabel as Code, sl.Definition as Description, sl.StatisticLabel as Name, ut.MetricAbbrev as MetricAbbrev, ut.EnglishAbbrev as EnglishAbbrev, st.StatisticTypeCode as StatType
                                FROM ((DepVars s
                                LEFT JOIN StatLabel sl on (s.StatisticLabelID = sl.StatisticLabelID))
                                LEFT JOIN Units ut on (sl.UnitID = ut.UnitID))
                                LEFT JOIN StatType st on (sl.statisticTypeID = st.StatisticTypeID)
                                WHERE st.DefType = 'BC';";
                    break;
                case SQLType.e_statisticgrouptype:
                    results = @"SELECT DISTINCT (0-1) as ID, st.StatisticTypeCode as Code, st.StatisticType as Name, st.DefType as DefType
                                FROM StatType st;";
                    break;
                default:
                    sm("invalid sqltype");
                    break;
            }

            return results;
        }
        private string getMySqlSql(SQLType type)
        {
            string results = string.Empty;
            switch (type)
            {
                case SQLType.e_region:
                    results = @"INSERT INTO ""shared"".""Regions""(""Name"",""Code"") VALUES('{0}','{1}')";
                    break;
                case SQLType.e_regressionregion:
                    results = @"INSERT INTO ""nss"".""RegressionRegions""(""Name"",""Code"",""Description"",""CitationID"",""StatusID"") VALUES('{0}', '{1}', '{2}',{3}, {4})";
                    break;
                case SQLType.e_equation:
                    results = @"INSERT INTO ""nss"".""Equations""(""RegressionRegionID"",""PredictionIntervalID"",""UnitTypeID"",""Expression"",""DA_Exponent"",""OrderIndex"",""RegressionTypeID"",""StatisticGroupTypeID"",""EquivalentYears"") 
                                VALUES({0},{1},{2},'{3}',{4},{5},{6},{7},{8})";
                    break;
                case SQLType.e_variables:
                    results = @"INSERT INTO ""nss"".""Variables""(""EquationID"",""VariableTypeID"",""UnitTypeID"",""MinValue"",""MaxValue"") VALUES({0},{1},{2},{3},{4})";
                    break;
                case SQLType.e_variableunitypes:
                    results = @"INSERT INTO ""nss"".""VariableUnitTypes""(""VariableID"",""UnitTypeID"") VALUES({0},{1})";
                    break;
                case SQLType.e_postcitation:
                    results = @"INSERT INTO ""nss"".""Citations""(""Title"",""Author"",""CitationURL"") VALUES('{0}','{1}','{2}')";
                    break;
                case SQLType.e_getcitation:
                    results = @"SELECT * FROM ""nss"".""Citations""";
                    break;
                case SQLType.e_regionregressionregion:
                    results = @"INSERT INTO ""nss"".""RegionRegressionRegions""(""RegionID"",""RegressionRegionID"") VALUES({0},{1})";
                    break;
                case SQLType.e_predictioninterval:
                    results = @"INSERT INTO ""nss"".""PredictionIntervals""(""BiasCorrectionFactor"",""Student_T_Statistic"",""Variance"",""XIRowVector"",""CovarianceMatrix"") 
                                VALUES({0},{1},{2},'{3}','{4}')";
                    break;
                case SQLType.e_equationunitypes:
                    results = @"INSERT INTO ""nss"".""EquationUnitTypes""(""EquationID"",""UnitTypeID"") VALUES({0},{1})";
                    break;
                case SQLType.e_equationerror:
                    results = @"INSERT INTO ""nss"".""EquationErrors""(""EquationID"",""ErrorTypeID"",""Value"") VALUES({0},{1},{2})";
                    break;
                case SQLType.e_getstatisticgroups:
                    results= @"SELECT * FROM ""nss"".""StatisticGroupType_view""";
                    break;
                case SQLType.e_getvariabletypes:
                    results = @"SELECT * FROM ""nss"".""VariableType_view""";
                    break;
                case SQLType.e_getunittypes:
                    results= @"SELECT * FROM ""nss"".""UnitType_view""";
                    break;
                case SQLType.e_getregressiontypes:
                    results= @"SELECT * FROM ""nss"".""RegressionType_view""";
                    break;
                case SQLType.e_getregressionregions:
                    results = @"SELECT * FROM ""nss"".""RegressionRegions""";
                    break;
                default:
                    break;
            }
            return results;
        }

        protected void init(string connectionString) {
            switch (connectionType)
            {
                case ConnectionType.e_access:
                    this.connection = new OdbcConnection(connectionString);
                    break;
                case ConnectionType.e_postgresql:
                    this.connection = new NpgsqlConnection(connectionString);
                    break;
                case ConnectionType.e_mysql:
                    //this.connection = new MySqlConnection(connectionString);
                    break;
                default:
                    break;
            }

        }                
        #endregion
        #region "Enumerated Constants"
        public enum SQLType
        {
            e_region,
            e_regressionregion,
            e_equation,            
            e_variables,

            e_getcitation,
            e_getstatisticgroups,
            e_getvariabletypes,
            e_getunittypes,
            e_getregressiontypes,
            e_getregressionregions,
            e_postcitation,
            e_regionregressionregion,
            e_predictioninterval,
            e_equationunitypes,
            e_equationerror,
            e_variableunitypes,

            e_errortype,
            e_statisticgrouptype,
            e_variabletype,
            e_unittype,
            e_regressiontype,
            e_equationcovariance,
        }
        public enum ConnectionType
        {
            e_access,
            e_postgresql,
            e_mysql
        }
        #endregion

    }
}