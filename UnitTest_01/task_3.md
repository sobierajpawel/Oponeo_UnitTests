## Task 3

### Using DataRow in MSTest

1. Create a model class in the implementation module. Name it as ```Customer```.

```cs
 public class Customer
    {
        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
```

2. Create ```PriceCalculator``` which calculate price with added discount depending on a few conditions.

 - When a customer is older than 65 then a price should be lowered by 30%
 - When a customer is younger than 4 then a price is 0
 - When a customer visited last time a place 200 days ago then add 50% discount.

3. Create unit tests to cover all conditions.

4. Use ```[DataRow]``` attribute to use different parameters in the same tests. 
