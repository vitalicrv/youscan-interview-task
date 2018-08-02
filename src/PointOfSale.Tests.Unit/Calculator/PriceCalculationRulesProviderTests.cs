using NUnit.Framework;
using PointOfSale.Calculator;
using PointOfSale.Calculator.CalculationRules;

namespace PointOfSale.Tests.Unit.Calculator
{
    [TestFixture]
    public class PriceCalculationRulesProviderTests
    {
        class StubPriceCalculationRule : IPriceCaluclationRule
        {
            public decimal CalculatePrice(int quantity, decimal priceForSingleItem)
            {
                return 0;
            }
        }

        class SecondStubPriceCalculationRule : IPriceCaluclationRule
        {
            public decimal CalculatePrice(int quantity, decimal priceForSingleItem)
            {
                return 0;
            }
        }

        private PriceCalculationRulesProvider _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new PriceCalculationRulesProvider();
        }

        [Test]
        public void GetRule_ShouldReturnDefaultPriceCalculationRule_WhenThereIsNoSpecialRuleForItemCode()
        {
            var itemCode = "A";

            var rule = _sut.GetRule(itemCode);

            Assert.IsInstanceOf<DefaultPriceCalculationRule>(rule);
        }

        [Test]
        public void GetRule_ShouldReturnStubPriceCalculationRule_WhenItWasAddedForItemCode()
        {
            var itemCode = "A";
            _sut.AddRule(itemCode, new StubPriceCalculationRule());

            var rule = _sut.GetRule(itemCode);

            Assert.IsInstanceOf<StubPriceCalculationRule>(rule);
        }

        [Test]
        public void GetRule_ShouldReturnSecondStubPriceCalculationRule_WhenTwoStubRulesWasAddedForItemCode()
        {
            var itemCode = "A";
            _sut.AddRule(itemCode, new StubPriceCalculationRule());
            _sut.AddRule(itemCode, new SecondStubPriceCalculationRule());

            var rule = _sut.GetRule(itemCode);

            Assert.IsInstanceOf<SecondStubPriceCalculationRule>(rule);
        }
    }
}