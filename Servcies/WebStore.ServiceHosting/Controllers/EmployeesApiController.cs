using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpGet]
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")]
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        [HttpPost]
        public int Add([FromBody] Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }

        [HttpPut /*("{id}")*/]
        public void Edit( /*int id, */ Employee employee)
        {
            _EmployeesData.Edit(employee);
            SaveChanges();
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            SaveChanges();
            return result;
        }

        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }
}
