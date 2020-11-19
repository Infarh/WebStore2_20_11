using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _Employees = TestData.Employees;

        public IEnumerable<Employee> Get() => _Employees;

        public Employee GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);

        public int Add(Employee employee)
        {
            if(employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return employee.Id;

            employee.Id = _Employees.Count == 0 ? 1 : _Employees.Max(e => e.Id) + 1;
            _Employees.Add(employee);
            return employee.Id;
        }

        public void Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if(_Employees.Contains(employee)) return;

            var db_employee = GetById(employee.Id);
            if(db_employee is null) return;

            db_employee.Name = employee.Name;
            db_employee.Surname = employee.Surname;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
        }

        public bool Delete(int id) => _Employees.RemoveAll(e => e.Id == id) > 0;

        public void SaveChanges() { }
    }
}
