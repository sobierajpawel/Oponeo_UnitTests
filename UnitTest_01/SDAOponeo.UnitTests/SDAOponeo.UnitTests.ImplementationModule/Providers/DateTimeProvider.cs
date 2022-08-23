namespace SDAOponeo.UnitTests.ImplementationModule.Providers
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetLocalDate()
        {
            return DateTime.Now;
        }
    }
}
