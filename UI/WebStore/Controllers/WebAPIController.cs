using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestApi;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValueService _ValueService;

        public WebAPIController(IValueService ValueService) => _ValueService = ValueService;

        public IActionResult Index()
        {
            var values = _ValueService.Get();

            return View(values);
        }
    }
}
