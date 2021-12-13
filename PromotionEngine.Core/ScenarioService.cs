﻿using PromotionEngine.Domains;
using PromotionEngine.Products;
using System;

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

            foreach(var scenarioItem in Scenario.ScenarioItems)
            {
                var promotion = PromotionDataService.GetProductPromotion(scenarioItem.Product);
                if (promotion == null)
                    totalCalculatedPrice += scenarioItem.Product.UnitPrice * scenarioItem.Quantity;
                else
                {
                    var promotionOccurance = scenarioItem.Quantity / promotion.ProductAndQuantity[scenarioItem.Product.SKU];
                    totalCalculatedPrice += promotionOccurance * promotion.PromotionPrice;

                    var remainingQuantityWithOutPromotion = scenarioItem.Quantity % promotion.ProductAndQuantity[scenarioItem.Product.SKU];
                    totalCalculatedPrice += scenarioItem.Product.UnitPrice * remainingQuantityWithOutPromotion;
                }
            }

            return totalCalculatedPrice;
        }
    }
}
