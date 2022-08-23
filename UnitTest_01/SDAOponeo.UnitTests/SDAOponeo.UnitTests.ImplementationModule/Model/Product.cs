namespace SDAOponeo.UnitTests.ImplementationModule.Model
{
    public class Product
    {
        public string Name { get; set; }
        public double TotalPrice => UnitPrice * Quantity;
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
    }
}
