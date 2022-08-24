namespace SDAOponeo.UnitTests.ImplementationModule.Model
{
    public class Order
    {
        public  Guid Guid { get; set; }
        public  DateTime OrderDateTime { get; set; }
        public  Customer Customer { get; set; }
        public  IEnumerable<Product> OrderLines {get;set;}

        public  string DiscountCode { get; set; }
    }
}
