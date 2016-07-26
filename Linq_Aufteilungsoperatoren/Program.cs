using System;
using System.Linq;
using Datenquelle;

namespace Linq_Aufteilungsoperatoren
{
    class Program
    {
        static void Main(string[] args)
        {
            Order[] orders = Service.GetOrders();
            Customer[] customers = Service.GetCustomer();
            Product[] products = Service.GetProducts();

            // Take & TakeWhile
            Console.WriteLine("- Take & TakeWhile -");

            var result = products.Take(3);
            foreach (var prod in result)
                Console.WriteLine(prod.ProductName);

            var result2 = products.Where(prod => prod.Price > 3).Take(3)
                                    .Select(p => new { p.ProductName, p.Price });
            foreach (var item in result2)
                Console.WriteLine("{0,-7}{1}", item.ProductName, item.Price);

            // TakeWhile
            Console.WriteLine();
            Console.WriteLine("TakeWhile");
            var result3 = products.Select(p => new { p.ProductName, p.Price })
                                    .TakeWhile(n => n.Price > 3);
            foreach (var item in result3)
                Console.WriteLine("{0,-7}{1}", item.ProductName, item.Price);

            // Skip & SkipWhile
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("- Skip & SkipWhile -");

            var result4 = products.Select(p => new { p.ProductName, p.Price })
                                    .Skip(2);
            foreach (var item in result4)
                Console.WriteLine("{0,-7}{1}", item.ProductName, item.Price);

            Console.WriteLine();
            Console.WriteLine("SkipWhile");
            var result5 = products.Select(p => new { p.ProductName, p.Price })
                                    .SkipWhile(s => s.Price > 3);
            foreach (var p in result5)
                Console.WriteLine("{0,-7}{1}", p.ProductName, p.Price);

            Console.ReadLine();
        }
    }
}
