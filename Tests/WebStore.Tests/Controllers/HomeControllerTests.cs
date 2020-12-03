using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blogs_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Blogs();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.BlogSingle();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.ContactUs();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Error404();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_thrown_ApplicationException()
        {
            var controller = new HomeController();

            const string exception_message = "";
            controller.Throw(exception_message);
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException2()
        {
            var controller = new HomeController();

            Exception error = null;
            const string exception_message = "";
            try
            {
                controller.Throw(exception_message);
            }
            catch (Exception e)
            {
                error = e;
            }

            var application_exception = Assert.IsType<ApplicationException>(error);
            Assert.Equal($"Исключение: {exception_message}", application_exception.Message);
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException3()
        {
            var controller = new HomeController();

            const string exception_message = "";

            var error = Assert.Throws<ApplicationException>(() => controller.Throw(exception_message));
            Assert.Equal($"Исключение: {exception_message}", error.Message);
        }

        [TestMethod]
        public void ErrorStatus_404_RedirectTo_Error404()
        {
            var controller = new HomeController();

            const string status_code_404 = "404";
            const string expected_method_name = nameof(HomeController.Error404);

            var result = controller.ErrorStatus(status_code_404);

            //Assert.NotNull(result);

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(expected_method_name, redirect_to_action.ActionName);
            Assert.Null(redirect_to_action.ControllerName);
        }
    }
}
