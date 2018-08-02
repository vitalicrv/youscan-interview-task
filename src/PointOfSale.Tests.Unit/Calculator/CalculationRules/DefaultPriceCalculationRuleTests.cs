using NUnit.Framework;
using PointOfSale.Calculator.CalculationRules;

namespace PointOfSale.Tests.Unit.Calculator.CalculationRules
{
    [TestFixture]
    public class DefaultPriceCalculationRuleTests
    {
        private DefaultPriceCalculationRule _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new DefaultPriceCalculationRule();
        }

        [Test]
        public void CalculatePrice_ShouldMultiplyQuantityByPriceForItem()
        {
            int quantity = 5;
            decimal price = 1.24m;
            decimal expectedTotalPrice = quantity * price;

            decimal totalPrice = _sut.CalculatePrice(quantity, price);

            Assert.AreEqual(expectedTotalPrice, totalPrice);
        }

        [Test]
        public void CalculatePrice_ShouldReturnZero_WhenQuantityIsZero()
        {
            int quantity = 0;
            decimal price = 1.24m;
            decimal expectedTotalPrice = 0.0m;

            decimal totalPrice = _sut.CalculatePrice(quantity, price);

            Assert.AreEqual(expectedTotalPrice, totalPrice);
        }
    }
}