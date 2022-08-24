using SDAOponeo.UnitTests.ImplementationModule.Model;

namespace SDAOponeo.UnitTests.ImplementationModule.Orders
{
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
}
