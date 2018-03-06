using System;

namespace FacadePattern
{
    public class ShippingService : IShippingService
    {
        public string ShipOrder(string orderNumber)
        {
            return $"TRACKING-123-{orderNumber}";
        }
    }
}