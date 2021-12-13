using PromotionEngine.Core;
using PromotionEngine.Domains;
using System;
using System.Collections.Generic;
using Xunit;

namespace PromotionEngine.Tests
{
    public class ScenarioServiceTests
    {
        ScenarioItem Factory_ScenarioItem(string sku, float unitPrice, int quantity)
        {
            return new ScenarioItem
            {
                Product = new Product
                {
                    SKU = sku,
                    UnitPrice = unitPrice
                },
                Quantity = quantity
            };
        }

        [Fact]
        public void CalculateScenatioTotal_TestScenarioAPassed_ShouldCalculateTotal()
        {
            //Arrange
            Scenario scenario = new Scenario
            {
                ScenarioName = "A",
                ScenarioItems = new List<ScenarioItem>
                {
                    Factory_ScenarioItem("A", 50, 1),
                    Factory_ScenarioItem("B", 30, 1),
                    Factory_ScenarioItem("C", 20, 1)
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
