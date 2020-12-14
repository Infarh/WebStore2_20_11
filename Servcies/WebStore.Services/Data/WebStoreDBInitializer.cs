using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly ILogger<WebStoreDBInitializer> _Logger;

        public WebStoreDBInitializer(
            WebStoreDB db,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<WebStoreDBInitializer> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _Logger = Logger;
        }

        public void Initialize()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация БД...");

            var db = _db.Database;

            //if(db.EnsureDeleted())
            //    if(!db.EnsureCreated())
            //        throw new InvalidOperationException("Ошибка при создании БД");

            if (db.GetPendingMigrations().Any())
            {
                _Logger.LogInformation("Миграция БД...");
                db.Migrate();
                _Logger.LogInformation("Миграция БД выполнена успешно {0}мс", timer.ElapsedMilliseconds);
            }
            else
                _Logger.LogInformation("Миграция БД не требуется");

            InitializeProducts();
            InitializeEmployees();
            InitializeIdentityAsync().Wait();

            _Logger.LogInformation("Инициализация БД выполнена успешно {0:0.###}c", timer.Elapsed.TotalSeconds);
        }

        private void InitializeProducts()
        {
            var timer = Stopwatch.StartNew();

            _Logger.LogInformation("Инициализация каталога товаров...");
            if (_db.Products.Any())
            {
                _Logger.LogInformation("Инициализация каталога товаров не требуется");
                return;
            }

            var db = _db.Database;
            using (db.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSections] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSections] OFF");

                db.CommitTransaction();
            }
            _Logger.LogInformation("Инициализация категорий выполнена {0} мс", timer.ElapsedMilliseconds);

            using (db.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrands] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrands] OFF");

                db.CommitTransaction();
            }
            _Logger.LogInformation("Инициализация брендов выполнена {0} мс", timer.ElapsedMilliseconds);

            using (db.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                db.CommitTransaction();
            }
            _Logger.LogInformation("Инициализация товаров выполнена {0} мс", timer.ElapsedMilliseconds);
        }

        private void InitializeEmployees()
        {
            if (_db.Employees.Any()) return;

            using (_db.Database.BeginTransaction())
            {
                TestData.Employees.ForEach(employee => employee.Id = 0);

                _db.Employees.AddRange(TestData.Employees);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }
        }

        private async Task InitializeIdentityAsync()
        {
            _Logger.LogInformation("Инициализация Identity...");
            var timer = Stopwatch.StartNew();

            async Task CheckRoleExist(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    _Logger.LogInformation("Добавление роли {0} {1} мс", RoleName, timer.ElapsedMilliseconds);
                    await _RoleManager.CreateAsync(new Role { Name = RoleName });
                }
            }

            await CheckRoleExist(Role.Administrator);
            await CheckRoleExist(Role.User);

            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                _Logger.LogInformation("Добавление администратора...");
                var admin = new User { UserName = User.Administrator };
                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _Logger.LogInformation("Добавление администратора выполнено успешно");
                    var role_arr_result = await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                    if (role_arr_result.Succeeded)
                        _Logger.LogInformation("Добавление администратору роли Администратор выполнено успешно");
                    else
                    {
                        var error = string.Join(",", role_arr_result.Errors.Select(error => error.Description));
                        _Logger.LogError("Ошибка при добавлении роли Администратор администратору {0}", error);
                        throw new InvalidOperationException($"Ошибка при добавлении пользователю Администратор роли Администратор: {error}");
                    }
                }
                else
                {
                    var error = string.Join(", ", creation_result.Errors.Select(e => e.Description));
                    _Logger.LogError("Ошибка при Администратора {0}", error);
                    throw new InvalidOperationException($"Ошибка при создании пользователя Администратор: {error}");
                }
            }
        }
    }
}
