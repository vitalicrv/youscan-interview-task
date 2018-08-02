using System;

namespace PointOfSale.Services
{
    public class StockItemNotFoundException : Exception
    {
        public StockItemNotFoundException()
        {
        }

        public StockItemNotFoundException(string message)
            : base(message)
        {
        }

        public StockItemNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}