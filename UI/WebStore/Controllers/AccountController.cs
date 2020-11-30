using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Процесс регистрации нового пользвоателя

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model/*, [FromServices] IMapper Mapper*/)
        {
            if (!ModelState.IsValid) return View(Model);

            //var new_user = Mapper.Map<User>(Model);

            using (_Logger.BeginScope("Регистрация нового пользователя {0}", Model.UserName))
            {
                _Logger.LogInformation("Регистрация нового пользователя {0}", Model.UserName);

                var user = new User
                {
                    UserName = Model.UserName
                };

                var registration_result = await _UserManager.CreateAsync(user, Model.Password);
                if (registration_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", Model.UserName);

                    await _UserManager.AddToRoleAsync(user, Role.User);
                    _Logger.LogInformation("Пользователю {0} назначена роль {1}", Model.UserName, Role.User);

                    await _SignInManager.SignInAsync(user, isPersistent: false);
                    _Logger.LogInformation("Пользователь {0} автоматически вошёл в систему в первый раз", Model.UserName);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in registration_result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                _Logger.LogWarning("Ошибка при регистрации нового пользователя {0} {1}",
                    Model.UserName,
                    string.Join(",", registration_result.Errors.Select(error => error.Description)));
            }

            return View(Model);
        }

        #endregion

        #region Процесс входа пользователя в систему

        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            using (_Logger.BeginScope("Вход пользователя {0} в систему", Model.UserName))
            {
                var login_result = await _SignInManager.PasswordSignInAsync(
                    Model.UserName,
                    Model.Password,
                    Model.RememberMe,
                    lockoutOnFailure: false);

                if (login_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь успешно вошёл в систему");

                    if (Url.IsLocalUrl(Model.ReturnUrl))
                    {
                        _Logger.LogInformation("Перенаправляю вошедшего пользователя {0} на адрес {1}",
                            Model.UserName, Model.ReturnUrl);
                        return Redirect(Model.ReturnUrl);
                    }
                    _Logger.LogInformation("Перенаправляю вошедшего пользователя {0} на главную страницу", Model.UserName);
                    return RedirectToAction("Index", "Home");
                }

                _Logger.LogWarning("Ошибка ввода пароля при входе пользователя {0} в систему", Model.UserName);

                ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль!");
            }

            return View(Model);
        }

        #endregion

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity!.Name;
            await _SignInManager.SignOutAsync();
            _Logger.LogInformation("Пользователь {0} вышел из системы", user_name);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}
