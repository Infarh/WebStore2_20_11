using System;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Throw(string id) => 
            throw new ApplicationException($"Исключение: {id ?? "<null>"}");

        public IActionResult Blogs() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Checkout() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Error404() => View();
    }
}
