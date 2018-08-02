namespace PointOfSale.Model
{
    public class StockItem
    {
        public string Code { get; }

        public StockItem(string code)
        {
            Code = code;
        }
    }
}
