using Datenquelle;
using System;
using System.Linq;

namespace Linq_extended
{
    class Program
    {
        static void Main(string[] args)
        {
            // Customer Name length < 6
            Console.WriteLine("Customer Name length < 6");

            Customer[] customers = Service.GetCustomer();
            var cust = from customer in customers
                       where customer.Name.Length < 6
                       select new { customer.Name, customer.City };
            foreach (var item in cust)
                Console.WriteLine("Name: {0}, Ort: {1}", item.Name, item.City);

            // Customer Name length > 6
            Console.WriteLine();
            Console.WriteLine("Customer Name length > 6");

            cust = customers
                    .Where(customer => customer.Name.Length > 6)
                    .Select(c => new { c.Name, c.City });

            foreach (var item in cust)
                Console.WriteLine("Name: {0}, Ort: {1}", item.Name, item.City);

            // From Customer name == Hans -> Order Quantity > 6 
            Console.WriteLine();
            Console.WriteLine("From Customer name == Hans -> Order Quantity > 6 ");

            var query = customers
                        .Where(c => c.Name == "Hans")
                        .SelectMany(c => c.Orders)
                        .Where(order => order.Quantity > 6)
                        .Select(order => new { order.OrderID, order.ProductID });
            foreach (var item in query)
                Console.WriteLine("Order ID: {0}, Product ID: {1}", item.OrderID, item.ProductID);

            // From Order where Quantity > 3 and index < 10
            Console.WriteLine();
            Console.WriteLine("From Order where Quantity > 3 and index < 10");

            Order[] orders = Service.GetOrders();
            var query2 = orders
                    .Where((order, index) => order.Quantity > 3 && index < 10)
                    .Select(ord => new { ord.OrderID, ord.ProductID, ord.Quantity });
            foreach (var item in query2)
                Console.WriteLine("{0,-5}{1,-5}{2}", item.OrderID, item.ProductID, item.Quantity);

            // Orders orderby Quantity
            Console.WriteLine();
            Console.WriteLine("Orders orderby Quantity");

            orders = Service.GetOrders();
            var result2 = orders
                            .OrderByDescending(order => order.Quantity)
                            .ThenByDescending(order => order.Shipped)
                            .Select(order => new { order.OrderID, order.Quantity, order.Shipped })
                            .Reverse();
            foreach (var item in result2)
                Console.WriteLine("ID: {0,-3}Menge: {1,-4}Gelifert: {2}", item.OrderID, item.Quantity, item.Shipped);

            // Gruppieren mit "GroupBy"
            Console.WriteLine();
            Console.WriteLine("Gruppieren mit 'GroupBY'");

            var result3 = customers
                            .GroupBy(cust2 => cust2.City);
            foreach (IGrouping<Cities, Customer> temp in result3)
            {
                Console.WriteLine(new String('=', 40));
                Console.WriteLine("Stadt: {0}", temp.Key);
                Console.WriteLine(new String('-', 40));
                foreach (var item in temp)
                    Console.WriteLine("       {0}", item.Name);
            }

            // Verknüpfung mit 'Join'
            Console.WriteLine();
            Console.WriteLine("Verknüpfung mit 'Join'");

            Product[] products = Service.GetProducts();

            var liste = orders
                        .Join(products,
                                ord => ord.ProductID,
                                prod => prod.ProductID, (a, b) => new
                                {
                                    a.OrderID,
                                    b.ProductName,
                                    a.ProductID,
                                    b.Price,
                                    a.Quantity
                                });

            foreach (var m in liste)
                Console.WriteLine("Order: {0,-3} Product: {4,-9} ProductID: {1} Menge: {2} Preis: {3}",
                    m.OrderID, m.ProductID, m.Quantity, m.Price, m.ProductName);

            // Verknüpfung mit 'GroupJoin'
            Console.WriteLine();
            Console.WriteLine("Verknüpfung mit 'GroupJoin'");

            var liste2 = products
                          .GroupJoin(customers.SelectMany(cust3 => cust3.Orders),
                            prod => prod.ProductID,
                            ord => ord.ProductID,
                                (a, b) => new { a.ProductID, a.ProductName, Orders = b });

            foreach (var t in liste2)
            {
                Console.WriteLine("Product: {0,-3} - {1}", t.ProductID, t.ProductName);
                foreach (var order in t.Orders)
                    Console.WriteLine("   OrderID: {0}", order.OrderID);
            }

            Console.ReadLine();
        }
    }
}
