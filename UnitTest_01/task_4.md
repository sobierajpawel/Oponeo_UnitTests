## Task 4

### Test driven development

1. Create a class ```OrderProduct``` which should contain a few properties like below.

```cs
 public class OrderProduct
    {
        public string Name { get; set; }    
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice => UnitPrice * Quantity;
    }
```

2. Create tests for the following conditions for the products' prices calculator:

- When a sum of the products' prices is higher than 200$ then a 10 percentage discount should be added to the sum.
- When a sum of the products' prices is higher than 400$ then a 20 percentage discount should be added to the sum.
- When a customer ordered more than 10 quantity of the same product, add a 10 percentage discount only to the sum of prices on a particular product.

3. Create implementations for the written tests.

4. Check tests results are correct.

5. Refactor code and think about improving your source code.
