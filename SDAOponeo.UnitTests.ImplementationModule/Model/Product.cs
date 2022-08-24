namespace SDAOponeo.UnitTests.ImplementationModule.Model
{
    public class Product
    {
        public virtual string Name { get; set; }
        public virtual double TotalPrice => UnitPrice * Quantity;
        public virtual double UnitPrice { get; set; }
        public virtual double Quantity { get; set; }
    }
}
