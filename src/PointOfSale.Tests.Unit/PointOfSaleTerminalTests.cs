using Moq;
using NUnit.Framework;
using PointOfSale.Calculator;
using PointOfSale.Exceptions;
using PointOfSale.Model;
using PointOfSale.Services;

namespace PointOfSale.Tests.Unit
{
    [TestFixture]
    public class PointOfSaleTerminalTests
    {
        private PointOfSaleTerminal _sut;

        private Mock<ICodeScanner> _codeScannerMock;
        private Mock<IOrderList> _orderListMock;
        private Mock<IPriceCalculator> _priceCalculatorMock;
        private Mock<IPricingService> _pricingServiceMock;

        [SetUp]
        public void Setup()
        {
            _codeScannerMock = new Mock<ICodeScanner>(MockBehavior.Strict);
            _orderListMock = new Mock<IOrderList>();
            _priceCalculatorMock = new Mock<IPriceCalculator>();
            _pricingServiceMock = new Mock<IPricingService>();

            _sut = new PointOfSaleTerminal(_codeScannerMock.Object,
                _orderListMock.Object,
                _priceCalculatorMock.Object,
                _pricingServiceMock.Object);
        }

        [Test]
        public void Scan_ShouldAddAnItemToAnOrderList_WhenScannerCanFindAStockItem()
        {
            string itemCode = "A";
            decimal priceA = 1.25m;
            var item = SetupScannerWithStockItem(itemCode);
            SetupPricingService(item, priceA);

            _sut.Scan(itemCode);

            _codeScannerMock.VerifyAll();
            _orderListMock.Verify(i => i.AddItem(item, priceA), Times.Once);
        }

        [Test]
        public void Scan_ShouldThrowAnStockItemNotFoundException_WhenScannerThrowsStockItemNotFoundException()
        {
            string itemCode = "A";
            SetupScannerToThrowAnException();

            var errorMessage = "Could not process item. See inner exception for more details";
            var exception = Assert.Throws<ItemCounldNotBeProcessedException>(() => _sut.Scan(itemCode));
            Assert.AreEqual(errorMessage, exception.Message, "Error message should match");
            Assert.IsInstanceOf<StockItemNotFoundException>(exception.InnerException);
        }

        [Test]
        public void Scan_ShouldThrowAnItemCounldNotBeProcessedException_WhenPricingServiceThrowsPriceNotFoundException()
        {
            string itemCode = "A";
            SetupScannerWithStockItem(itemCode);
            SetupPricingServiceToThrowAnException();

            var errorMessage = "Could not process item. See inner exception for more details";
            var exception = Assert.Throws<ItemCounldNotBeProcessedException>(() => _sut.Scan(itemCode));
            Assert.AreEqual(errorMessage, exception.Message, "Error message should match");
            Assert.IsInstanceOf<PriceNotFoundException>(exception.InnerException);
        }

        [Test]
        public void CalculateTotal_ShouldCallCalculatorWithOrderList_WhenCalled()
        {
            _sut.CalculateTotal();

            _priceCalculatorMock.Verify(i => i.CalculatePrice(_orderListMock.Object), Times.Once);
        }

        private StockItem SetupScannerWithStockItem(string itemCode)
        {
            StockItem item = new StockItem(itemCode);

            _codeScannerMock.Setup(i => i.ScanCode(itemCode))
                .Returns(item)
                .Verifiable();

            return item;
        }

        private void SetupScannerToThrowAnException()
        {
            _codeScannerMock.Setup(i => i.ScanCode(It.IsAny<string>()))
                .Throws<StockItemNotFoundException>()
                .Verifiable();
        }

        private void SetupPricingService(StockItem item, decimal priceA)
        {
            _pricingServiceMock.Setup(i => i.GetPriceForStockItem(item.Code)).Returns(priceA).Verifiable();
        }

        private void SetupPricingServiceToThrowAnException()
        {
            _pricingServiceMock.Setup(i => i.GetPriceForStockItem(It.IsAny<string>()))
                .Throws<PriceNotFoundException>();
        }
    }
}
