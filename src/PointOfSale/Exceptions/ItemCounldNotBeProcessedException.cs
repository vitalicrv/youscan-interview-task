using System;

namespace PointOfSale.Exceptions
{
    public class ItemCounldNotBeProcessedException : Exception
    {
        public ItemCounldNotBeProcessedException()
        {
        }

        public ItemCounldNotBeProcessedException(string message)
            : base(message)
        {
        }

        public ItemCounldNotBeProcessedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}