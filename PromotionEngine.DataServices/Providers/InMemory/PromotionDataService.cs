﻿using PromotionEngine.DataServices.Interfaces;
using PromotionEngine.Domains;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.DataServices.Providers.InMemory
{
    public class PromotionDataService: IPromotionDataService
    {
        public IList<Promotion> GetAllPromotions()
        {
            return new List<Promotion>
            {
                new Promotion {
                    ProductAndQuantity = new Dictionary<string, int>
                    {
                        { "A", 3 }
                    }, PromotionPrice = 130
                },
                new Promotion {
                    ProductAndQuantity = new Dictionary<string, int>
                    {
                        { "B", 2 }
                    }, PromotionPrice = 45
                },
                new Promotion {
                    ProductAndQuantity = new Dictionary<string, int>
                    {
                        { "C", 1 },
                        { "D", 1 }
                    }, PromotionPrice = 30
                },
                new Promotion {
                    ProductAndQuantity = new Dictionary<string, int>
                    {
                        { "E", 3 },
                        { "F", 2 }
                    }, PromotionPrice = 70
                },
                new Promotion {
                    ProductAndQuantity = new Dictionary<string, int>
                    {
                        { "AB", 1 },
                        { "BC", 1 },
                        { "AC", 1 }
                    }, PromotionPrice = 310
                },
            };                          
        }

        /// <summary>
        /// Gets first promotion for product.
        /// Returns null, if no promotion found for product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Promotion GetProductPromotion(Product product)
        {
            //returns null if no promotion
            return GetAllPromotions()
                    .FirstOrDefault(p => p.ProductAndQuantity.Keys.Contains(product.SKU));
        }
    }
}
