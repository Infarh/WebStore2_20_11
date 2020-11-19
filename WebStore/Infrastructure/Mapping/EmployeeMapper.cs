using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeesViewModel ToView(this Employee employee) => new EmployeesViewModel
        {
            Id = employee.Id,
            FirstName = employee.Name,
            LastName = employee.Surname,
            Patronymic = employee.Patronymic,
            Age = employee.Age,
            EmployementDate = employee.EmployementDate
        };

        public static IEnumerable<EmployeesViewModel> ToView(this IEnumerable<Employee> employees) => employees.Select(ToView);

        public static Employee FromView(this EmployeesViewModel Model) => new Employee
        {
            Id = Model.Id,
            Surname = Model.LastName,
            Name = Model.FirstName,
            Patronymic = Model.Patronymic,
            Age = Model.Age,
            EmployementDate = Model.EmployementDate
        };
    }
}
