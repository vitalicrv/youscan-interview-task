using System.Collections.Generic;
using PointOfSale.Model;

namespace PointOfSale.Services
{
    public class InMemoryStockItemsService : IStockItemsService
    {
        private readonly Dictionary<string, StockItem> _repository = new Dictionary<string, StockItem>();

        public StockItem GetItemByCode(string code)
        {
            if (_repository.ContainsKey(code))
            {
                return _repository[code];
            }

            throw new StockItemNotFoundException($"Could not find item with code \"{code}\" in stock");
        }

        public void AddStockItem(string code)
        {
            if (_repository.ContainsKey(code))
                return;

            _repository[code] = new StockItem(code);
        }
    }
}