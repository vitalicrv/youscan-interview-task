using NUnit.Framework;
using PointOfSale.Calculator.CalculationRules;

namespace PointOfSale.Tests.Unit.Calculator.CalculationRules
{
    public class DiscountForPackPriceCalculationRuleTests
    {
        private DiscountForPackPriceCalculationRule _sut;
        private int _itemsInPack;
        private decimal _priceForPack;
        private decimal _priceForItem;

        [SetUp]
        public void Setup()
        {
            _itemsInPack = 5;
            _priceForPack = 7.0m;
            _priceForItem = 1.2m;
            _sut = new DiscountForPackPriceCalculationRule(_itemsInPack, _priceForPack);
        }

        [Test]
        public void CalculatePrice_ShouldReturnPriceForPack_WhenItemQuantityEqualsItemsInPack()
        {
            var totalPrice = _sut.CalculatePrice(_itemsInPack, _priceForItem);

            Assert.AreEqual(_priceForPack, totalPrice, "Total price should match price for one pack");
        }

        [Test]
        public void CalculatePrice_ShouldMultiplyQuantityByPriceForSingleItem_WhenQuantityIsLessThenItemsInPack()
        {
            int quantity = _itemsInPack - 1;
            var expectedTotalPrice = quantity * _priceForItem;

            var totalPrice = _sut.CalculatePrice(quantity, _priceForItem);

            Assert.AreEqual(expectedTotalPrice, totalPrice, "Total price should match expected total price");
        }

        [Test]
        public void CalculatePrice_ShouldReturnPriceForTwoPacksPlusPriceForItemsNotInPack()
        {
            int quantity = _itemsInPack * 2 + _itemsInPack - 1;
            var expectedTotalPrice = (quantity / _itemsInPack * _priceForPack) + 
                                      (quantity % _itemsInPack * _priceForItem);

            var totalPrice = _sut.CalculatePrice(quantity, _priceForItem);

            Assert.AreEqual(expectedTotalPrice, totalPrice, "Total price should match expected total price");
        }
    }
}