using PointOfSale.Model;

namespace PointOfSale.Calculator
{
    public interface IPriceCalculator
    {
        decimal CalculatePrice(IOrderList list);
    }
}