
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

namespace OraDataHelper
{
    /**通用数据库执行类
     *执行条件：目前只测试通过Oracle,Sql Server
     * 设计者：邓杰
     * QQ:382992838@QQ.COM
    */

    public class DbCreator : DBCreator, IDataFactory,IDisposable
    {
        private DbConnection conn = null;

        public DbCommand cmd = null;

        public DbParameter para = null;

        public DbCreator():base()
        {
            conn = GetDbProviderFactory.CreateConnection();
            conn.ConnectionString = SysConfig.ConnectString;

            cmd = GetDbProviderFactory.CreateCommand();
            cmd.Connection = conn;

            para = GetDbProviderFactory.CreateParameter();
        }

        ~DbCreator()
        {
            this.Dispose();
        }

        

        /// <summary>
        /// 测试连接数据库  OK
        /// </summary>
        /// <returns></returns>
        public bool ConnectionTest()
        {
            
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.AddErrorLog(e.Message);
            }
            
            return false;
        }

        /// <summary>
        /// 执行insert,update,delete  OK
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int ExecuteCommand(DbCommand cmd, out string error)
        {
            error = "";
            int ret = 0;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.AddErrorLog(ex.Message); error = ex.Message; return -10;
            }

            return ret;
        }

        /// <summary>
        /// 执行insert,update,delete OK
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="parameters"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int ExecuteCommandParameter(string sqlstr, Dictionary<DbParameter, object> parameters, out string error)
        {
            error = "";
            int ret = 0;
            
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                DbCommand cmd = GetDbProviderFactory.CreateCommand(); 
                cmd.CommandText =sqlstr;
                cmd.Connection = conn;

                DbParameter[] pars = parameters.Keys.ToArray();
                object[] objs = parameters.Values.ToArray();
                for (int i = 0; i < pars.Length; i++)
                {
                    AddInParameter(cmd, pars[i].ParameterName, pars[i].DbType, pars[i].Size, objs[i]);
                }
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.AddErrorLog(ex.Message); error = ex.Message; ret = -10;
            }
            return ret;
            
        }

        /// <summary>
        /// 增加dbparamenter参数 OK
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, int size, object value)
        {
            DbParameter dbParameter = (DbParameter)cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(dbParameter);
        }
        /// <summary>
        /// 执行select OK
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sqlstr)
        {
            DataTable dt = new DataTable();
            
            try
            {
                DbDataAdapter da = GetDbProviderFactory.CreateDataAdapter();   
                DbCommand cmd = GetDbProviderFactory.CreateCommand(); 
                cmd.CommandText = sqlstr;
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Log.AddErrorLog(ex.Message);
            }
            
            return dt;
        }

        /// <summary>
        /// 执行insert,update,delete  OK
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int ExecuteNonQuerySql(string sqlstr, out string error)
        {
            error = "";
            int ret = 0;
            DbCommand cmd = GetDbProviderFactory.CreateCommand();
            cmd.CommandText = sqlstr;
            cmd.Connection = conn;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlstr;
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.AddErrorLog(ex.Message); ret = -10; error = ex.Message;
            }

            return ret;
            
        }

        /// <summary>
        /// 执行select OK
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public DataSet ExecuteQueryDataSet(string sqlstr, out string error)
        {
            error = "";
            DataSet ds = new DataSet();
           
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                DbDataAdapter dap = GetDbProviderFactory.CreateDataAdapter();
                dap.SelectCommand = GetDbProviderFactory.CreateCommand();
                dap.SelectCommand.CommandText = sqlstr;
                dap.SelectCommand.Connection = conn;
                dap.Fill(ds);
            }
            catch (OracleException ex)
            {
                Log.AddErrorLog(ex.Message); error = ex.Message;
            }

            return ds;
            
        }

        /// <summary>
        /// 执行select语句  OK
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public DbDataReader ExecuteQueryReader(string sqlstr, out string error)
        {
            error = "";
            DbDataReader reader = null;
            
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                DbCommand cmd = GetDbProviderFactory.CreateCommand();
                cmd.CommandText = sqlstr;
                cmd.Connection = conn;
                reader = cmd.ExecuteReader();
            }
            catch (OracleException ex)
            {
                Log.AddErrorLog(ex.Message); error = ex.Message;
            }
            
            return reader;
            
        }
        /// <summary>
        /// 执行select OK
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public object ExecuteQueryScalar(string sqlstr, out string error)
        {
            error = "";
            object ret = null;
            

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                //OracleCommand cmd = new OracleCommand(sqlstr, conn);
                DbCommand cmd = GetDbProviderFactory.CreateCommand();
                cmd.CommandText = sqlstr;
                cmd.Connection = conn;
                ret = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Log.AddErrorLog(ex.Message); error = ex.Message;
            }

            return ret;
            
        }

        /// <summary>
        /// 未测试
        /// </summary>
        /// <param name="proName"></param>
        /// <returns></returns>
        public bool ExecuteStoredProcedure(string proName)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //OracleCommand cmd = new OracleCommand(proName, conn);
                DbCommand cmd = GetDbProviderFactory.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Log.AddErrorLog(e.Message);
                return false;
            }
   
        }

        /// <summary>
        /// 未测试
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="paraName"></param>
        /// <returns></returns>
        public int ExecuteStoredProcedure(string proName, params DbParameter[] paraName)
        {
            
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                
                DbCommand cmd = GetDbProviderFactory.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                if (paraName != null)
                {
                    cmd.Parameters.AddRange(paraName);
                }
                DbParameter param = GetDbProviderFactory.CreateParameter(); //new DbParameter();
                cmd.Parameters.Add(param);
                param.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();
                return (int)param.Value;
            }
            catch (Exception e)
            {
                Log.AddErrorLog(e.Message);
                return -1;
            }

            
        }
        public void Dispose()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }

}