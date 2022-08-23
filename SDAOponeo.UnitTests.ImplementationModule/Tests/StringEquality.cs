namespace SDAOponeo.UnitTests.ImplementationModule.Tests
{
    public class StringEquality
    {
        public bool IsEqualYourName(string firstName)
        {
            return firstName.ToLowerInvariant() == "paweł";
        }
    }
}
