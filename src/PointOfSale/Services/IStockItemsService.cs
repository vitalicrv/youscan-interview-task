using PointOfSale.Model;

namespace PointOfSale.Services
{
    public interface IStockItemsService
    {
        StockItem GetItemByCode(string code);
    }
}