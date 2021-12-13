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
                       Product = new Product
                       {
                           SKU = "A",
                           UnitPrice = 50
                       },
                       Quantity = 1
                    },
                    new ScenarioItem
                    {
                       Product = new Product
                       {
                           SKU = "B",
                           UnitPrice = 30
                       },
                       Quantity = 1
                    },
                    new ScenarioItem
                    {
                       Product = new Product
                       {
                           SKU = "C",
                           UnitPrice = 20
                       },
                       Quantity = 1
                    }
                }
            };

            ScenarioService scenarioService = new ScenarioService(scenario);
            int expectedTotal = 100;

            //Act
            var actutalTotal = scenarioService.CalculateScenatioTotal();

            //Assert
            Assert.Equal(expectedTotal, actutalTotal);

        }
    }
}
