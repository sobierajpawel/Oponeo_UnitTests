using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAOponeo.UnitTests.ImplementationModule.Tests
{
    public class FibonacciSequence
    {
        //0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, ...
        public int GetElement(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
                return 0;
            else if (index == 1)
                return 1;
            else
                return GetElement(index - 1) + GetElement(index - 2);
        }
    }
}
