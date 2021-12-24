 using PromotionEngine.Domains;
using PromotionEngine.Products;

namespace PromotionEngine.Core
{
    public class ScenarioService
    {
        Scenario Scenario { get; }
        PromotionDataService PromotionDataService { get { return new PromotionDataService(); } }
        public ScenarioService(Scenario scenario)
        {
            Scenario = scenario;
        }

        public float CalculateScenatioTotal()
        {
            float totalCalculatedPrice = 0;

            foreach (var scenarioItemKey in Scenario.ScenarioItems.Keys)
            {
                var scenarioItem = Scenario.ScenarioItems[scenarioItemKey];

                if (scenarioItem.IsCalculatedInTotal)
                    continue;

                var promotion = PromotionDataService.GetProductPromotion(scenarioItem.Product);

                if (promotion == null)
                { 
                    totalCalculatedPrice += scenarioItem.Quantity * scenarioItem.Product.UnitPrice;
                    continue;
                }

                //check if all promotion items exists in scenario
                bool allPromotionItemsInScenario = true;

                foreach (var promotionSubItem in promotion.ProductAndQuantity)
                    if (!Scenario.ScenarioItems.ContainsKey(promotionSubItem.Key))
                    {
                        allPromotionItemsInScenario = false;
                        break;
                    }

                if (!allPromotionItemsInScenario)//calculate the current item without any promotion
                    totalCalculatedPrice += scenarioItem.Quantity * scenarioItem.Product.UnitPrice;
                else
                {
                    //find the minimum promotion occurance from scenario
                    var promotionOccuranceInProductionCombination = int.MaxValue;
                    foreach (var promotionSubItem in promotion.ProductAndQuantity)
                    {
                        var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];
                        var subItemPromotionOccurance = scenarioSubItem.Quantity / promotionSubItem.Value;
                        if (subItemPromotionOccurance < promotionOccuranceInProductionCombination)
                            promotionOccuranceInProductionCombination = subItemPromotionOccurance;
                    }

                    if (promotionOccuranceInProductionCombination == int.MaxValue)
                        promotionOccuranceInProductionCombination = 0;

                    //calculate total for number of promotions to be applied
                    totalCalculatedPrice += promotionOccuranceInProductionCombination * promotion.PromotionPrice;

                    //calculate total for items for which promotion can't be applied
                    foreach (var promotionSubItem in promotion.ProductAndQuantity)
                    {
                       var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];
                 
                       //find remaining items that couldn't be included due to promotion combinations
                       var remainingQuantityWithOutPromotion = scenarioSubItem.Quantity - (promotionOccuranceInProductionCombination * promotionSubItem.Value);

                       totalCalculatedPrice += remainingQuantityWithOutPromotion * scenarioSubItem.Product.UnitPrice;

                       scenarioSubItem.IsCalculatedInTotal = true;

                    }

                }

            }

            return totalCalculatedPrice;
        }
    }
}