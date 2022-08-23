using SDAOponeo.UnitTests.ImplementationModule.Model;
using SDAOponeo.UnitTests.ImplementationModule.Providers;

namespace SDAOponeo.UnitTests.ImplementationModule.Calculators
{
    public class DiscountCalculator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public DiscountCalculator(IDateTimeProvider dateTimeProvider)
        {
            this._dateTimeProvider = dateTimeProvider;
        }

        public double CalculatePrice(Product product)
        {
            if (_dateTimeProvider.GetLocalDate().Hour > 10)
                return product.UnitPrice - (product.UnitPrice * 0.1);

            return product.UnitPrice;
        }
    }
}
