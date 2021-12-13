﻿using PromotionEngine.Domains;
using System.Collections.Generic;

namespace PromotionEngine.Products
{
    public class ProductDataService
    {
        public IList<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product { SKU = "A", UnitPrice = 50 },
                new Product { SKU = "B", UnitPrice = 30 },
                new Product { SKU = "C", UnitPrice = 20 },
                new Product { SKU = "D", UnitPrice = 15 }
            };
        }

        public Promotion GetProductPromotion(Product product)
        {
            //returns null if no promotion

            return null;
        }
    }
}
