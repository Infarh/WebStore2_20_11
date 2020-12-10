using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke([FromServices] ICartService CartService) => View(CartService.TransformFromCart());
    }
}
