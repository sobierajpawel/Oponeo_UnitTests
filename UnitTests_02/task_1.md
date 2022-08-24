## Task 1

### Use Moq to create mock objects dummy, spy, mock objects...

1. Create a solution in Visual Studio with a class library project. Name solution as 'Oponeo.Orders', and project name as 'Oponeo.Orders.Domain'.

2. Add `Product` `Customer` and `Order` classes to your domain model.

```cs
 public class Product
    {
        public string Name { get; set; }
        public double TotalPrice => UnitPrice * Quantity;
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
    }
```

```cs
 public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public int Age { get; set; }    
    }
```

```cs
 public class Order
    {
        public Guid Guid { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Product> OrderLines {get;set;}

        public string DiscountCode { get; set; }
    }
```

3. Add a library project to the solution. Name it `Oponeo.Orders.BusinessLogic`. Add `ProductPriceCalculator` to the project.

```cs
 public class ProductPriceCalculator
    {
        private const int FIRST_LEVEL_TOTAL_DISCOUNT = 400;
        private const int SECOND_LEVEL_TOTAL_DISCOUNT = 850;
        private const int QUANTITY_LEVLE_DISCOUNT = 10;

        private const double FIRST_PERCENTAGE_LEVEL = 0.1;
        private const double SECOND_PERCENTAGE_LEVEL = 0.2;

        public virtual double GetPrice(IEnumerable<Product> products) 
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
```

4. Create a class named `OrderValidator`.

```cs
public interface IOrderValidator
    {
        bool Validate(Order order);
    }

    public class OrderValidator : IOrderValidator
    {
        public bool Validate(Order order)
        {
            if (order.OrderLines == null || !order.OrderLines.Any())
            {
                return false;
            }
            else if (order.Customer == null)
            {
                return false;
            }

            return true;
        }
    }
```

5. Add an implementation of `DiscountCodesService`.

```cs
  public interface IDiscountCodeService
    {
        IEnumerable<string> GetAvailableCodes();
        bool UseCode(string code);
    }

    public class DiscountCodesService : IDiscountCodeService
    {
        public IEnumerable<string> GetAvailableCodes()
        {
            throw new NotImplementedException();
        }

        public bool UseCode(string code)
        {
            throw new NotImplementedException();
        }
    }
```

6. Add an implementation of `ProcessedOrder`.

```cs
  public class ProcessedOrder
    {
        public Order Order { get; set; }

        public double NetTotal { get; set; }
    }
```

7. Add the implementation of order processor.

```cs
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
```
8. Create `MS Test Library project` name it Oponeo.Orders.UnitTests.Moq

9. Writes tests using `Moq` which covers all scenarious including:

- using the same discount code twice (it should be available only once, use `fake` class to do that
- creating object when a one of the parameters and you expect exception
