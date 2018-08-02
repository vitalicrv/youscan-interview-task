using System.Collections.Generic;

namespace PointOfSale.Services
{
    public class InMemoryPricingService : IPricingService
    {
        private readonly Dictionary<string, decimal> _repository = new Dictionary<string, decimal>();

        public decimal GetPriceForStockItem(string itemCode)
        {
            if (_repository.ContainsKey(itemCode))
            {
                return _repository[itemCode];
            }

            throw new PriceNotFoundException($"Could not find price for item with code {itemCode}");
        }

        public void SetPriceForStockItem(string itemCode, decimal price)
        {
            _repository[itemCode] = price;
        }
    }
}