using System.Linq;
using NUnit.Framework;
using PointOfSale.Model;

namespace PointOfSale.Tests.Unit
{
    [TestFixture]
    public class OrderListTests
    {
        private OrderList _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new OrderList();
        }

        [Test]
        public void GetOrderItems_ShouldReturnAnEmptyList_WhenOrderListIsInitialized()
        {
            _sut = new OrderList();

            var itemsInTheList = _sut.GetOrderItems();
            Assert.AreEqual(0, itemsInTheList.Count, "Order list should not contain any item");
        }

        [Test]
        public void GetOrderItems_ShouldReturnAListWithTwoItems_WhenTwoOrderItemsWhereAdded()
        {
            var stockItemA = new StockItem("A");
            var stockItemB = new StockItem("B");
            var priceA = 1.25m;
            var priceB = 4.25m;

            _sut.AddItem(stockItemA, priceA);
            _sut.AddItem(stockItemB, priceB);

            var itemsInTheList = _sut.GetOrderItems();
            Assert.AreEqual(2, itemsInTheList.Count, "Order list should contain 2 items");
            Assert.IsTrue(itemsInTheList.Any(i => i.ItemCode == stockItemA.Code && i.Price == priceA),
                "List should contain stockitem1 with correct price");
            Assert.IsTrue(itemsInTheList.Any(i => i.ItemCode == stockItemB.Code && i.Price == priceB),
                "List should contain stockitem2 with correct price");
        }

        [Test]
        public void AddItem_ShouldAddANewItemWithQuantityOne_WhenThereAreNoSameItemsInTheList()
        {
            var stockItemA = new StockItem("A");
            var priceA = 1.25m;

            _sut.AddItem(stockItemA, priceA);

            var itemsInTheList = _sut.GetOrderItems();
            Assert.AreEqual(1, itemsInTheList.Count, "Order list should have one item");
            Assert.AreEqual(1, itemsInTheList.First().Quantity, "Order item quantity should be 1");
        }

        [Test]
        public void AddItem_ShouldNotAddANewItemToAList_WhenThereAreSameItemsInTheListAlready()
        {
            var stockItemA = new StockItem("A");
            var priceA = 1.25m;

            _sut.AddItem(stockItemA, priceA);
            _sut.AddItem(stockItemA, priceA);

            var itemsInTheList = _sut.GetOrderItems();
            Assert.AreEqual(1, itemsInTheList.Count, "Order list should have one item");
        }

        [Test]
        public void AddItem_ShouldIncreaseQuantityByOne_WhenThereAreSameItemsInTheList()
        {
            var stockItemA = new StockItem("A");
            var priceA = 1.25m;

            _sut.AddItem(stockItemA, priceA);
            _sut.AddItem(stockItemA, priceA);

            var itemsInTheList = _sut.GetOrderItems();
            Assert.AreEqual(1, itemsInTheList.Count, "Order list should have one item");
            Assert.AreEqual(2, itemsInTheList.First().Quantity, "Order item quantity should be 1");
            Assert.AreEqual(priceA, itemsInTheList.First().Price, "Order item price should be correct");
        }

        [Test]
        public void AddItem_ShouldIncreaseQuantityByOneForCorrectItem_WhenThereAreSameItemsInTheList()
        {
            var stockItemB = new StockItem("B");
            var stockItemA = new StockItem("A");
            var priceB = 4.25m;
            var priceA = 1.25m;

            _sut.AddItem(stockItemB, priceB);
            _sut.AddItem(stockItemA, priceA);
            _sut.AddItem(stockItemA, priceA);

            var itemsInTheList = _sut.GetOrderItems();
            Assert.AreEqual(2, itemsInTheList.Count, "Order list should have two items");

            var itemB = itemsInTheList.Single(i => i.ItemCode == stockItemB.Code);
            Assert.AreEqual(1, itemB.Quantity, "Quantity should be 1 for item with code B");
            Assert.AreEqual(priceB, itemB.Price, "Price should be correct for item with code B");

            var itemA = itemsInTheList.Single(i => i.ItemCode == stockItemA.Code);
            Assert.AreEqual(2, itemA.Quantity, "Quantity should be 2 for item with code A");
            Assert.AreEqual(priceA, itemA.Price, "Price should be correct for item with code A");
        }
    }
}