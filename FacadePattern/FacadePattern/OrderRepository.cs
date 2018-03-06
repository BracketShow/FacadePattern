namespace FacadePattern
{
    public class OrderRepository : IOrderRepository
    {
        public void SaveOrder(Order order)
        {
            order.OrderNumber = "A123";
        }
    }
}