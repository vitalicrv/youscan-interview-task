using PointOfSale.Calculator.CalculationRules;

namespace PointOfSale.Calculator
{
    public interface IPriceCalculationRulesProvider
    {
        IPriceCaluclationRule GetRule(string code);
    }
}