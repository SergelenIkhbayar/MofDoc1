using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using MofDoc.Enum;

namespace MofDoc.Class
{
    public class OracleConnector
    {
        private static OracleConnection orConn = null;

        private static void getConnection()
        {
            try
            {
                if (orConn == null || orConn.State == System.Data.ConnectionState.Closed)
                {
                    orConn = new OracleConnection("Data Source=10.11.11.42:1521/cmcdbbt;User ID=hr_mof;Password=cmcocprab;");
                    orConn.Open();
                }
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC системийн сервертэй холбогдож чадсангүй: ", ex.Message);
                throw new MofException("CMC системийн сервертэй холбогдож чадсангүй!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC системийн сервертэй холбогдож чадсангүй: ", ex.Message);
                throw new MofException("CMC системийн сервертэй холбогдож чадсангүй!", ex);
            }
        }

        internal static DataSet GetByQuery(string query)
        {
            OracleCommand orCmd = null;
            OracleDataAdapter orDataAdapter = null;
            DataSet ds = null;
            try
            {
                getConnection();
                orCmd = new OracleCommand(query, orConn);
                orCmd.CommandType = CommandType.Text;

                orDataAdapter = new OracleDataAdapter(orCmd);

                ds = new DataSet();
                orDataAdapter.Fill(ds);
                return ds;
            }
            catch (MofException ex) { throw ex; }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа: " + ex.Message);
                throw new MofException("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа: " + ex.Message);
                throw new MofException("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа", ex);
            }
            finally
            {
                orCmd = null;
                orDataAdapter = null;
                ds = null;
            }
        }

        internal static object GetObjectByQuery(string query)
        {
            OracleCommand orCmd = null;
            OracleDataAdapter orDataAdapter = null;
            DataSet ds = null;
            try
            {
                getConnection();
                orCmd = new OracleCommand(query, orConn);
                orCmd.CommandType = CommandType.Text;

                orDataAdapter = new OracleDataAdapter(orCmd);

                ds = new DataSet();
                orDataAdapter.Fill(ds);
                return ds.Tables[0].Rows[0].ItemArray[0];
            }
            catch (MofException ex) { throw ex; }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа: " + ex.Message);
                throw new MofException("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа: " + ex.Message);
                throw new MofException("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа", ex);
            }
            finally
            {
                orCmd = null;
                orDataAdapter = null;
                ds = null;
            }
        }

        internal static DataSet GetByProcedure(string procedureName,List<OracleParameter> inputParameter, List<OracleParameter> outputParameter, List<string> tableNames)
        {
            OracleCommand orCmd = null;
            OracleDataAdapter orDataAdapter = null;
            DataSet ds = null;
            try
            {
                getConnection();
                orCmd = orConn.CreateCommand();
                orCmd.CommandText = procedureName;
                orCmd.CommandType = CommandType.StoredProcedure;
                foreach (OracleParameter parameter in inputParameter) { orCmd.Parameters.Add(parameter); }
                foreach (OracleParameter parameter in outputParameter) { orCmd.Parameters.Add(parameter); }
                orCmd.ExecuteNonQuery();

                orDataAdapter = new OracleDataAdapter(orCmd);
                ds = new DataSet();

                int count = inputParameter.Count;
                if (tableNames == null || tableNames.Count.Equals(0))
                    orDataAdapter.Fill(ds);
                else
                {
                    foreach (string tableName in tableNames)
                    {
                        orDataAdapter.Fill(ds, tableName, (OracleRefCursor)orCmd.Parameters[count].Value);
                        count++;
                    }
                }
                return ds;
            }
            catch (MofException ex) { throw ex; }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа: " + ex.Message);
                throw new MofException("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа: " + ex.Message);
                throw new MofException("CMC(10.11.11.42) системтэй холбогдсон боловч хандах үед алдаа гарлаа", ex);
            }
            finally
            {
                orCmd = null;
                orDataAdapter = null;
                ds = null;
            }
        }
    }
}