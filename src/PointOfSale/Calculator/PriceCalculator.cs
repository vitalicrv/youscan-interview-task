using PointOfSale.Model;

namespace PointOfSale.Calculator
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly IPriceCalculationRulesProvider _rulesProvider;

        public PriceCalculator(IPriceCalculationRulesProvider rulesProvider)
        {
            _rulesProvider = rulesProvider;
        }

        public decimal CalculatePrice(IOrderList list)
        {
            decimal totalAmount = 0.0m;

            foreach (var orderItem in list.GetOrderItems())
            {
                totalAmount += _rulesProvider.GetRule(orderItem.ItemCode).CalculatePrice(orderItem.Quantity, orderItem.Price);
            }

            return totalAmount;
        }
    }
}