using PromotionEngine.Domains;
using System.Collections.Generic;

namespace PromotionEngine.Products
{
    public class PromotionDataService
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

            return null;
        }
    }
}
