using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib;
using System.Data;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string sSQL = "SELECT USER_ID, USER_PWD, USER_NAME,USER_DEPT, USER_TYPE, USER_PARENTID,USER_COMMENT, USER_GROUP FROM C##RHL2.BA_USER WHERE USER_TYPE =10";
            DataSet ds = DataHelper.Instance.ExecuteDataset(sSQL);
        }
    }
}
