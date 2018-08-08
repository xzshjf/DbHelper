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
            TestExecuteScalar();

            Console.ReadLine();
        }

        public static void TestExecuteScalar()
        {
            object obj = DataHelper.Instance.ExecuteScalar("SELECT ORD_HEAT_NO, ORD_TREATNO FROM L2ADMIN.PL_HEATPLAN WHERE ORD_HEAT_NO = ''");

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
