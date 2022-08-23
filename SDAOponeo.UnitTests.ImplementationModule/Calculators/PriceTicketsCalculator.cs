using SDAOponeo.UnitTests.ImplementationModule.Model;

namespace SDAOponeo.UnitTests.ImplementationModule.Calculators
{
    public class PriceTicketsCalculator
    {
        public double CalculatePrice(double basePrice, Customer customer, DateTime? lastVisitedDateTime)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (customer.Age > 65)
            {
                return basePrice - (basePrice * 0.3);
            }
            else if (customer.Age < 4)
            {
                return 0;
            }

            if (lastVisitedDateTime.HasValue)
            {
                var differnce = DateTime.UtcNow - lastVisitedDateTime.Value;

                if (differnce.Days > 200)
                {
                    return basePrice - (basePrice * 0.5);
                }
            }

            return basePrice;
        }
    }
}
