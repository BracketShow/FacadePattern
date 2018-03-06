namespace FacadePattern
{
    public class OrderItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Taxes { get; set; }
        public decimal Total => Quantity * Price + Taxes;
    }
}