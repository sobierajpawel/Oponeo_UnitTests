using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDAOponeo.UnitTests.ImplementationModule.Calculators;
using SDAOponeo.UnitTests.ImplementationModule.Model;
using System;

namespace SDAOponeo.UnitTests.Tests.Calculators
{
    [TestClass]
    public class PriceTicketsCalculatorTests
    {
        [TestMethod]
        [DataRow(65)]
        [DataRow(64)]
        [DataRow(4)]
        [DataRow(5)]
        public void Should_Return_Base_Price_If_Customer_Is_Younger_Than_65_And_Older_Than_4_And_Visited_Last_Time_Shorter_Than_200_Days(int age)
        {
            //Arrange
            var customer = new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Age = age
            };
            var priceTicketsCalculator = new PriceTicketsCalculator();
            double basePrice = 20;
            DateTime lastVisitedDateTime = new DateTime(2022, 7, 1, 13, 0, 0);

            //Act
            var totalPrice = priceTicketsCalculator.CalculatePrice(basePrice, customer, lastVisitedDateTime);

            //Assert
            Assert.AreEqual(basePrice, totalPrice);
        }

        //[TestMethod]
        //[DataRow(66)]
        //[DataRow(67)]
        //[DataRow(73)]
        //public void Should_Return_Price_Lower_Of_30_Percentage_If_Customer_Is_Older_Than_65(int age)
        //{
        //    //Arrange
        //    var customer = new Customer
        //    {
        //        FirstName = "Jan",
        //        LastName = "Kowalski",
        //        Age = age
        //    };
        //    var priceTicketsCalculator = new PriceTicketsCalculator();
        //    double basePrice = 20;
        //    DateTime lastVisitedDateTime = new DateTime(2022, 7, 1, 13, 0, 0);

        //    //Act
        //    var totalPrice = priceTicketsCalculator.CalculatePrice(basePrice, customer, lastVisitedDateTime);

        //    //Assert
        //    Assert.AreEqual(14, totalPrice);
        //}

        [TestMethod]
        [DataRow(3)]
        [DataRow(2)]
        [DataRow(1)]
        public void Should_Return_Price_Zero_If_Customer_Is_Younger_Than_4(int age)
        {
            //Arrange
            var customer = new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Age = age
            };
            var priceTicketsCalculator = new PriceTicketsCalculator();
            double basePrice = 20;
            DateTime lastVisitedDateTime = new DateTime(2022, 7, 1, 13, 0, 0);

            //Act
            var totalPrice = priceTicketsCalculator.CalculatePrice(basePrice, customer, lastVisitedDateTime);

            //Assert
            Assert.AreEqual(0, totalPrice);
        }

        [TestMethod]
        public void Should_Return_Price_Lower_Of_50_Percentage_If_Customer_Is_Between_4_65_Last_Visited_Was_Later_Than_200Days()
        {
            //Arrange
            var customer = new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Age = 33
            };
            var priceTicketsCalculator = new PriceTicketsCalculator();
            double basePrice = 20;
            DateTime lastVisitedDateTime = new DateTime(2021, 7, 1, 13, 0, 0);

            //Act
            var totalPrice = priceTicketsCalculator.CalculatePrice(basePrice, customer, lastVisitedDateTime);

            //Assert
            Assert.AreEqual(10, totalPrice);
        }

        [TestMethod]
        public void Should_Return_Base_Price_If_Customer_Is_Between_4_65_And_It_Never_Visited_Place()
        {
            //Arrange
            var customer = new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Age = 33
            };
            var priceTicketsCalculator = new PriceTicketsCalculator();
            double basePrice = 20;
            DateTime? lastVisitedDateTime = null;

            //Act
            var totalPrice = priceTicketsCalculator.CalculatePrice(basePrice, customer, lastVisitedDateTime);

            //Assert
            Assert.AreEqual(20, totalPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_Return_ArgumentNullException_When_Customer_Is_Null()
        {
            //Arrange
            Customer customer = null;
            var priceTicketsCalculator = new PriceTicketsCalculator();
            double basePrice = 20;
            DateTime? lastVisitedDateTime = null;

            //Act
            var totalPrice = priceTicketsCalculator.CalculatePrice(basePrice, customer, lastVisitedDateTime);

            //Assert
            Assert.Fail("An exception should be thrown before");
        }
    }
}
