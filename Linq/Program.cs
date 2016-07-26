using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons =
            {
                new Person { Name="Maier"  , Age=34 },
                new Person { Name="Müller" , Age=51 },
                new Person { Name="Schmidt", Age=30 },
                new Person { Name="Fischer", Age=25 },
                new Person { Name="Schulz" , Age=67 },
            };

            var query = from pers in persons where pers.Age >= 50 select pers;

            foreach (var item in query)
                Console.WriteLine("{0,-8}{1}", item.Name, item.Age);



            Console.ReadLine();
        }
    }
}
