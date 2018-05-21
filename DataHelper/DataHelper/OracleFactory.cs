
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
using NLogManager;
using System.Diagnostics;
namespace DatabaseLib
{   
    public class OracleFactory : IDataFactory,IDisposable
    {
        private OracleConnection OraConn = null;

        public OracleFactory()
        {
            OraConn = new OracleConnection(DataHelper.ConnectString);
            try
            {
                OraConn.Open();
            }
            catch (Exception ex)
            {
                Log.AddErrorLog(ex.Message);
            }
        }


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

        /// <summary>
        /// 测试通过  2018-5-4 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string SQL)
        {
            string error = "";
            DataSet ds = new DataSet();
            try
            {
                if(OraConn.State != ConnectionState.Open)
                    OraConn.Open();
                OracleDataAdapter OraDa = new OracleDataAdapter(SQL, OraConn);
                OraDa.Fill(ds);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.AddErrorLog(error);
            }
           
            return ds;

        }

        public DataSet ExecuteDataset(string SQL, string TableName)
        {
            string error = "";
            DataSet ds = new DataSet();
            try
            {
                if (OraConn.State != ConnectionState.Open)
                    OraConn.Open();
                OracleDataAdapter command = new OracleDataAdapter(SQL, OraConn);
                command.Fill(ds,TableName);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.AddErrorLog(error);
            }
            return ds;
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
            try
            {
                if (OraConn.State != ConnectionState.Open)
                    OraConn.Open();
                OracleCommand comm = new OracleCommand(sSQL, OraConn);
                reader = comm.ExecuteReader();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.AddErrorLog(error);
            }
            return reader;
        }

        public object ExecuteScalar(string sSQL)
        {
            string error = "";
            
            object ret = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                if (OraConn.State != ConnectionState.Open)
                    OraConn.Open();
                OracleCommand comm = new OracleCommand(sSQL, OraConn);
                ret = comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.AddErrorLog(error);
            }
            return ret;
            
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
        public void Dispose()
        {
            if (OraConn != null)
            {
                if (OraConn.State != ConnectionState.Closed)
                    OraConn.Close();
                OraConn = null;
            }
        }
    }

}