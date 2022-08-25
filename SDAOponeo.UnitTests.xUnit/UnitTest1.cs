using Moq;
using SDAOponeo.UnitTests.ImplementationModule.Calculators;
using SDAOponeo.UnitTests.ImplementationModule.Model;
using SDAOponeo.UnitTests.ImplementationModule.Orders;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace SDAOponeo.UnitTests.xUnit
{
    public class OrderProcessorXUnitTests
    {
        private Order? order;
        private readonly string testedDiscountCode = "FDFDT$23";
        private Mock<ProductPriceCalculator>? mockedProductPriceCalculator;

        public OrderProcessorXUnitTests()
        {
            order = new Order
            {
                OrderLines = It.IsAny<IEnumerable<Product>>(),
                Customer = It.IsAny<Customer>(),
                Guid = Guid.NewGuid(),
                OrderDateTime = DateTime.Now,
            };
            mockedProductPriceCalculator = new Mock<ProductPriceCalculator>();
        }

        [Fact]

        public void Should_Throw_Argument_Exception_When_ProductPriceCalculator_Is_Null()
        {
            //Arrange
            var orderValidatorMock = new Mock<IOrderValidator>();
            var discountServiceMock = new Mock<IDiscountCodeService>();

            Assert.Throws<ArgumentNullException>(() => new OrderProcessor(null, orderValidatorMock.Object, discountServiceMock.Object));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_When_OrderValidator_Is_Null()
        {
            //Arrange
            var discountServiceMock = new Mock<IDiscountCodeService>();
            var priceCalculatorMock = new Mock<ProductPriceCalculator>();

            Assert.Throws<ArgumentNullException>(() => new OrderProcessor(priceCalculatorMock.Object, null, discountServiceMock.Object));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_When_DiscountService_Is_Null()
        {
            //Arrange
            var orderValidatorMock = new Mock<IOrderValidator>();
            var priceCalculatorMock = new Mock<ProductPriceCalculator>();

            Assert.Throws<ArgumentNullException>(() => new OrderProcessor(priceCalculatorMock.Object, orderValidatorMock.Object, null));
        }

        [Fact]
        public void Should_Throw_Argument_Exception_When_Validation_Result_Is_False()
        {
            //Arrange
            var orderValidatorMock = new Mock<IOrderValidator>();
            var discountService = new FakeCodesService();

            orderValidatorMock.Setup(x => x.Validate(It.IsAny<Order>())).Returns(false);
            var orderProcessor = new OrderProcessor(mockedProductPriceCalculator.Object, orderValidatorMock.Object, discountService);

            //Act
            var result =
            Assert.Throws<ArgumentException>(() => orderProcessor.ProcessOrder(order));
        }

        [Fact]
        public void Should_Return_The_Same_Price_When_Discount_Code_Is_Not_Given()
        {
            //Arrange
            var orderValidatorMock = new Mock<IOrderValidator>();
            var discountService = new FakeCodesService();
            var testedTotal = 50d;

            mockedProductPriceCalculator.Setup(x => x.GetPrice(It.IsAny<IEnumerable<Product>>())).Returns(100d);
            orderValidatorMock.Setup(x => x.Validate(It.IsAny<Order>())).Returns(true);
            var orderProcessor = new OrderProcessor(mockedProductPriceCalculator.Object, orderValidatorMock.Object, discountService);

            //Act
            var result = orderProcessor.ProcessOrder(order);

            //Assert
            result.NetTotal.ShouldBe(100d);

            orderValidatorMock.Verify(x => x.Validate(It.IsAny<Order>()), Times.Once);
            mockedProductPriceCalculator.Verify(x => x.GetPrice(It.IsAny<IEnumerable<Product>>()), Times.Once);
        }

        [Fact]
        public void Should_Not_Allow_To_Use_The_Same_Discount_Code_Twice()
        {
            //Arrange
            var orderValidatorMock = new Mock<IOrderValidator>();
            var discountService = new FakeCodesService();

            var totalPrice = 100d;
            discountService.AddCode(testedDiscountCode);

            orderValidatorMock.Setup(x => x.Validate(It.IsAny<Order>())).Returns(true);
            mockedProductPriceCalculator.Setup(x => x.GetPrice(It.IsAny<IEnumerable<Product>>())).Returns(totalPrice);
            var orderProcessor = new OrderProcessor(mockedProductPriceCalculator.Object, orderValidatorMock.Object, discountService);
            order.DiscountCode = testedDiscountCode;

            //Act
            var resultOnce = orderProcessor.ProcessOrder(order);
            var resultTwo = orderProcessor.ProcessOrder(order);

            //Assert
            Assert.Equal(resultOnce.NetTotal, totalPrice - 50d);
            Assert.Equal(resultTwo.NetTotal, totalPrice);

            orderValidatorMock.Verify(x => x.Validate(It.IsAny<Order>()), Times.Exactly(2));
            mockedProductPriceCalculator.Verify(x => x.GetPrice(It.IsAny<IEnumerable<Product>>()), Times.Exactly(2));
        }
    }
}