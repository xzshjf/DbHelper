using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
using NLogManager;
namespace ConsoleTest
{
    class Program
    {
         
        static void Main(string[] args)
        {
            //long ss = 0;
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //string sSQL = "SELECT LASTUPTIME0,LASTUPTIME12,LASTUPTIME11 FROM L2ADMIN.BA_PCMESS WHERE PC_TYPE = 100";
            //OracleDataReader dr = (OracleDataReader)DataHelper.Instance.ExecuteReader(sSQL);
            //Log.AddErrorLog("系统错误!");
            List<Person> lists = new List<Person>();
            Random r = new Random();
            //添加数据
            lists.Add(new Person("5", "dengjie",40));
            lists.Add(new Person("7", "qiangyabo", 36));
            lists.Add(new Person("2", "zhangdong", 30));
            lists.Add(new Person("1", "hanjunfeng", 45));
            lists.Add(new Person("5", "jiatao", 28));
            lists.Add(new Person("5", "jiatao", 8));

            Console.WriteLine("排序前：");
            foreach (var p in lists)
            {
                Console.WriteLine(p);
            }
            
            lists.Sort();//排序
            Console.WriteLine("排序后：");
            foreach (var p in lists)
            {
                Console.WriteLine(p);
            }

            Console.ReadLine();
        }
    }
    
    public class Person : IComparable<Person>
    {
        private string _id;
        private string _name;
        private int _age;

        public Person()
        {

        }
        public Person(string id, string name, int age)
        {
            this._id = id;
            this._name = name;
            this._age = age;
        }


        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }

        public int CompareTo(Person other)
        {
            if(0==this.Id.CompareTo(other.Id))
              return this.Age.CompareTo(other.Age);
            else
            {
                return this.Id.CompareTo(other.Id);
            }
        }

        public override string ToString()
        {
            return "Id:" + _id + ",Name:" + _name + ",Age:" + _age.ToString();
        }
    }

    //public class Person : IComparer<Person>
    //{
    //    private string _id;
    //    private string _name;
    //    private int _age;

    //    public Person(string id, string name, int age)
    //    {
    //        this._id = id;
    //        this._name = name;
    //        this._age = age;
    //    }


    //    public string Id
    //    {
    //        get
    //        {
    //            return _id;
    //        }
    //        set
    //        {
    //            _id = value;
    //        }
    //    }
    //    public string Name
    //    {
    //        get
    //        {
    //            return _name;
    //        }
    //        set
    //        {
    //            _name = value;
    //        }
    //    }
    //    public int Age
    //    {
    //        get
    //        {
    //            return _age;
    //        }
    //        set
    //        {
    //            _age = value;
    //        }
    //    }

    //    public int Compare(Person x, Person y)
    //    {
    //        return x.Id.CompareTo(y.Id);
    //    }

    //    public override string ToString()
    //    {
    //        return "Id:" + _id + ",Name:" + _name + ",Age:" + _age.ToString();
    //    }


    //}
}
