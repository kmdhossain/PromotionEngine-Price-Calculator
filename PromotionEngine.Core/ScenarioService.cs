using PromotionEngine.Domains;
using System;

namespace PromotionEngine.Core
{
    public class ScenarioService
    {
        Scenario Scenario { get; }

        public ScenarioService(Scenario scenario)
        {
            Scenario = scenario;
        }

        public int CalculateScenatioTotal()
        {
            //TODO: complete the algorithm

            return 0;//TDD approach: initially would be failed.
        }
    }
}
