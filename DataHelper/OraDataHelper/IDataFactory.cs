using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace OraDataHelper
{
    /**
     * 通用数据库接口
     * 设计者：邓杰
     * QQ:382992838@QQ.COM
    */
    public interface IDataFactory
    {

        bool ConnectionTest();
        int ExecuteNonQuerySql(string sqlstr, out string error);
        DataSet ExecuteQueryDataSet(string sqlstr, out string error);
        DataTable ExecuteDataTable(string sqlstr);
        DbDataReader ExecuteQueryReader(string sqlstr, out string error);
        object ExecuteQueryScalar(string sqlstr, out string error);
        void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, int size, object value);
        int ExecuteCommand(DbCommand cmd,out string error);
        int ExecuteCommandParameter(string SqlStr, Dictionary<DbParameter, object> parameters, out string error);

        int ExecuteStoredProcedure(string proName, params DbParameter[] paraName);
        bool ExecuteStoredProcedure(string proName);
    }

    public class DBCreator
    {
      
        private DbProviderFactory dbproviderfactory = null;

        public DBCreator()
        {
            dbproviderfactory =CreateDbProviderFactory();
        }

        public DbProviderFactory GetDbProviderFactory
        {
            get
            {
                return this.dbproviderfactory;
            }
        }

        public DbProviderFactory CreateDbProviderFactory()
        {
           
            DbProviderFactory f = null;
            DataRow[] dr;
            try
            {
                switch (SysConfig.m_dbMode.ToUpper())
                {
                    case "MSSQL":
                        dr = DbProviderFactories.GetFactoryClasses().Select("InvariantName='" + SysConfig.m_dbType + "'");
                        f = DbProviderFactories.GetFactory(dr[0]);
                        break;
                    case "ORACLE":
                        dr = DbProviderFactories.GetFactoryClasses().Select("InvariantName='"+ SysConfig.m_dbType +"'");
                        f = DbProviderFactories.GetFactory(dr[0]);
                        break;
                }
                
            }
            catch (Exception ex)
            {
                Log.AddErrorLog(ex.Message);
            }
            return f;

        }
    }
}