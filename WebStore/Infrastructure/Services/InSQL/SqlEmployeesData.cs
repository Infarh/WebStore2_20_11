using System;
using System.Collections.Generic;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebStoreDB _db;

        public SqlEmployeesData(WebStoreDB db) => _db = db;

        public IEnumerable<Employee> Get() => _db.Employees;

        public Employee GetById(int id) => _db.Employees.Find(id);

        public int Add(Employee employee)
        {
            if(employee is null) throw new ArgumentNullException(nameof(employee));

            _db.Add(employee);
            //_db.Employees.Add(employee);

            return employee.Id;
        }

        public void Edit(Employee employee)
        {
            if(employee is null) throw new ArgumentNullException(nameof(employee));

            _db.Update(employee);
            //_db.Employees.Update(employee);
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
                return false;
            _db.Remove(employee);
            //_db.Employees.Remove(employee);
            return true;
        }

        public void SaveChanges() => _db.SaveChanges();
    }
}
