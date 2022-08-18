## Task 1

### Handling exceptions in MSTests

1. In the implementation project create your own implementation of the fibonacci sequence. Just in case below you can find a implementation that you can use. Please try to use your own, though.

```cs
 public class FibonacciSequence
    {
        public int GetFibonacciNumber(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");

            if (index == 0)
                return 1;
            else if (index == 1)
                return 1;
            else
                return GetFibonacciNumber(index - 1) + GetFibonacciNumber(index - 2);
        }
    }
```

2. In the unit tests project write tests to cover all cases except this one when ```index < 0```.

3. Write a unit test which uses ``` try catch ``` to cover a case when exception is thrown.

4. Write a unit tests which uses ``` ExpectedException ``` attribute to cover the same case as in the point 3.
