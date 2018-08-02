using Moq;
using NUnit.Framework;
using PointOfSale.Calculator;
using PointOfSale.Calculator.CalculationRules;
using PointOfSale.Model;

namespace PointOfSale.Tests.Unit.Calculator
{
    [TestFixture]
    public class PriceCalculatorTests
    {
        private PriceCalculator _sut;
        private Mock<IPriceCalculationRulesProvider> _calculationRulesProviderMock;
        private Mock<IPriceCaluclationRule> _calculationRuleMock;

        [SetUp]
        public void Setup()
        {
            SetupCalculationRules();
            _sut = new PriceCalculator(_calculationRulesProviderMock.Object);
        }

        [Test]
        public void CalculatePrice_ShouldCallCalcualtionRuleOnce_WhenThereIsOneItemInTheOrderList()
        {
            int quantity = 1;
            decimal priceForSingleItem = 1.25m;
            var orderList = new OrderList();
            orderList.AddItem(new StockItem("A"), priceForSingleItem);

            _sut.CalculatePrice(orderList);

            _calculationRuleMock.Verify(i => i.CalculatePrice(quantity, priceForSingleItem), Times.Once);
            _calculationRulesProviderMock.Verify();
        }

        [Test]
        public void CalculatePrice_ShouldCallCalcualtionRuleForEachItem_WhenThereIsMoreThenOneItemInTheOrderList()
        {
            int quantity = 1;
            decimal priceForSingleItem = 1.25m;
            var orderList = new OrderList();
            orderList.AddItem(new StockItem("A"), priceForSingleItem);
            orderList.AddItem(new StockItem("B"), priceForSingleItem);

            _sut.CalculatePrice(orderList);

            _calculationRuleMock.Verify(i => i.CalculatePrice(quantity, priceForSingleItem), Times.Exactly(2));
            _calculationRulesProviderMock.Verify();
        }

        [Test]
        public void CalculatePrice_ShouldSumAllPricesForAllItems()
        {
            int quantity = 1;
            decimal priceForItemA = 1.25m;
            decimal priceForItemB = 4.25m;
            decimal expectedTotal = priceForItemA + priceForItemB;

            var orderList = new OrderList();
            orderList.AddItem(new StockItem("A"), priceForItemA);
            orderList.AddItem(new StockItem("B"), priceForItemB);

            _calculationRuleMock.Setup(i => i.CalculatePrice(quantity, priceForItemA))
                .Returns(priceForItemA)
                .Verifiable();

            _calculationRuleMock.Setup(i => i.CalculatePrice(quantity, priceForItemB))
                .Returns(priceForItemB)
                .Verifiable();

            var totalPrice = _sut.CalculatePrice(orderList);

            Assert.AreEqual(expectedTotal, totalPrice, "Total price should match");
            _calculationRuleMock.VerifyAll();
            _calculationRulesProviderMock.VerifyAll();
        }

        private void SetupCalculationRules()
        {
            _calculationRuleMock = new Mock<IPriceCaluclationRule>();

            _calculationRulesProviderMock = new Mock<IPriceCalculationRulesProvider>();
            _calculationRulesProviderMock.Setup(i => i.GetRule(It.IsAny<string>()))
                .Returns(_calculationRuleMock.Object)
                .Verifiable();
        }
    }
}