using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit3;
using NUnit.Framework;

namespace PointOfSale.Tests.BDD
{
    [TestFixture]
    [FeatureDescription(
        @"As a user
I want proper total amount to be calculated
When I scan items in point of sale terminal")]
    public partial class PointOfSaleTerminalFeature
    {
        [Scenario]
        public void Scanning_items_with_codes_ABCD()
        {
            Runner.RunScenario(
                given => All_items_are_added_to_a_stock_items_service(),
                and => All_prices_are_added_to_pricing_service(),
                and => Terminal_is_ready_to_scan_items(),
                when => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("B"),
                and => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("D"),
                then => Total_amount_should_be_AMOUNT(7.25m));
        }

        [Scenario]
        public void Scanning_items_with_codes_ABCDABA()
        {
            Runner.RunScenario(
                given => All_items_are_added_to_a_stock_items_service(),
                and => All_prices_are_added_to_pricing_service(),
                and => Dicount_for_pack_is_added_for_item_with_code_CODE_with_price_PRICE_for_QUANTITY("A", 3.0m, 3),
                and => Dicount_for_pack_is_added_for_item_with_code_CODE_with_price_PRICE_for_QUANTITY("C", 5.0m, 6),
                and => Terminal_is_ready_to_scan_items(),
                when => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("B"),
                and => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("D"),
                and => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("B"),
                and => Scanning_item_with_code_CODE("A"),
                then => Total_amount_should_be_AMOUNT(13.25m));
        }

        [Scenario]
        public void Scanning_items_with_codes_CCCCCCC()
        {
            Runner.RunScenario(
                given => All_items_are_added_to_a_stock_items_service(),
                and => All_prices_are_added_to_pricing_service(),
                and => Dicount_for_pack_is_added_for_item_with_code_CODE_with_price_PRICE_for_QUANTITY("A", 3.0m, 3),
                and => Dicount_for_pack_is_added_for_item_with_code_CODE_with_price_PRICE_for_QUANTITY("C", 5.0m, 6),
                and => Terminal_is_ready_to_scan_items(),
                when => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("C"),
                and => Scanning_item_with_code_CODE("C"),
                then => Total_amount_should_be_AMOUNT(6.0m));
        }

        [Scenario]
        public void Scanning_items_with_codes_AAAAAAAB()
        {
            Runner.RunScenario(
                given => All_items_are_added_to_a_stock_items_service(),
                and => All_prices_are_added_to_pricing_service(),
                and => Dicount_for_pack_is_added_for_item_with_code_CODE_with_price_PRICE_for_QUANTITY("A", 3.0m, 3),
                and => Dicount_for_pack_is_added_for_item_with_code_CODE_with_price_PRICE_for_QUANTITY("C", 5.0m, 6),
                and => Terminal_is_ready_to_scan_items(),
                when => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("A"),
                and => Scanning_item_with_code_CODE("B"),
                then => Total_amount_should_be_AMOUNT(11.5m));
        }
    }
}
