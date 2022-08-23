using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDAOponeo.UnitTests.ImplementationModule.Tests;

namespace SDAOponeo.UnitTests.Tests.Tests
{
    [TestClass]
    public class FizzBuzzServiceTest
    {
        [TestMethod]
        public void Should_Return_FizzBuzz_When_Value_Mod_3_And_5()
        {
            //Arrange
            var fizzBuzzService = new FizzBuzzService();
            int value = 15;

            //Act
            string result = fizzBuzzService.Process(value);

            //Assert
            Assert.AreEqual("FizzBuzz", result);
        }

        [TestMethod]
        public void Should_Return_Fizz_When_Value_Mod_3()
        {
            //Arrange
            var fizzBuzzService = new FizzBuzzService();
            int value = 27;

            //Act
            string result = fizzBuzzService.Process(value);

            //Assert 
            Assert.AreEqual("Fizz", result);
        }
        
        [TestMethod]
        public void Should_Return_Buzz_When_Value_Mod_5()
        {
            //Arrange
            var fizzBuzzService = new FizzBuzzService();
            int value = 10;

            //Act
            string result = fizzBuzzService.Process(value);

            //Assert 
            Assert.AreEqual("Buzz", result);
        }

        [TestMethod]
        public void Should_Return_Value_When_Value_Not_Mod_5_Or_3()
        {
            //Arrange
            var fizzBuzzService = new FizzBuzzService();
            int value = 22;

            //Act
            string result = fizzBuzzService.Process(value);

            //Assert 
            Assert.AreEqual(value.ToString(), result);
        }
    }
}
