namespace PointOfSale.Services
{
    public interface IPricingService
    {
        decimal GetPriceForStockItem(string itemCode);
    }
}