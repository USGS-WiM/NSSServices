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
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


#endregion

namespace FU_NSSDB
{
    public class dbOps : IDisposable
    {
        #region "Fields"
        private string connectionString = string.Empty;
        private DbConnection connection;
        public ConnectionType connectionType { get; private set; }
        #endregion
        #region Properties
        private List<string> _message = new List<string>();
        public List<string> Messages
        {
            get { return _message; }
        }
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public dbOps(string pSQLconnstring, ConnectionType pConnectionType, bool doResetTables=false)
        {
            this.connection = null;
            this.connectionString = pSQLconnstring;
            this.connectionType = pConnectionType;
            init();
            if (doResetTables) this.ResetTables();
        }
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
                    if (this.connection.State != ConnectionState.Closed) this.connection.Close();
                    this.connection.Dispose();

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
        public List<T> GetDBItems<T>(SQLType type, params object[] args)
        {
            List<T> dbList = null;
            string sql = string.Empty;
            try
            {
                sql = string.Format(getSQL(type),args);

                this.OpenConnection();
                DbCommand command = getCommand(sql);
                Func<IDataReader, T> fromdr = (Func<IDataReader, T>)Delegate.CreateDelegate(typeof(Func<IDataReader, T>),null, typeof(T).GetMethod("FromDataReader"));

                using (DbDataReader reader = command.ExecuteReader())
                {
                    dbList = reader.Select<T>(fromdr).ToList();
                    sm("DB return count: " + dbList.Count);
                }//end using

                return dbList;
            }
            catch (Exception ex)
            {
                this.sm(ex.Message);
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public bool Update(SQLType type, Int32 pkID, Object[] args)
        {

            string sql = string.Empty;
            try
            {
                this.OpenConnection();
                DbCommand command = getCommand(String.Format(getSQL(type), pkID, args));
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                this.sm(ex.Message);
                return false;
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void ExecuteSql(string fileName)
        {
            try
            {
                List<string> sqlList = null;
                
                using (StreamReader reader = new StreamReader(fileName))
                {
                    sqlList = Regex.Split(reader.ReadToEnd(), @"(?<=[;])").ToList();    
                }//end using   
                sm($"Count: {sqlList.Count}");
                this.OpenConnection();
                for (int i = 0; i < sqlList.Count; i++)
                {
                    var sql = sqlList[i];
                    using (DbCommand command = getCommand(sql))
                    {
                        command.CommandTimeout = 2*60;// timeout
                        var updatedRows = command.ExecuteNonQuery();
                        sm($"Updated {i}, out of {sqlList.Count}");
                    }//end using                        
                }//next item                
            }
            catch (Exception ex)
            {
                this.sm(ex.Message);
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
        } 
        public Int32 AddItem(SQLType type, Object[] args) {
            string sql = string.Empty;
            try
            {
                args = args.Select(a => a == null ? "null" : a).ToArray();
                sql = String.Format(getSQL(type), args);

                this.OpenConnection();
                DbCommand command = getCommand(sql);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT lastval();";
                var id = command.ExecuteScalar();
                return Convert.ToInt32(id);                
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
                sql += @"TRUNCATE TABLE ""Regions"" RESTART IDENTITY CASCADE;
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
        private T FromDataReader<T>(IDataReader r)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] {  });
        }
        private DbCommand getCommand(string sql) {
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
                    results = @"SELECT DISTINCT sl.StatLabel
                                FROM (DepVars dv
                                LEFT JOIN StatLabel sl on (dv.StatisticLabelID = sl.StatisticLabelID))";
                    break;
                case SQLType.e_unittype:
                    results = @"SELECT DISTINCT MetricAbbrev FROM Units UNION SELECT EnglishAbbrev FROM Units";
                    break;
                case SQLType.e_variabletype:
                    //select all variables used in equations and report.
                    results = @"SELECT DISTINCT sl.StatLabel 
                                FROM ([Parameters] p 
                                LEFT JOIN StatLabel sl ON ( p.StatisticLabelID = sl.StatisticLabelID))";
                    break;
                case SQLType.e_statisticgrouptype:
                    results = @"SELECT DISTINCT st.StatisticTypeCode FROM StatType st WHERE st.DefType ='FS'";
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
                    results = @"INSERT INTO ""nss"".""Regions""(""Name"",""Code"") VALUES('{0}','{1}')";
                    break;
                case SQLType.e_regressionregion:
                    results = @"INSERT INTO ""nss"".""RegressionRegions""(""Name"",""Code"",""Description"",""CitationID"") VALUES('{0}', '{1}', '{2}',{3})";
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
                default:
                    break;
            }
            return results;
        }
        private void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open) this.connection.Close();
                this.connection.Open();
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw ex;
            }
        }
        private void CloseConnection()
        {
            try
            {
                if (this.connection.State == ConnectionState.Open) this.connection.Close();
            }
            catch (Exception ex)
            {
                if (this.connection.State == ConnectionState.Open) connection.Close();
                throw ex;
            }
        }
        private void init() {
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
        private void sm(string msg)
        {
            this._message.Add(msg);
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















