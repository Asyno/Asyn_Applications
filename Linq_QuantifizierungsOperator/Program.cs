using Datenquelle;
using System;
using System.Linq;

namespace Linq_QuantifizierungsOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = Service.GetProducts();
            Order[] orders = Service.GetOrders();
            Customer[] customers = Service.GetCustomer();

            // Any & All
            Console.WriteLine("- Any & All -");

            bool result = (from cust in customers
                           from ord in cust.Orders
                           where cust.Name == "Willi"
                           select new { ord.ProductID })
                           .Any(ord => ord.ProductID == 7);
            if (result)
                Console.WriteLine("ProductID = 7 ist enthalten");
            else
                Console.WriteLine("ProductID = 7 ist nicht enthalten");

            result = (from prod in products
                      select prod).All(p => p.Price > 3);

            if (result)
                Console.WriteLine("Prices in Products are > 3");
            else
                Console.WriteLine("Not all Prices in Products are > 3");

            Console.ReadLine();
        }
    }
}
