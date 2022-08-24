using SDAOponeo.UnitTests.ImplementationModule.Calculators;
using SDAOponeo.UnitTests.ImplementationModule.Model;

namespace SDAOponeo.UnitTests.ImplementationModule.Orders
{
    public class OrderProcessor
    {
        private readonly IDiscountCodeService _discountCodeService;
        private readonly ProductPriceCalculator _productPriceCalculator;
        private readonly IOrderValidator _orderValidator;
        public OrderProcessor(ProductPriceCalculator productPriceCalculator, IOrderValidator orderValidator, IDiscountCodeService discountCodeService)
        {
            this._productPriceCalculator = productPriceCalculator ?? throw new ArgumentNullException(nameof(productPriceCalculator));
            this._orderValidator = orderValidator ?? throw new ArgumentNullException(nameof(orderValidator));
            this._discountCodeService = discountCodeService ?? throw new ArgumentNullException(nameof(discountCodeService));
        }

        public ProcessedOrder ProcessOrder(Order order)
        {
            if (!_orderValidator.Validate(order))
            {
                throw new ArgumentException($"Validation exception in {order.Guid}");
            }

            var netTotal = _productPriceCalculator.GetPrice(order.OrderLines);

            if (!string.IsNullOrWhiteSpace(order.DiscountCode))
            {
                var availableCodes = _discountCodeService.GetAvailableCodes();
                if (availableCodes.Any(x => x == order.DiscountCode))
                {
                    if (_discountCodeService.UseCode(order.DiscountCode))
                    {
                        netTotal -= 50;
                    }
                }
            }

            return new ProcessedOrder
            {
                Order = order,
                NetTotal = netTotal,
            };
        }
    }
}
