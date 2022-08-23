namespace SDAOponeo.UnitTests.ImplementationModule.Tests
{
    public class FizzBuzzService
    {
        public string Process(int value)
        {
            bool fizz = value % 3 == 0;
            bool buzz = value % 5 == 0;

            if (fizz && buzz)
                return "FizzBuzz";
            else if (fizz)
                return "Fizz";
            else if (buzz)
                return "Buzz";
            else
                return value.ToString();
        }
    }
}
