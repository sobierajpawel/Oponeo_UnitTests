using SDAOponeo.UnitTests.ImplementationModule.Model;

namespace SDAOponeo.UnitTests.ImplementationModule.Calculators
{
    public class ProductPriceCalculator
    {
        private const int FIRST_LEVEL_TOTAL_DISCOUNT = 400;
        private const int SECOND_LEVEL_TOTAL_DISCOUNT = 850;
        private const int QUANTITY_LEVLE_DISCOUNT = 10;

        private const double FIRST_PERCENTAGE_LEVEL = 0.1;
        private const double SECOND_PERCENTAGE_LEVEL = 0.2;

        public double GetPrice(IEnumerable<Product> products) 
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));
            else if (!products.Any())
                throw new ArgumentException(nameof(products));  

            var totalPrice = 0d;
            bool isDiscountApplied = false;

            foreach (var product in products)
            {
                if (product.Quantity >= QUANTITY_LEVLE_DISCOUNT)
                {
                    totalPrice += product.TotalPrice - (product.TotalPrice * FIRST_PERCENTAGE_LEVEL);
                    isDiscountApplied = true;
                }
                else
                {
                    totalPrice += product.TotalPrice;
                }
            }

            if (!isDiscountApplied)
            {
                if (totalPrice > FIRST_LEVEL_TOTAL_DISCOUNT)
                {
                    return totalPrice - (totalPrice * FIRST_PERCENTAGE_LEVEL);
                }
                else if (totalPrice > SECOND_LEVEL_TOTAL_DISCOUNT)
                {
                    return totalPrice - (totalPrice * SECOND_PERCENTAGE_LEVEL);
                }
            }

            return totalPrice;
        }
    }
}
