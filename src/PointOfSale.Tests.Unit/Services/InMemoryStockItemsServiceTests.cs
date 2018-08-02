using NUnit.Framework;
using PointOfSale.Services;

namespace PointOfSale.Tests.Unit.Services
{
    [TestFixture]
    public class InMemoryStockItemsServiceTests
    {
        private InMemoryStockItemsService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new InMemoryStockItemsService();
        }

        [Test]
        public void GetItemByCode_ShouldThrowStockItemNotFoundException_WhenThereIsNoStockItemFound()
        {
            var itemCode = "A";

            var exception = Assert.Throws<StockItemNotFoundException>(() => _sut.GetItemByCode(itemCode),
                "Code scanner should throw an exception");

            Assert.AreEqual($"Could not find item with code \"{itemCode}\" in stock",
                exception.Message, "Exception should contain explanation message");
        }

        [Test]
        public void GetItemByCode_ShouldReturnStockItemwithCodeA_WhenThereIsOneInStock()
        {
            var itemCodeA = "A";
            var itemCodeB = "B";
            _sut.AddStockItem(itemCodeB);
            _sut.AddStockItem(itemCodeA);

            var actualItem = _sut.GetItemByCode(itemCodeA);

            Assert.AreEqual(itemCodeA, actualItem.Code, "Item code should match");
        }
    }
}