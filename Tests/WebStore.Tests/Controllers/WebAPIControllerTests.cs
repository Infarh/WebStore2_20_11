using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using WebStore.Controllers;
using WebStore.Interfaces.TestApi;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebAPIControllerTests
    {
        //private class ValueService : IValueService
        //{

        //}

        [TestMethod]
        public void Index_Returns_View_with_Values()
        {
            var expected_values = new[] { "1", "2", "3" };

            var values_service_mock = new Mock<IValueService>();
            values_service_mock
               .Setup(service => service.Get())
               .Returns(expected_values);

            // Режим "Стаб"
            var controller = new WebAPIController(values_service_mock.Object);

            var result = controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            Assert.Equal(expected_values.Length, model.Count());

            // Режим "Мок"

            values_service_mock.Verify(service => service.Get());
            values_service_mock.VerifyNoOtherCalls();
        }
    }
}
