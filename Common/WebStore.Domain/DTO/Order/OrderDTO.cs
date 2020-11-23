using System;
using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class CreateOrderModel
    {
        public OrderViewModel Order { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }
}
