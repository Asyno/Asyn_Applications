using System;
using System.Linq;

namespace Linq_Set_Operatoren
{
    class Program
    {
        static void Main(string[] args)
        {
            // Distinct - Keine Doppelten einträge
            Console.WriteLine();
            Console.WriteLine("- Distinct -");

            string[] cities = { "Aachen", "Köln", "Bonn", "Aachen", "Bonn", "Hof" };
            var liste3 = (from p in cities select p).Distinct();
            foreach (var city in liste3)
                Console.WriteLine(city);

            // Union - zwei Listen verbinden und keine Doppelten einträge
            Console.WriteLine();
            Console.WriteLine("- Union -");

            String[] names = { "Peter", "Willi", "Hans" };

            var listCities = from c in cities select c;
            var listNames = from n in names select n;
            var listComplete = listCities.Union(listNames);
            foreach (var p in listComplete)
                Console.WriteLine(p);

            // Intersect - zwei Listen verbinden, nur einträge die in beiden Listen sind
            Console.WriteLine();
            Console.WriteLine("- Intersect -");

            String[] cities1 = { "Aachen", "Köln", "Bonn", "Aachen", "Frankfurt" };
            String[] cities2 = { "Düsseldorf", "Bonn", "Bremen", "Köln" };

            var listeCities1 = from c in cities1 select c;
            var listeCities2 = from c in cities2 select c;
            var listeComplete = listeCities1.Intersect(listeCities2);

            foreach (var p in listeComplete)
                Console.WriteLine(p);
            // Except
            Console.WriteLine("Except");
            var listeComplete2 = listeCities1.Except(listeCities2);
            foreach (var p in listeComplete2)
                Console.WriteLine(p);

            Console.ReadLine();
        }
    }
}
