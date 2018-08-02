namespace PointOfSale.Calculator.CalculationRules
{
    public class DiscountForPackPriceCalculationRule : IPriceCaluclationRule
    {
        private readonly int _itemsInPack;
        private readonly decimal _priceForPack;

        public DiscountForPackPriceCalculationRule(int itemsInPack, decimal priceForPack)
        {
            _itemsInPack = itemsInPack;
            _priceForPack = priceForPack;
        }

        public decimal CalculatePrice(int quantity, decimal priceForSingleItem)
        {
            decimal priceForPacks = (quantity / _itemsInPack) * _priceForPack;
            decimal priceForSingleItems = (quantity % _itemsInPack) * priceForSingleItem;

            return priceForPacks + priceForSingleItems;
        }
    }
}