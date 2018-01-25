using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MofDoc.Class
{
    public class SqlConnector
    {
        internal static string serverIpAddress = "localhost";//"10.12.11.13\\SQLEXPRESS";
        internal static string serverAdminName = "sa";
        internal static string serverAdminPass = "diamond";//"newmanager";
        internal static Dictionary<string, SqlConnection> dbInfos = null;

        internal static void ResetConnection()
        {
            if (dbInfos == null) return;
            foreach (KeyValuePair<string, SqlConnection> entry in dbInfos)
            {
                dbInfos.Single(t => t.Key.Equals(entry.Key)).Value.Close();
                dbInfos.Single(t => t.Key.Equals(entry.Key)).Value.Open();
            }
        }

        internal static void ResetConnection(string dbName)
        {
            if (dbInfos == null) return;
            if (!dbInfos.ContainsKey(dbName)) return;
            dbInfos.Single(t => t.Key.Equals(dbName)).Value.Close();
            dbInfos.Single(t => t.Key.Equals(dbName)).Value.Open();
        }

        internal static SqlConnection getConnection(string dbName)
        {
            SqlConnection sqlConn = null;
            try
            {
                if (dbInfos == null)
                    dbInfos = new Dictionary<string, SqlConnection>();

                if (!dbInfos.ContainsKey(dbName))
                {
                    sqlConn = new SqlConnection(string.Format("Initial Catalog={0}; Data Source={1}; User ID={2}; Password={3}", dbName, serverIpAddress, serverAdminName, serverAdminPass));
                    sqlConn.Open();
                    dbInfos.Add(dbName, sqlConn);
                    return sqlConn;
                }
                else
                {
                    sqlConn = dbInfos.Single(t => t.Key.Equals(dbName)).Value;
                    if (sqlConn == null || sqlConn.State.Equals(System.Data.ConnectionState.Closed) || !sqlConn.Database.Equals(dbName))
                    {
                        sqlConn = new SqlConnection(string.Format("Initial Catalog={0}; Data Source={1}; User ID={2}; Password={3}", dbName, serverIpAddress, serverAdminName, serverAdminPass));
                        sqlConn.Open();
                        dbInfos[dbName] = sqlConn;
                    }
                    return sqlConn;
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдож чадсангүй!: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдож чадсангүй!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдож чадсангүй!: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдож чадсангүй!", serverIpAddress), ex);
            }
            finally { sqlConn = null; }
        }

        internal static DataSet GetByQuery(string query)
        {
            SqlCommand sqlCmd = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            try
            {
                sqlCmd = new SqlCommand(query, getConnection("master"));
                sqlCmd.CommandType = CommandType.Text;

                sqlDataAdapter = new SqlDataAdapter(sqlCmd);

                ds = new DataSet();
                sqlDataAdapter.Fill(ds);
                return ds;
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally
            {
                sqlCmd = null;
                sqlDataAdapter = null;
                ds = null;
            }
        }

        internal static DataSet GetByQuery(string dbName, string query)
        {
            SqlCommand sqlCmd = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            try
            {
                sqlCmd = new SqlCommand(query, getConnection(dbName));
                sqlCmd.CommandType = CommandType.Text;

                sqlDataAdapter = new SqlDataAdapter(sqlCmd);

                ds = new DataSet();
                sqlDataAdapter.Fill(ds);
                return ds;
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally
            {
                sqlCmd = null;
                sqlDataAdapter = null;
                ds = null;
            }
        }

        internal static void Insert(string dbName, string tableName, Dictionary<string, string> parameters)
        {
            SqlCommand sqlCmd = null;
            string query = null;
            string columns = null;
            string parameterStr = null;
            int count = 0;
            try
            {
                if (parameters == null)
                    throw new MofException("Дамжуулсан утга хоосон байна! Утга дамжуулна уу.");
                if (parameters.Count.Equals(0))
                    throw new MofException("Дамжуулсан утга хоосон байна! Утга дамжуулна уу.");

                foreach(KeyValuePair<string, string> entry in parameters)
                {
                    columns += count.Equals(0) ? entry.Key : ',' + entry.Key;
                    parameterStr += count.Equals(0) ? string.IsNullOrEmpty(entry.Value) ? "NULL" : entry.Value 
                        : ',' + (string.IsNullOrEmpty(entry.Value) ? "NULL" : entry.Value);
                    count++;
                }

                query = string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, columns, parameterStr);
                sqlCmd = new SqlCommand(query, getConnection(dbName));
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(ex.Number, string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { sqlCmd = null; query = null; columns = null; parameterStr = null;}
        }

        internal static void Update(string dbName, string tableName, Dictionary<string, string> parameters, Dictionary<string, string> filters)
        {
            SqlCommand sqlCmd = null;
            string query = null;
            string parameterStr = null;
            string filterStr = null;
            int count = 0;
            try
            {
                if (parameters == null)
                    throw new MofException("Дамжуулсан утга хоосон байна! Утга дамжуулна уу.");
                if (parameters.Count.Equals(0))
                    throw new MofException("Дамжуулсан утга хоосон байна! Утга дамжуулна уу.");

                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    parameterStr += count.Equals(0) ? string.Format("{0} = {1}", entry.Key, string.IsNullOrEmpty(entry.Value) ? "NULL" : entry.Value) 
                        : ',' + string.Format(" {0} = {1}", entry.Key, string.IsNullOrEmpty(entry.Value) ? "NULL" : entry.Value);
                    count++;
                }

                count = 0;
                if (filters != null)
                {
                    foreach (KeyValuePair<string, string> entry in filters)
                    {
                        filterStr += count.Equals(0) ? string.Format("{0} {1}", entry.Key, entry.Value) : string.Format(" AND {0} {1}", entry.Key, entry.Value);
                        count++;
                    }
                    query = string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, parameterStr, filterStr);
                }
                else query = string.Format("UPDATE {0} SET {1}", tableName, parameterStr);
                sqlCmd = new SqlCommand(query, getConnection(dbName));
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(ex.Number, string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { sqlCmd = null; query = null; parameterStr = null; filterStr = null; }
        }

        internal static void UpdateByPkId(string dbName, string tableName, Dictionary<string, string> parameters, decimal pkId)
        {
            SqlCommand sqlCmd = null;
            string query = null;
            string parameterStr = null;
            string filterStr = null;
            int count = 0;
            try
            {
                if (parameters == null)
                    throw new MofException("Дамжуулсан утга хоосон байна! Утга дамжуулна уу.");
                if (parameters.Count.Equals(0))
                    throw new MofException("Дамжуулсан утга хоосон байна! Утга дамжуулна уу.");

                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    parameterStr += count.Equals(0) ? string.Format("{0} = {1}", entry.Key, string.IsNullOrEmpty(entry.Value) ? "NULL" : entry.Value)
                        : ',' + string.Format(" {0} = {1}", entry.Key, string.IsNullOrEmpty(entry.Value) ? "NULL" : entry.Value);
                    count++;
                }
                filterStr = string.Format("PKID = {0}", pkId);
                query = string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, parameterStr, filterStr);
                sqlCmd = new SqlCommand(query, getConnection(dbName));
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(ex.Number, string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { sqlCmd = null; query = null; parameterStr = null; filterStr = null; }
        }

        internal static void Delete(string dbName, string tableName, Dictionary<string, string> filters)
        {
            SqlCommand sqlCmd = null;
            string query = null;
            string filterStr = null;
            int count = 0;
            try
            {
                foreach (KeyValuePair<string, string> entry in filters)
                {
                    filterStr += count.Equals(0) ? string.Format("{0} {1}", entry.Key, entry.Value) : string.Format(" AND {0} {1}", entry.Key, entry.Value);
                    count++;
                }
                query = string.Format("DELETE FROM {0} WHERE {1}", tableName, filterStr);
                sqlCmd = new SqlCommand(query, getConnection(dbName));
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { sqlCmd = null; query = null; filterStr = null; }
        }

        internal static void DeleteByPkId(string dbName, string tableName, decimal pkId)
        {
            SqlCommand sqlCmd = null;
            string query = null;
            string filterStr = null;
            try
            {
                filterStr = string.Format("PKID = {0}", pkId);
                query = string.Format("DELETE FROM {0} WHERE {1}", tableName, filterStr);
                sqlCmd = new SqlCommand(query, getConnection(dbName));
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { sqlCmd = null; query = null; filterStr = null; }
        }

        internal static object GetFunction(string dbName, string functionName)
        {
            SqlCommand sqlCmd = null;
            try
            {
                sqlCmd = new SqlCommand(string.Format("SELECT dbo.{0}()", functionName), getConnection(dbName));
                return sqlCmd.ExecuteScalar();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { sqlCmd = null; }
        }

        internal static object GetFunction(string dbName, string functionName, List<SqlParameter> parameters)
        {
            SqlCommand sqlCmd = null;
            string query = null;
            try
            {
                query = string.Format("SELECT dbo.{0}(", functionName);
                foreach (SqlParameter param in parameters)
                    query += param.ParameterName;
                query += ")";

                sqlCmd = new SqlCommand(query, getConnection(dbName));
                sqlCmd.Parameters.AddRange(parameters.ToArray());
                return sqlCmd.ExecuteScalar();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { sqlCmd = null; query = null; }
        }

        internal static DataTable GetTable(string dbName, string tableName, Dictionary<string, string> filter)
        {
            string query = null;
            int count = 0;
            try
            {
                query = string.Format("SELECT * FROM {0}", tableName);

                if (filter == null || filter.Count.Equals(0))
                    goto Outer;

                foreach (KeyValuePair<string, string> entry in filter)
                {
                    if (count.Equals(0)) query += string.Format(" WHERE {0} {1}", entry.Key, entry.Value);
                    else query += string.Format(" AND {0} {1}", entry.Key, entry.Value);
                    count++;
                }

            Outer:
                return GetByQuery(dbName, query).Tables[0];
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { query = null; }
        }

        internal static DataTable GetTable(string dbName, string tableName, Dictionary<string, string> filter, string condition)
        {
            string query = null;
            int count = 0;
            try
            {
                query = string.Format("SELECT * FROM {0}", tableName);

                if (filter == null || filter.Count.Equals(0))
                    goto Outer;

                foreach (KeyValuePair<string, string> entry in filter)
                {
                    if (count.Equals(0)) query += string.Format(" WHERE {0} {1}", entry.Key, entry.Value);
                    else query += string.Format(" AND {0} {1}", entry.Key, entry.Value);
                    count++;
                }
                query += " " + condition;
            Outer:
                return GetByQuery(dbName, query).Tables[0];
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { query = null; }
        }

        internal static DataTable GetTable(string dbName, string tableName, List<string> columns, Dictionary<string, string> filter)
        {
            string query = null;
            int count = 0;
            try
            {
                query = "SELECT ";
                if (columns == null || columns.Count.Equals(0))
                    return null;
                foreach (string column in columns)
                {
                    if (count.Equals(0)) query += column;
                    else query += ", " + column;
                    count++;
                }
                count = 0;

                query += string.Format(" FROM {0}", tableName);

                if (filter == null || filter.Count.Equals(0))
                    goto Outer;

                foreach (KeyValuePair<string, string> entry in filter)
                {
                    if(count.Equals(0)) query += string.Format(" WHERE {0} {1}", entry.Key, entry.Value);
                    else query += string.Format(" AND {0} {1}", entry.Key, entry.Value);
                    count++;
                }
                count = 0;

            Outer:
                return GetByQuery(dbName, query).Tables[0];
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { query = null; }
        }

        internal static void GetStoredProcedure(string dbName, string storedProcedureName, Dictionary<string, object> parameters)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = SqlConnector.getConnection(dbName);
                cmd = new SqlCommand(storedProcedureName, conn);
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> entry in parameters)
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
                cmd.ExecuteNonQuery();
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { conn = null; cmd = null; }
        }

        internal static DataSet GetStoredProcedure(string dbName, string storedProcedureName, Dictionary<string, object> parameters, List<string> tableNames)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            try
            {
                conn = SqlConnector.getConnection(dbName);
                cmd = new SqlCommand(storedProcedureName, conn);
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> entry in parameters)
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
                sqlDataAdapter = new SqlDataAdapter(cmd);
                ds = new DataSet();

                if (tableNames == null || tableNames.Count.Equals(0))
                    sqlDataAdapter.Fill(ds);
                else
                {
                    foreach (string tableName in tableNames)
                        sqlDataAdapter.Fill(ds, tableName);
                }
                return ds;
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { conn = null; cmd = null; ds = null; sqlDataAdapter = null; }
        }

        internal static List<object> GetStoredProcedureByOutputParameter(string dbName, string storedProcedureName, List<SqlParameter> parameters)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            List<object> returnValues = null;
            try
            {
                conn = SqlConnector.getConnection(dbName);
                cmd = new SqlCommand(storedProcedureName, conn);
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters == null)
                    throw new MofException("Ямар ч утга дамжуулаагүй байна! Утга дамжуулна уу. Please set parameters");
                if (parameters.Count.Equals(0))
                    throw new MofException("Ямар ч утга дамжуулаагүй байна! Утга дамжуулна уу. Please set parameters");

                foreach (SqlParameter param in parameters)
                    cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                returnValues = new List<object>();

                foreach (SqlParameter param in parameters.Where(t => t.Direction.Equals(ParameterDirection.Output) || t.Direction.Equals(ParameterDirection.InputOutput)).ToList())
                    returnValues.Add(cmd.Parameters[param.ParameterName].Value);

                return returnValues;
            }
            catch (MofException ex) { throw ex; }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа: {1}", serverIpAddress, ex.Message));
                throw new MofException(string.Format("{0} сервертэй холбогдсон боловч хандах үед алдаа гарлаа!", serverIpAddress), ex);
            }
            finally { conn = null; cmd = null; returnValues = null; }
        }
    }
}