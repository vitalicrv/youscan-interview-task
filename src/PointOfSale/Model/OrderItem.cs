namespace PointOfSale.Model
{
    public class OrderItem
    {
        public string ItemCode { get; }

        public int Quantity { get; private set; }

        public decimal Price { get; }

        public OrderItem(string itemCode, int quantity, decimal price)
        {
            ItemCode = itemCode;
            Quantity = quantity;
            Price = price;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}
