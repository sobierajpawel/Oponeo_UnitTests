using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAOponeo.UnitTests.ImplementationModule.Orders
{
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

    public class FakeCodesService : IDiscountCodeService
    {
        private List<string> availableCodes = new List<string>();

        public IEnumerable<string> GetAvailableCodes()
        {
            return availableCodes;
        }

        public bool UseCode(string code)
        {
            if (availableCodes.Contains(code))
            {
                availableCodes.Remove(code);
                return true;
            }

            return false;
        }

        public void AddCode(string code)
        {
            availableCodes.Add(code);
        }
    }
}
