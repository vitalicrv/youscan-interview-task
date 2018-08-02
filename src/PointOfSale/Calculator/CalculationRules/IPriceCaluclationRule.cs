namespace PointOfSale.Calculator.CalculationRules
{
    public interface IPriceCaluclationRule
    {
        decimal CalculatePrice(int quantity, decimal priceForSingleItem);
    }
}