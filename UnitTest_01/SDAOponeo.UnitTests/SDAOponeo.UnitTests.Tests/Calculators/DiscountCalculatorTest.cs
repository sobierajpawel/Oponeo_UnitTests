using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDAOponeo.UnitTests.ImplementationModule.Calculators;
using SDAOponeo.UnitTests.ImplementationModule.Model;
using SDAOponeo.UnitTests.ImplementationModule.Providers;
using System;

namespace SDAOponeo.UnitTests.Tests.Calculators
{
    [TestClass]
    public class DiscountCalculatorTest
    {
        [TestMethod]
        public void Should_Price_Be_The_Same()
        {
            //Arrange
            var product = new Product
            {
                Name = "Letuce",
                UnitPrice = 10
            };
            var dateTime = new DateTime(1, 1, 1, 9, 0, 0);
            var dateTimeProvider = new TestDateTimeProvider(dateTime);
            var discountCalculator = new DiscountCalculator(dateTimeProvider);

            //Act
            var result = discountCalculator.CalculatePrice(product);

            //Arrange
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Should_Price_Be_Lower_Of_10_Percentage()
        {
            //Arrange
            var product = new Product
            {
                Name = "Letuce",
                UnitPrice = 10
            };
            var dateTime = new DateTime(1, 1, 1, 16, 0, 0);
            var dateTimeProvider = new TestDateTimeProvider(dateTime);
            var discountCalculator = new DiscountCalculator(dateTimeProvider);

            //Act
            var result = discountCalculator.CalculatePrice(product);

            //Arrange
            Assert.AreEqual(9, result);
        }
    }
}
