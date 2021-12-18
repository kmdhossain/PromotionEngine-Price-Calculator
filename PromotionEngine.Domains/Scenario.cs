
using System.Collections.Generic;

namespace PromotionEngine.Domains
{
    public class Scenario
    {
        public string ScenarioName { get; set; }
        public  Dictionary<string, ScenarioItem> ScenarioItems { get; set; }
    }
}
