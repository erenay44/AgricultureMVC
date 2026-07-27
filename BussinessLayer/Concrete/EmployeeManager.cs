using BussinessLayer.Abstract;
using DataLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class EmployeeManager:IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public void Delete(Employee t)
        {
           _employeeDal.Delete(t);
        }

        public Employee GetById(int id)
        {
            return _employeeDal.GetById(id);
        }

        public List<Employee> GetListAll()
        {
            return _employeeDal.GetListAll();
        }

        public void Insert(Employee t)
        {
            _employeeDal.Insert(t);
        }

        public void Update(Employee t)
        {
            _employeeDal.Update(t);
        }
    }
}
