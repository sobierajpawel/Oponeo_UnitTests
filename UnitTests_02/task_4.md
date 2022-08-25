## Task 4

### Benchmark DotNet

1. Create a project of the console application, name it as 'Oponeo.Orders.BenchmarkDotNet'

2. Create a class which has a three methods to check if string contains numbers, use e.g. different approaches like:

- using `Regex`
- using `Double.TryParse`
- using `LINQ and All() method`

3. Add `[Benchmark]` as well as `[Arguments]` above method signature. 

4. Add arguments above class signature.

```cs
  [SimpleJob(RunStrategy.ColdStart, targetCount: 1000)]
  [MinColumn, MaxColumn, MeanColumn, MedianColumn]
 ```
 
 5. Use this instruction in the main method.
 
 ```cs
 var summary = BenchmarkRunner.Run<StringDigitComparator>();
 ```
 
 6. Resolve all issues e.g. with using `Release` mode instead of `Debug`. Try to run benchmarking correctly. You should receive a proper result in the console window.
