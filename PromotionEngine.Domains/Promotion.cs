using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Domains
{
    public class Promotion
    {
        public Dictionary<string, int> ProductAndQuantity { get; set; }
        public double PromotionPrice { get; set; }
    }
}
