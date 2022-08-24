using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SDAOponeo.UnitTests.ImplementationModule.Calculators;
using SDAOponeo.UnitTests.ImplementationModule.Model;
using SDAOponeo.UnitTests.ImplementationModule.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAOponeo.UnitTests.Tests.Orders
{
    [TestClass]
    public class OrderProcessorTests
    {       
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_Argument_Exception_When_Validation_Result_Is_False()
        {
            //Arrange
            var priceCalculator = new Mock<ProductPriceCalculator>();
            var orderValidatorMock = new Mock<IOrderValidator>();    
            var discountService = new FakeCodesService();

            orderValidatorMock.Setup(x=>x.Validate(It.IsAny<Order>())).Returns(false);
            var orderProcessor = new OrderProcessor(priceCalculator.Object, orderValidatorMock.Object, discountService);

            var order = new Order
            {
                OrderLines = It.IsAny<IEnumerable<Product>>(),
                Customer = It.IsAny<Customer>(),
                Guid = Guid.NewGuid(),
                OrderDateTime = DateTime.Now,
            };

            //Act
            var result = orderProcessor.ProcessOrder(order);
        }

        [TestMethod]
        public void Should_Not_Allow_To_Use_The_Same_Discount_Code_Twice()
        {
            //Arrange
            var priceCalculator = new Mock<ProductPriceCalculator>();
            var orderValidatorMock = new Mock<IOrderValidator>();
            var discountService = new FakeCodesService();
            var testedDiscountCode = "FDFDT$23";
            var totalPrice = 100d;
            discountService.AddCode(testedDiscountCode);

            orderValidatorMock.Setup(x => x.Validate(It.IsAny<Order>())).Returns(true);
            priceCalculator.Setup(x => x.GetPrice(It.IsAny<IEnumerable<Product>>())).Returns(totalPrice);
            var orderProcessor = new OrderProcessor(priceCalculator.Object, orderValidatorMock.Object, discountService);

            var order = new Order
            {
                OrderLines = It.IsAny<IEnumerable<Product>>(),
                Customer = It.IsAny<Customer>(),
                Guid = Guid.NewGuid(),
                OrderDateTime = DateTime.Now,
                DiscountCode = testedDiscountCode
            };

            //Act
            var resultOnce = orderProcessor.ProcessOrder(order);
            var resultTwo = orderProcessor.ProcessOrder(order);

            //Assert
            Assert.AreEqual(resultOnce.NetTotal, totalPrice - 50d);
            Assert.AreEqual(resultTwo.NetTotal, totalPrice);    
        }
    }
}
