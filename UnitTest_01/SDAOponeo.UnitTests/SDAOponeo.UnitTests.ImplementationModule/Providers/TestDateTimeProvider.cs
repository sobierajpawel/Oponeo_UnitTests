namespace SDAOponeo.UnitTests.ImplementationModule.Providers
{
    public class TestDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _testedDateTime;
        public TestDateTimeProvider(DateTime testedDateTime)
        {
            this._testedDateTime = testedDateTime;
        }

        public DateTime GetLocalDate()
        {
            return _testedDateTime;
        }
    }
}
