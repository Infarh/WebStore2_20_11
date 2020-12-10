using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebStore.Domain.DTO.Order;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService) => _CartService = CartService;

        public IActionResult Details() => View(new CartOrderViewModel { Cart = _CartService.TransformFromCart() });

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult Clear()
        {
            _CartService.Clear();
            return RedirectToAction(nameof(Details));
        }

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel OrderModel, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartOrderViewModel
                {
                    Cart = _CartService.TransformFromCart(),
                    Order = OrderModel
                });

            var create_order_model = new CreateOrderModel
            {
                Order = OrderModel,
                Items = _CartService.TransformFromCart().Items
                   .Select(item => new OrderItemDTO
                   {
                       Id = item.Product.Id,
                       Price = item.Product.Price,
                       Quantity = item.Quantity
                   })
                   .ToList()
            };


            var order = await OrderService.CreateOrder(User.Identity.Name, create_order_model);

            _CartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region WebAPI

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCartAPI(int id)
        {
            _CartService.AddToCart(id);
            return Json(new { id, message = $"Товар с id:{id} был добавлен в корзину" });
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            _CartService.DecrementFromCart(id);
            return Ok();
        }

        public IActionResult RemoveFromCartAPI(int id)
        {
            _CartService.RemoveFromCart(id);
            return Ok();
        }

        public IActionResult ClearAPI()
        {
            _CartService.Clear();
            return Ok();
        }

        #endregion
    }
}
