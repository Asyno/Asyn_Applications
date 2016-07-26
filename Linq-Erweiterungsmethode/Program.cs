using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Erweiterungsmethode
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr = { "Peter", "Uwe", "Willi", "Udo", "Gernot" };
            IEnumerable<string> query = arr.Where(name => name.Length < 4);
            foreach (string item in query)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
