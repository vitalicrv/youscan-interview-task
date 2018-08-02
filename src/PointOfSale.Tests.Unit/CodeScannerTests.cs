using Moq;
using NUnit.Framework;
using PointOfSale.Model;
using PointOfSale.Services;

namespace PointOfSale.Tests.Unit
{
    [TestFixture]
    public class CodeScannerTests
    {
        private CodeScanner _sut;
        private Mock<IStockItemsService> _stockItemsServiceMock;

        [SetUp]
        public void Setup()
        {
            _stockItemsServiceMock = new Mock<IStockItemsService>();
            _sut = new CodeScanner(_stockItemsServiceMock.Object);
        }

        [Test]
        public void ScanCode_ShouldReturnStockItemFromStockItemsService_WhenCalled()
        {
            var code = "A";
            var stockItem = new StockItem(code);
            _stockItemsServiceMock.Setup(i => i.GetItemByCode(code))
                .Returns(stockItem)
                .Verifiable();

            var item = _sut.ScanCode(code);

            Assert.AreEqual(stockItem, item, "Stock item should match");
            Assert.AreEqual(code, item.Code, "Stock item code should match");
            _stockItemsServiceMock.VerifyAll();
        }
    }
}