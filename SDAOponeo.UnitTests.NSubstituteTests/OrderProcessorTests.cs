using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SDAOponeo.UnitTests.ImplementationModule.Calculators;
using SDAOponeo.UnitTests.ImplementationModule.Model;
using SDAOponeo.UnitTests.ImplementationModule.Orders;
using System;
using System.Collections.Generic;

namespace SDAOponeo.UnitTests.NSubstituteTests
{
    [TestClass]
    public class OrderProcessorTests
    {
        [TestInitialize]
        public void Initialize()
        {
            order = new Order
            {
                Guid = Guid.NewGuid(),
                OrderDateTime = DateTime.Now,
            };
            mockedProductPriceCalculator = Substitute.For<ProductPriceCalculator>();
        }

        [TestMethod]
        public void Should_Not_Allow_To_Use_The_Same_Discount_Code_Twice()
        {
            //Arrange
            var orderValidatorMock = Substitute.For<IOrderValidator>();
            var discountService = new FakeCodesService();
            discountService.AddCode(testedDiscountCode);

            var totalPrice = 100d;      

            orderValidatorMock.Validate(Arg.Any<Order>()).Returns(true);
            mockedProductPriceCalculator.GetPrice(Arg.Any<IEnumerable<Product>>()).Returns(totalPrice);
           
            var orderProcessor = new OrderProcessor(mockedProductPriceCalculator, orderValidatorMock, discountService);
            order.DiscountCode = testedDiscountCode;

            //Act
            var resultOnce = orderProcessor.ProcessOrder(order);
            var resultTwo = orderProcessor.ProcessOrder(order);

            //Assert
            Assert.AreEqual(resultOnce.NetTotal, totalPrice - 50d);
            Assert.AreEqual(resultTwo.NetTotal, totalPrice);

            orderValidatorMock.Received().Validate(Arg.Any<Order>());
            mockedProductPriceCalculator.Received().GetPrice(Arg.Any<IEnumerable<Product>>());
        }

        [TestMethod]
        public void Should_Return_The_Same_Price_When_Discount_Code_Is_Not_Given()
        {
            //Arrange
            var orderValidatorMock = Substitute.For<IOrderValidator>();
            var discountService = new FakeCodesService();

            orderValidatorMock.Validate(Arg.Any<Order>()).Returns(true);
            mockedProductPriceCalculator.GetPrice(Arg.Any<IEnumerable<Product>>()).Returns(100d);
            
            var orderProcessor = new OrderProcessor(mockedProductPriceCalculator, orderValidatorMock, discountService);

            //Act
            var result = orderProcessor.ProcessOrder(order);

            //Assert
            Assert.AreEqual(100d, result.NetTotal);

            orderValidatorMock.Received().Validate(Arg.Any<Order>());
            mockedProductPriceCalculator.Received().GetPrice(Arg.Any<IEnumerable<Product>>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_Throw_Argument_Exception_When_DiscountService_Is_Null()
        {
            //Arrange
            var orderValidatorMock = Substitute.For<IOrderValidator>();
            var priceCalculatorMock = Substitute.For<ProductPriceCalculator>();

            var orderProcessor = new OrderProcessor(priceCalculatorMock, orderValidatorMock, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_Throw_Argument_Exception_When_OrderValidator_Is_Null()
        {
            //Arrange
            var discountServiceMock = Substitute.For<IDiscountCodeService>();
            var priceCalculatorMock = Substitute.For<ProductPriceCalculator>();

            var orderProcessor = new OrderProcessor(priceCalculatorMock, null, discountServiceMock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_Throw_Argument_Exception_When_ProductPriceCalculator_Is_Null()
        {
            //Arrange
            var orderValidatorMock = Substitute.For<IOrderValidator>();
            var discountServiceMock = Substitute.For<IDiscountCodeService>();

            var orderProcessor = new OrderProcessor(null, orderValidatorMock, discountServiceMock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_Argument_Exception_When_Validation_Result_Is_False()
        {
            //Arrange
            var orderValidatorMock = Substitute.For<IOrderValidator>();
            var discountService = new FakeCodesService();

            orderValidatorMock.Validate(Arg.Any<Order>()).Returns(false);
            var orderProcessor = new OrderProcessor(mockedProductPriceCalculator, orderValidatorMock, discountService);

            //Act
            var result = orderProcessor.ProcessOrder(order);
        }

        private readonly string testedDiscountCode = "FDFDT$23";
        private ProductPriceCalculator? mockedProductPriceCalculator;
        private Order? order;
    }
}