using PromotionEngine.Domains;
using PromotionEngine.Products;
using System;
using System.Collections.Generic;

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
            var _promotionCheckedForProduct = new HashSet<string>();
            float totalCalculatedPrice = 0;

            foreach (var scenarioItem in Scenario.ScenarioItems.Values)
            {
                if (_promotionCheckedForProduct.Contains(scenarioItem.Product.SKU))
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
                        // calculate the promotion for C & D, if D exists in scenario, 
                        foreach (var promotionItem in promotion.ProductAndQuantity)
                            _promotionCheckedForProduct.Add(promotionItem.Key);

                        // otherwise consider C as non promotion item
                        totalCalculatedPrice += 20;
                    }

                }
            }

            return totalCalculatedPrice;
        }
    }
}
