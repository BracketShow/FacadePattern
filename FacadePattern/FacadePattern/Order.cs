using System.Collections.Generic;

namespace FacadePattern
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}