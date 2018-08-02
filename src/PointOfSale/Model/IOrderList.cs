using System.Collections.Generic;

namespace PointOfSale.Model
{
    public interface IOrderList
    {
        void AddItem(StockItem newItem, decimal price);

        List<OrderItem> GetOrderItems();
    }
}