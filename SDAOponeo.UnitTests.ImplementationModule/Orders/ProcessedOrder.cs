using SDAOponeo.UnitTests.ImplementationModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAOponeo.UnitTests.ImplementationModule.Orders
{
    public class ProcessedOrder
    {
        public Order Order { get; set; }

        public double NetTotal { get; set; }
    }
}
