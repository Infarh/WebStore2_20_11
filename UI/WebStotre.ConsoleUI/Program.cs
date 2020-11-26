using System;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Employees;

namespace WebStotre.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            Console.WriteLine("Для запроса нажмите Enter");
            Console.ReadLine();

            var employees_client = new EmployeesClient(configuration);

            var employees = employees_client.Get();

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Surname} {employee.Name} {employee.Patronymic}");
            }

            Console.ReadLine();
        }
    }
}
