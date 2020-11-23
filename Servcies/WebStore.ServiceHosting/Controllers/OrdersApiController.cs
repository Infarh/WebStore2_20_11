using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Order;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        [HttpGet("user/{UserName}")] // http://localhost:5001/api/orders/user/Ivanov
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => 
            await _OrderService.GetUserOrders(UserName);

        [HttpGet("{id}")] // http://localhost:5001/api/orders/5
        public async Task<OrderDTO> GetOrderById(int id) => await _OrderService.GetOrderById(id);

        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel) => 
            await _OrderService.CreateOrder(UserName, OrderModel);
    }
}
