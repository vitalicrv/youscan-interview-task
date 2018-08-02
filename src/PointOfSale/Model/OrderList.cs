using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Model
{
    public class OrderList : IOrderList
    {
        private readonly List<OrderItem> _list;

        public OrderList()
        {
            _list = new List<OrderItem>();
        }

        public void AddItem(StockItem newItem, decimal price)
        {
            var orderItem = GetItemFromListByCode(newItem.Code);

            if (orderItem != null)
            {
                orderItem.AddQuantity(1);
            }
            else
            {
                _list.Add(new OrderItem(newItem.Code, 1, price));
            }
        }

        public List<OrderItem> GetOrderItems()
        {
            return _list;
        }

        private OrderItem GetItemFromListByCode(string code)
        {
            return _list.SingleOrDefault(i => i.ItemCode == code);
        }
    }
}