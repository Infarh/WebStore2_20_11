using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>API управления сотрудниками</summary>
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        /// <summary>Получение списка всех сотрудников</summary>
        /// <returns>Список всех сотрудников</returns>
        [HttpGet]
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        /// <summary>Получение сотрудника по его идентификатору</summary>
        /// <param name="id">Идентификатор интересующего сотрудника</param>
        /// <returns>Сотрудник с указанным идентификатором</returns>
        [HttpGet("{id}")]
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        /// <summary>Добавление нового сотрудника</summary>
        /// <param name="employee">Новый сотрудник</param>
        /// <returns>Идентификатор, присвоенный сотруднику</returns>
        [HttpPost]
        public int Add([FromBody] Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }

        /// <summary>Редактирование сотрудника</summary>
        /// <param name="employee">Редактируемый сотрудник</param>
        [HttpPut /*("{id}")*/]
        public void Edit( /*int id, */ Employee employee)
        {
            _EmployeesData.Edit(employee);
            SaveChanges();
        }

        /// <summary>Удаление сотрудника по указанному идентификатору</summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        /// <returns>Истина, если сотрудник был удалён</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            SaveChanges();
            return result;
        }

        /// <summary>Сохранение изменений</summary>
        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }
}
