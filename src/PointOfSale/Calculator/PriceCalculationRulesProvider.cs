using System.Collections.Generic;
using PointOfSale.Calculator.CalculationRules;

namespace PointOfSale.Calculator
{
    public class PriceCalculationRulesProvider : IPriceCalculationRulesProvider
    {
        private readonly DefaultPriceCalculationRule _defaultRule;
        private readonly Dictionary<string, IPriceCaluclationRule> _repository;

        public PriceCalculationRulesProvider()
        {
            _defaultRule = new DefaultPriceCalculationRule();
            _repository = new Dictionary<string, IPriceCaluclationRule>();
        }

        public IPriceCaluclationRule GetRule(string code)
        {
            if (_repository.ContainsKey(code))
            {
                return _repository[code];
            }

            return _defaultRule;
        }

        public void AddRule(string code, IPriceCaluclationRule rule)
        {
            _repository[code] = rule;
        }
    }
}