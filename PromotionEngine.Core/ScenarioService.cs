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

                //no promotion applicable for current product, thus calculate as regular product item and skip promotion calculation
                if ((promotion == null) || !IsAllPromotionItemsExistInScenario(promotion))
                {
                    totalCalculatedPrice += scenarioItem.Quantity * scenarioItem.Product.UnitPrice;
                    continue;
                }

                var minimumPromotionsForProductCombinationInScenario = GetMinimumPromotionsForProductCombinationInScenario(promotion);

                //calculate total based on number of promotions to be applied
                totalCalculatedPrice += minimumPromotionsForProductCombinationInScenario * promotion.PromotionPrice;

                //calculate total based on items for which promotion can't be applied
                foreach (var promotionSubItem in promotion.ProductAndQuantity)
                {
                    var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];

                    //find remaining items that couldn't be included due to promotion combinations
                    var remainingQuantityWithOutPromotion = scenarioSubItem.Quantity - (minimumPromotionsForProductCombinationInScenario * promotionSubItem.Value);

                    totalCalculatedPrice += remainingQuantityWithOutPromotion * scenarioSubItem.Product.UnitPrice;

                    scenarioSubItem.IsCalculatedInTotal = true;

                }

            }

            return totalCalculatedPrice;
        }

        int GetMinimumPromotionsForProductCombinationInScenario(Promotion promotion)
        {
            var minimumPromotionsForProductCombination = int.MaxValue;
            foreach (var promotionSubItem in promotion.ProductAndQuantity)
            {
                if (Scenario.ScenarioItems.ContainsKey(promotionSubItem.Key))
                {
                    var scenarioSubItem = Scenario.ScenarioItems[promotionSubItem.Key];
                    var promitionSubItemQuantity = promotionSubItem.Value;
                    var subItemPromotionOccurence = scenarioSubItem.Quantity / promitionSubItemQuantity;
                    if (subItemPromotionOccurence < minimumPromotionsForProductCombination)
                        minimumPromotionsForProductCombination = subItemPromotionOccurence;
                }
            }

            if (minimumPromotionsForProductCombination == int.MaxValue)
                minimumPromotionsForProductCombination = 0;

            return minimumPromotionsForProductCombination;
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