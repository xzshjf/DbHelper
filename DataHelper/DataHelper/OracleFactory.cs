
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace DatabaseLib
{   
    public class OracleFactory : IDataFactory
    {
        public bool BulkCopy(IDataReader reader, string tableName, string command = null, SqlBulkCopyOptions options = SqlBulkCopyOptions.Default)
        {
            throw new NotImplementedException();
        }

        public void CallException(string message)
        {
            DataHelper.AddErrorLog(new Exception(message));
        }

        public bool ConnectionTest()
        {
            using (SqlConnection m_Conn = new SqlConnection(DataHelper.ConnectString))
            {
                try
                {
                    m_Conn.Open();
                    if (m_Conn.State == ConnectionState.Open)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    CallException(e.Message);
                }
            }
            return false;
        }

        public DbParameter CreateParam(string paramName, DbType dbType, object objValue, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            throw new NotImplementedException();
        }

        public DataRow ExecuteDataRowProcedure(string ProName, params DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public DataRowView ExecuteDataRowViewProcedure(string ProName, params DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataset(string SQL)
        {
            string error = "";
            using (OracleConnection conn = new OracleConnection(DataHelper.ConnectString))
            {
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQL, conn);
                    command.Fill(ds);
                }
                catch (OracleException ex)
                {
                    error = ex.Message; ds = null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                return ds;
            }
        }

        public DataSet ExecuteDataset(string SQL, string TableName)
        {
            string error = "";
            using (OracleConnection conn = new OracleConnection(DataHelper.ConnectString))
            {
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQL, conn);
                    command.Fill(ds, TableName);
                }
                catch (OracleException ex)
                {
                    error = ex.Message; ds = null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                return ds;
            }
        }

        public DataSet ExecuteDataset(string[] SQLs, string[] TableNames)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSetProcedure(string ProName, params DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSetProcedure(string ProName, ref int returnValue, params DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(string SQL)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTableProcedure(string ProName, params DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTableProcedure(string ProName, ref int returnValue, DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string SQL)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string[] SQLs)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string[] SQLs, object[][] Pars)
        {
            throw new NotImplementedException();
        }

        public DbDataReader ExecuteProcedureReader(string sSQL, params DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public DbDataReader ExecuteReader(string sSQL)
        {
            string error = "";
            OracleDataReader reader = null;
            OracleConnection conn = new OracleConnection(DataHelper.ConnectString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OracleCommand comm = new OracleCommand(sSQL, conn);
                reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (OracleException ex)
            {
                error = ex.Message; reader = null;
            }

            return reader;
        }

        public object ExecuteScalar(string sSQL)
        {
            string error = "";
            using (OracleConnection conn = new OracleConnection(DataHelper.ConnectString))
            {
                object ret = null;
                OracleCommand cmd = new OracleCommand();
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = sSQL;
                    ret = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    error = ex.Message; ret = null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
                return ret;
            }
        }

        public bool ExecuteStoredProcedure(string ProName)
        {
            throw new NotImplementedException();
        }

        public int ExecuteStoredProcedure(string ProName, params DbParameter[] ParaName)
        {
            throw new NotImplementedException();
        }

        public void FillDataSet(ref DataSet ds, string SQL, string TableName)
        {
            throw new NotImplementedException();
        }
    }

}