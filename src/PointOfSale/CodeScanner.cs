using PointOfSale.Model;
using PointOfSale.Services;

namespace PointOfSale
{
    public class CodeScanner : ICodeScanner
    {
        private readonly IStockItemsService _stockItemsService;

        public CodeScanner(IStockItemsService stockItemsService)
        {
            _stockItemsService = stockItemsService;
        }

        public StockItem ScanCode(string code)
        {
            return _stockItemsService.GetItemByCode(code);
        }
    }
}