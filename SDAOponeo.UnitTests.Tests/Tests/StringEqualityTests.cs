using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDAOponeo.UnitTests.ImplementationModule.Tests;

namespace SDAOponeo.UnitTests.Tests.Tests
{
    [TestClass]
    public class StringEqualityTests
    {
        [TestMethod]
        public void Should_Return_True_If_FirstName_Is_Pawel()
        {
            //Arrange
            string firstName = "Paweł";
            var stringEquality = new StringEquality();

            //Act
            bool result = stringEquality.IsEqualYourName(firstName);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
