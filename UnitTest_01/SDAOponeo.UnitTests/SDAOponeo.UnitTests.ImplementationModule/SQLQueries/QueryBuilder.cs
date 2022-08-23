using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAOponeo.UnitTests.ImplementationModule.SQLQueries
{
    internal class QueryBuilder
    {
        private const string PROCEDURE_NAME = "[procedure_1]";
        private StringBuilder result = new StringBuilder();

        public void AddProperty(string nameProperty, string value)
        {
            result.Append($"{nameProperty} = {value}");
        }

        public string GetExecutionProcedure()
        {
            return "EXEC [procedure_1] @param1 = value1, @param2 = value2";
        }
    }
}
