using NUnit.Framework;
using PointOfSale.Services;

namespace PointOfSale.Tests.Unit.Services
{
    [TestFixture]
    public class InMemoryPricingServiceTests
    {
        private InMemoryPricingService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new InMemoryPricingService();
        }

        [Test]
        public void GetPriceForStockItem_ShouldThrowAnException_WhenPriceCouldNotBeFoundForAnItem()
        {
            var itemCode = "A";

            var exception = Assert.Throws<PriceNotFoundException>(() => _sut.GetPriceForStockItem(itemCode),
                "Should throw PriceNotFoundException when price list is empty");
            var errorMessage = $"Could not find price for item with code {itemCode}";
            Assert.AreEqual(errorMessage, exception.Message, "Error message should match");
        }

        [Test]
        public void GetPriceForStockItem_ShouldReturnCorrectPriceForStockItem()
        {
            var itemCodeA = "A";
            decimal priceA = 1.25m; 
            _sut.SetPriceForStockItem(itemCodeA, priceA);

            var actualPrice = _sut.GetPriceForStockItem(itemCodeA);

            Assert.AreEqual(priceA, actualPrice, "Price should match for stock item with code A");
        }

        [Test]
        public void GetPriceForStockItem_ShouldReturnLastUpdatedPriceForStockItem()
        {
            var itemCodeA = "A";
            decimal firstPriceA = 1.25m;
            decimal secondPriceA = 1.26m;
            _sut.SetPriceForStockItem(itemCodeA, firstPriceA);
            _sut.SetPriceForStockItem(itemCodeA, secondPriceA);

            var actualPrice = _sut.GetPriceForStockItem(itemCodeA);

            Assert.AreEqual(secondPriceA, actualPrice, "Price should match for stock item with code A");
        }
    }
}