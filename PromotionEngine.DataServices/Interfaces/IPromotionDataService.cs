using PromotionEngine.Domains;
using System.Collections.Generic;

namespace PromotionEngine.DataServices.Interfaces
{
    public interface IPromotionDataService
    {
        IList<Promotion> GetAllPromotions();
        Promotion GetProductPromotion(Product product);
    }
}
