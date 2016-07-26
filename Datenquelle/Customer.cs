namespace Datenquelle
{
    public class Customer
    {
        public string Name { get; set; }
        public Cities City { get; set; }
        public Order[] Orders { get; set; }
    }
}
