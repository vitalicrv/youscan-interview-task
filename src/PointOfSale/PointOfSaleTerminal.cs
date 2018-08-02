using System;
using PointOfSale.Calculator;
using PointOfSale.Exceptions;
using PointOfSale.Model;
using PointOfSale.Services;

namespace PointOfSale
{
    public class PointOfSaleTerminal
    {
        private readonly ICodeScanner _scanner;
        private readonly IOrderList _orderList;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IPricingService _pricingService;

        public PointOfSaleTerminal(ICodeScanner scanner,
            IOrderList orderList,
            IPriceCalculator priceCalculator,
            IPricingService pricingService)
        {
            _scanner = scanner;
            _orderList = orderList;
            _priceCalculator = priceCalculator;
            _pricingService = pricingService;
        }

        public void Scan(string code)
        {
            try
            {
                var item = _scanner.ScanCode(code);
                var price = _pricingService.GetPriceForStockItem(item.Code);
                _orderList.AddItem(item, price);
            }
            catch (Exception ex)
            {
                throw new ItemCounldNotBeProcessedException("Could not process item. See inner exception for more details", ex);
            }
        }

        public decimal CalculateTotal()
        {
            return _priceCalculator.CalculatePrice(_orderList);
        }
    }
}
