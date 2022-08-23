using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDAOponeo.UnitTests.ImplementationModule.Tests;
using System;

namespace SDAOponeo.UnitTests.Tests.Tests
{
    [TestClass]
    public class FibonacciSequenceTests
    {
        [TestMethod]
        public void Should_Return_Element_Of_Sequence_If_Index_Is_Given()
        {
            //Arrange
            var fibonnaciSequence = new FibonacciSequence();
            int index = 9;

            //Act 
            var result = fibonnaciSequence.GetElement(index);

            //Assert
            Assert.AreEqual(34, result);
        }

        [TestMethod]
        public void Should_Throw_ArgumentOutOfRangeException_When_Index_Is_Below_Zero_NotRecommend_Version()
        {
            //Arrange
            int index = -1;
            var fibonacciSequence = new FibonacciSequence();

            try
            {
                //Act
                var result = fibonacciSequence.GetElement(index);

                //Assert
                Assert.Fail("An exception should be thrown before this line");
            }
            catch(ArgumentOutOfRangeException ae)
            {
                Assert.IsTrue(ae.Message.Contains("index"));
            }
            catch (Exception ex)
            {
                Assert.Fail($"Different exception should be thrown {ex.GetType().ToString()}");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Should_Throw_ArgumentOutOfRangeException_When_Index_Is_Below_Zero_Recommend_Version()
        {
            //Arrange
            int index = -1;
            var fibonacciSequence = new FibonacciSequence();

            //Act
            var result = fibonacciSequence.GetElement(index);

            //Assert
            Assert.Fail("An exception should be thrown but it has not");
        }
    }
}
