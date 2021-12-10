using PromotionEngine.Core;
using PromotionEngine.Domains;
using System;
using System.Collections.Generic;
using Xunit;

namespace PromotionEngine.Tests
{
    public class ScenarioServiceTests
    {
        [Fact]
        public void CalculateScenatioTotal_ValidScenarioPassed_ShouldCalculateTotal()
        {
            //Arrange
            Scenario scenario = new Scenario
            {
                ScenarioName = "A",
                ScenarioItems = new List<ScenarioItem>
                {
                    new ScenarioItem
                    {
                       Product = new Product()
                    }
                }
            };

            ScenarioService scenarioService = new ScenarioService(scenario);
            int expectedTotal = -1;

            //Act
            var actutalTotal = scenarioService.CalculateScenatioTotal();

            //Assert
            Assert.Equal(expectedTotal, actutalTotal);

        }
    }
}
