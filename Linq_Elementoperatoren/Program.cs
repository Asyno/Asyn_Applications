using System;
using System.Linq;
using Datenquelle;
using System.Collections.Generic;

namespace Linq_Elementoperatoren
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = Service.GetProducts();
            Order[] orders = Service.GetOrders();
            Customer[] customers = Service.GetCustomer();

            // First - FirstOrDefault
            // First liefert immer das erste ergebnis einer Liste
            // FirstOrDefault lieffert das erste oder den Default wert (0, null)
            Console.WriteLine("- First & FirstOrDefault -");
            Console.WriteLine();
            Console.WriteLine("First item from Products");
            var result = products.Select(s => new { s.ProductName, s.Price }).First();
            Console.WriteLine("{0,-7}{1}",result.ProductName, result.Price);

            Console.WriteLine();
            Console.WriteLine("First item from products with price < 10");
            var result2 = products.Where(w => w.Price < 10)
                                    .Select(s => new { s.ProductName, s.Price }).First();
            Console.WriteLine("{0,-8}{1}", result2.ProductName, result2.Price);

            Console.WriteLine();
            Console.WriteLine("FirstOrDefault item from products with price < 1");
            var result3 = products.Where(w => w.Price < 1)
                                    .Select(s => new { s.ProductName, s.Price })
                                    .FirstOrDefault();
            if (result3 == null)
                Console.WriteLine("Kein Element entspricht der Bedingung");
            else
                Console.WriteLine("{0,-8}{1}", result3.ProductName, result3.Price);

            // Last & LastOrDefault
            // Last liefert immer das letzte ergebnis einer Liste
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("- Last & LastOrDefault -");

            var result4 = products.Where(w => w.Price < 5)
                                    .Select(s => new { s.ProductName, s.Price })
                                    .LastOrDefault();
            if (result4 == null)
                Console.WriteLine("Kein Element entspricht der Bedingung.");
            else
                Console.WriteLine("{0,-8}{1}", result4.ProductName, result4.Price);

            // Single & SingleOrDefault
            // Single liefert immer ein einzelnes ergebnis, wenn nach einem eindeutigen Wert gesucht wird
            // werden zwei oder mehr ergebnise gefunden, wird eine Exception geworfen.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("- Single & SingleOrDefault -");

            var result5 = products.Where(w => w.ProductID == 2)
                                    .Select(s => new { s.ProductID, s.ProductName })
                                    .SingleOrDefault();
            Console.WriteLine("{0,-4}{1}", result5.ProductID, result5.ProductName);

            // ElementAt & ElementAtOrDefault
            // ElementAt liefert immer das Element an der angegebenen Position in der Liste
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("- ElementAt & ElementAtOrDefault -");

            var result6 = products.Select(s => new { s.ProductID, s.ProductName })
                                    .ElementAt(3);
            Console.WriteLine("{0,-4}{1}", result6.ProductID, result6.ProductName);

            // DefaultIfEmpty
            // DefaultIfEmpty gibt Default<T> oder einen vorgegebenen Wert zurück, wenn die Liste leer ist
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("- DefaultIfEmpty -");

            List<String> list = new List<String>();
            //list.Add("Uwe");
            //list.Add("Peter");

            foreach (String t in list.DefaultIfEmpty("leer"))
                Console.WriteLine(t);

            // ToList & ToArray
            // To List & ToArray kopieren ein ergebnis sofort in eine Liste oder einem Array
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("- ToList & ToArray -");

            IEnumerable<String> querry = customers.Select(s => s.Name )
                                                    .ToArray();
            foreach (String t in querry)
                Console.WriteLine(t);

            List<String> querry2 = customers.Select(s => s.Name)
                                                .ToList();
            foreach (String t in querry2)
                Console.WriteLine(t);

            Console.ReadLine();
        }
    }
}
