## Task 3

### Using DataRow and DateTimeProvider in MSTest
 
1. Create ```PriceCalculator``` which calculate price with added discount depending on a few conditions.

 - 1) When a customer is older than 65 then a price should be lowered by 30%
 - 2) When a customer is younger than 4 then a price is 0
 - 3) When a customer visited last time a place 200 days ago then add 50% discount.
 - Discounts are not connected with each other. Priorities are given by numbers

3. Create unit tests to cover all conditions.

4. Use ```[DataRow]``` attribute to use different parameters in the same tests. 

5. Refactor your code to use external ```DateTimeProvider``` in order to check the last visited date in izolation.
