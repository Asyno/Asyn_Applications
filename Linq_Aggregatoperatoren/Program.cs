using Datenquelle;
using System;
using System.Linq;

namespace Linq_Aggregatoperatoren
{
    class Program
    {
        static void Main(string[] args)
        {
            Order[] orders = Service.GetOrders();
            Customer[] customers = Service.GetCustomer();
            Product[] products = Service.GetProducts();

            // Count & LongCount - Zähler
            Console.WriteLine("- Count & LongCount -");

            var anzahl = (from x in orders select x).Count();
            anzahl = orders.Count();

            Console.WriteLine("Anzahl der Bestellungen gesamt = {0}", anzahl);

            var countOrders = from c in customers
                              select new { c.Name, OrderCount = c.Orders.Count() };
            foreach (var item in countOrders)
                Console.WriteLine("{0} - {1}", item.Name, item.OrderCount);

            // Sum - Summiert
            Console.WriteLine();
            Console.WriteLine("- Sum -");

            var allOrders =
                from cust in customers
                from ord in cust.Orders
                join prod in products on ord.ProductID equals prod.ProductID
                select new { cust.Name, ord.ProductID, OrderAmmount = ord.Quantity * prod.Price };

            var summe =
                from cust in customers
                join ord in allOrders
                on cust.Name equals ord.Name into custWithOrd
                select new { cust.Name, TotalSumme = custWithOrd.Sum(s => s.OrderAmmount) };

            foreach (var s in summe)
                Console.WriteLine("Name: {0,-7} Bestellsumme: {1}", s.Name, s.TotalSumme);

            // Min - Max - Average
            Console.WriteLine();
            Console.WriteLine("- Max & Min & Average -");

            var max = (from p in products
                     select p.Price).Max();

            var min = (from p in products
                       select new { p.Price }).Min(x => x.Price);
            Console.WriteLine("Max Price in Products: {0}", max);
            Console.WriteLine("Min Price in Products: {0}", min);

            Console.ReadLine();
        }
    }
}
