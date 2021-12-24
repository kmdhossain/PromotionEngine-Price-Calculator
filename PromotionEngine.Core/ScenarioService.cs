using PromotionEngine.DataServices.Interfaces;
using PromotionEngine.Domains;

namespace PromotionEngine.Core
{
    public class ScenarioService
    {
        Scenario Scenario { get; }
        IPromotionDataService PromotionDataService { get; } 
        public ScenarioService(IPromotionDataService promotionDataService, Scenario scenario)
        {
            Scenario = scenario;
            PromotionDataService = promotionDataService;
        }

        public float CalculateScenatioTotal()
        {
            float totalCalculatedPrice = 0;

            foreach (var scenarioProductItemSKUKey in Scenario.ScenarioItems.Keys)
            {
                var scenarioItem = Scenario.ScenarioItems[scenarioProductItemSKUKey];

                if (scenarioItem.IsCalculatedInTotal)
                    continue;

                Promotion promotion = PromotionDataService.GetProductPromotion(scenarioItem.Product);

                //no promotion applicable for current product
                if (promotion == null)
                { 
                    totalCalculatedPrice += scenarioItem.Quantity * scenarioItem.Product.UnitPrice;
                    continue;
                }

                if (!IsAllPromotionItemsExistInScenario(promotion))//calculate the current item without any promotion
                    totalCalculatedPrice += scenarioItem.Quantity * scenarioItem.Product.UnitPrice;
                else
                {
                    var promotionOccuranceInProductCombination = GetPromotionOccuranceInProductCombination(promotion);
                    
                    //calculate total for number of promotions to be applied
                    totalCalculatedPrice += promotionOccuranceInProductCombination * promotion.PromotionPrice;

                    //calculate total for items for which promotion can't be applied
                    foreach (var promotionSubItem in promotion.ProductAndQuantity)
                    {
                       var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];
                 
                       //find remaining items that couldn't be included due to promotion combinations
                       var remainingQuantityWithOutPromotion = scenarioSubItem.Quantity - (promotionOccuranceInProductCombination * promotionSubItem.Value);

                       totalCalculatedPrice += remainingQuantityWithOutPromotion * scenarioSubItem.Product.UnitPrice;

                       scenarioSubItem.IsCalculatedInTotal = true;

                    }

                }

            }

            return totalCalculatedPrice;
        }

        int GetPromotionOccuranceInProductCombination(Promotion promotion)
        {
            var promotionOccuranceInProductCombination = int.MaxValue;
            foreach (var promotionSubItem in promotion.ProductAndQuantity)
            {
                var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];
                var subItemPromotionOccurance = scenarioSubItem.Quantity / promotionSubItem.Value;
                if (subItemPromotionOccurance < promotionOccuranceInProductCombination)
                    promotionOccuranceInProductCombination = subItemPromotionOccurance;
            }

            if (promotionOccuranceInProductCombination == int.MaxValue)
                promotionOccuranceInProductCombination = 0;

            return promotionOccuranceInProductCombination;
        }

        bool IsAllPromotionItemsExistInScenario(Promotion promotion)
        {
            bool allPromotionItemsInScenario = true;

            foreach (var promotionSubItem in promotion.ProductAndQuantity)
                if (!Scenario.ScenarioItems.ContainsKey(promotionSubItem.Key))
                {
                    allPromotionItemsInScenario = false;
                    break;
                }

            return allPromotionItemsInScenario;
        }
    }
}