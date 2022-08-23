using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDAOponeo.UnitTests.ImplementationModule.Calculators;
using SDAOponeo.UnitTests.ImplementationModule.Model;
using System;
using System.Collections.Generic;

namespace SDAOponeo.UnitTests.Tests.Calculators
{
    /*
     * 1) If total price > 400$ 10% discount
     * 2) If total price > 850$ 20$ discount
     * 3) If the quantity of product is 10 and more then add 10% discount to a particular total sum of product
     * 4) Discounts cannot be sum
     */
    [TestClass]
    public class ProductPriceCalculatorTest
    {

        [TestMethod]
        public void Should_Return_Base_Sum_If_There_Are_No_Discounts()
        {
            //Arrange
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Product X",
                    Quantity = 2,
                    UnitPrice = 100
                }
            };
            var totalPrice = 200;
            var productPriceCalculator = new ProductPriceCalculator();

            //Act 
            var resultPrice = productPriceCalculator.GetPrice(products);

            Assert.AreEqual(totalPrice, resultPrice);

        }

        [TestMethod]
        public void Should_Return_Price_Lower_Of_10_Percentage_Discount_If_Total_Price_Higher_Than_400()
        {
            //Arrange
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Product X",
                    Quantity = 4,
                    UnitPrice = 120
                }
            };
            var totalPrice = 432;
            var productPriceCalculator = new ProductPriceCalculator();

            //Act 
            var resultPrice = productPriceCalculator.GetPrice(products);

            Assert.AreEqual(totalPrice, resultPrice);

        }

        [TestMethod]
        public void Should_Return_Price_Lower_Of_20_Percentage_Discount_If_Total_Price_Higher_Than_850()
        {
            //Arrange
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Product X",
                    Quantity = 9,
                    UnitPrice = 100
                }
            };
            var totalPrice = 810;
            var productPriceCalculator = new ProductPriceCalculator();

            //Act 
            var resultPrice = productPriceCalculator.GetPrice(products);

            Assert.AreEqual(totalPrice, resultPrice);

        }


        [TestMethod]
        public void Should_Return_10_Percentage_Discount_Where_One_Of_Products_Is_Ordered_In_10_Qty()
        {
            //Arrange
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Product X",
                    Quantity = 10,
                    UnitPrice = 100
                },
                new Product
                {
                    Name = "Product Y",
                    Quantity = 5,
                    UnitPrice = 5
                }
            };
            var totalPrice = 925;
            var productPriceCalculator = new ProductPriceCalculator();

            //Act 
            var resultPrice = productPriceCalculator.GetPrice(products);

            //Assert
            Assert.AreEqual(totalPrice, resultPrice);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_Throw_ArgumentNullException_When_Collection_Of_Products_Is_Null()
        {
            //Arrange
            List<Product> products = null;
            var totalPrice = 925;
            var productPriceCalculator = new ProductPriceCalculator();

            //Act 
            var resultPrice = productPriceCalculator.GetPrice(products);

            //Assert
            Assert.AreEqual(totalPrice, resultPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_ArgumentException_When_Collection_Of_Products_Is_Empty()
        {
            //Arrange
            List<Product> products = new List<Product>();
            var totalPrice = 925;
            var productPriceCalculator = new ProductPriceCalculator();

            //Act 
            var resultPrice = productPriceCalculator.GetPrice(products);

            //Assert
            Assert.AreEqual(totalPrice, resultPrice);
        }
    }
}
