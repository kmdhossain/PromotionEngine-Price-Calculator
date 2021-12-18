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
                    totalCalculatedPrice += scenarioItem.Quantity * scenarioItem.Product.UnitPrice;
                else
                {
                    bool isSinglePromotion = promotion.ProductAndQuantity.Count == 1;
                    if (isSinglePromotion)
                    {
                        var promotionOccurance = scenarioItem.Quantity / promotion.ProductAndQuantity[scenarioItem.Product.SKU];
                        totalCalculatedPrice += promotionOccurance * promotion.PromotionPrice;

                        var remainingQuantityWithOutPromotion = scenarioItem.Quantity % promotion.ProductAndQuantity[scenarioItem.Product.SKU];
                        totalCalculatedPrice += remainingQuantityWithOutPromotion * scenarioItem.Product.UnitPrice;
                    }
                    else
                    {
                        //check if all promotion items exists in scenario
                        bool allPromotionItemsInScenario = true;

                        foreach (var promotionSubItem in promotion.ProductAndQuantity)
                            if (!Scenario.ScenarioItems.ContainsKey(promotionSubItem.Key))
                            {
                                allPromotionItemsInScenario = false;
                                break;
                            }

                        if(!allPromotionItemsInScenario)//calculate the current item without any promotion
                            totalCalculatedPrice += scenarioItem.Quantity * scenarioItem.Product.UnitPrice;
                        else
                        {
                            //find the minimum promotion occurance from scenario
                            var promotionOccuranceInSubItem = int.MaxValue;
                            foreach (var promotionSubItem in promotion.ProductAndQuantity)
                            {
                                var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];
                                var subItemPromotionOccurance = scenarioSubItem.Quantity / promotionSubItem.Value;
                                if (subItemPromotionOccurance < promotionOccuranceInSubItem)
                                    promotionOccuranceInSubItem = subItemPromotionOccurance;
                            }

                            if (promotionOccuranceInSubItem == int.MaxValue)
                                promotionOccuranceInSubItem = 0;

                            //calculate total for number of promotions to be applied
                            totalCalculatedPrice += promotionOccuranceInSubItem * promotion.PromotionPrice;

                            //calculate total for items for which promotion can't be applied
                            foreach (var promotionSubItem in promotion.ProductAndQuantity)
                            {
                                var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];
                                scenarioSubItem.IsCalculatedInTotal = true;

                                //find remaining items that included in promotion quantity for the item
                                var remainingQuantityWithOutPromotion = scenarioSubItem.Quantity % promotionSubItem.Value;
                                
                                //find remaining items that couldn't be included due to promotion combinations
                                remainingQuantityWithOutPromotion += scenarioSubItem.Quantity - (promotionOccuranceInSubItem * promotionSubItem.Value) - remainingQuantityWithOutPromotion;

                                totalCalculatedPrice += remainingQuantityWithOutPromotion * scenarioSubItem.Product.UnitPrice;
                            }

                       }

                    }

                }
            }

            return totalCalculatedPrice;
        }
    }
}
