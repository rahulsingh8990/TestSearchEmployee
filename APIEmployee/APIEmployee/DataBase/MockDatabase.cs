using APIEmployee.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace APIEmployee.DataBase
{
    public interface IMainModuleContext
    {
        IDbSet<List<EmployeeDetails>> employee { get; set; } // My collections...
    }
    public partial class MainModuleContext : DbContext, IMainModuleContext
    {
        public IDbSet<List<EmployeeDetails>> employee { get; set; }
        public MainModuleContext() : base() { }

        // Other methods
    }

    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private HashSet<T> _data;

        public FakeDbSet()
        {
            _data = new HashSet<T>();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public void Detach(T item)
        {
            _data.Remove(item);
        }

        Type IQueryable.ElementType
        {
            get { return _data.AsQueryable().ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _data.AsQueryable().Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public ObservableCollection<T> Local
        {
            get
            {
                return new ObservableCollection<T>(_data);
            }
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }

    public partial class FakeMainModuleContext : IMainModuleContext
    {
        public IDbSet<List<EmployeeDetails>> employee { get; set; }


        public void SaveChanges()
        {
            // do nothing (probably set a variable as saved for testing)
        }

        public FakeMainModuleContext()
        {

            employee = new FakeDbSet<List<EmployeeDetails>>()
            {
                new List<EmployeeDetails>()
                {
                    new EmployeeDetails()
                    {
                        Sno =1,
                        Age=24,
                        EmploymentStartDate=DateTime.Now.AddYears(-10),
                        EmploymentEndDate=DateTime.Now,
                        JobTitle ="software Developer",
                        Name = "Lalit Rana"

                    },
                    new EmployeeDetails()
                    {
                         Sno =2,
                        Age=26,
                        EmploymentStartDate=DateTime.Now.AddYears(-5),
                        EmploymentEndDate=DateTime.Now.AddYears(-4),
                        JobTitle ="Senior software Developer",
                        Name = "Rahul Rana"
                    },
                    new EmployeeDetails()
                    {
                         Sno =3,
                        Age=26,
                        EmploymentStartDate=DateTime.Now.AddYears(15),
                        EmploymentEndDate=DateTime.Now.AddYears(-4),
                        JobTitle ="Team Lead",
                        Name = "Akshay Anand"
                    },
                    new EmployeeDetails()
                    {

                         Sno =4,
                        Age=54,
                        EmploymentStartDate=DateTime.Now.AddYears(15),
                        EmploymentEndDate=DateTime.Now.AddYears(-4),
                        JobTitle ="Manager",
                        Name = "Sonam Gupta"
                    },
                    new EmployeeDetails()
                    {
                        Sno =5,
                        Age=12,
                        EmploymentStartDate=DateTime.Now.AddYears(15),
                        EmploymentEndDate=DateTime.Now.AddYears(-4),
                        JobTitle ="Manager",
                        Name = "Charu Chabra"
                    }
                }
            };

        }
        
      
    }
}