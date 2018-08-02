namespace PointOfSale.Calculator.CalculationRules
{
    public class DefaultPriceCalculationRule : IPriceCaluclationRule
    {
        public decimal CalculatePrice(int quantity, decimal priceForSingleItem)
        {
            return quantity * priceForSingleItem;
        }
    }
}