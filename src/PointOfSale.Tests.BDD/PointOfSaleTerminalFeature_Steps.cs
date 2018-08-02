using LightBDD.NUnit3;
using NUnit.Framework;
using PointOfSale.Calculator;
using PointOfSale.Calculator.CalculationRules;
using PointOfSale.Model;
using PointOfSale.Services;

namespace PointOfSale.Tests.BDD
{
    public partial class PointOfSaleTerminalFeature : FeatureFixture
    {
        private InMemoryStockItemsService _stockItemsService;
        private ICodeScanner _codeScanner;
        private IOrderList _orderList;
        private InMemoryPricingService _pricingService;
        private PriceCalculator _priceCalculator;
        private PriceCalculationRulesProvider _priceCalculationRulesProvider;
        private PointOfSaleTerminal _terminal;

        [SetUp]
        public void Setup()
        {
            _stockItemsService = new InMemoryStockItemsService();
            _codeScanner = new CodeScanner(_stockItemsService);
            _pricingService = new InMemoryPricingService();
            _priceCalculationRulesProvider = new PriceCalculationRulesProvider();
            _priceCalculator = new PriceCalculator(_priceCalculationRulesProvider);
        }

        private void Terminal_is_ready_to_scan_items()
        {
            _orderList = new OrderList();
            _terminal = new PointOfSaleTerminal(_codeScanner, _orderList, _priceCalculator, _pricingService);
        }

        private void All_items_are_added_to_a_stock_items_service()
        {
            _stockItemsService.AddStockItem("A");
            _stockItemsService.AddStockItem("B");
            _stockItemsService.AddStockItem("C");
            _stockItemsService.AddStockItem("D");
        }

        private void All_prices_are_added_to_pricing_service()
        {
            _pricingService.SetPriceForStockItem("A", 1.25m);
            _pricingService.SetPriceForStockItem("B", 4.25m);
            _pricingService.SetPriceForStockItem("C", 1.0m);
            _pricingService.SetPriceForStockItem("D", 0.75m);
        }

        private void Dicount_for_pack_is_added_for_item_with_code_CODE_with_price_PRICE_for_QUANTITY(
            string code, decimal price, int quantity)
        {
            _priceCalculationRulesProvider.AddRule(code, 
                new DiscountForPackPriceCalculationRule(quantity, price));
        }

        private void Scanning_item_with_code_CODE(string code)
        {
            _terminal.Scan(code);
        }

        private void Total_amount_should_be_AMOUNT(decimal amount)
        {
            Assert.AreEqual(amount, _terminal.CalculateTotal(), "Calculated total amount should match");
        }
    }
}
