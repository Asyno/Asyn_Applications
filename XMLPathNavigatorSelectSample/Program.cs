using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace XMLPathNavigatorSelectSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string trenner = new string('-', 45);
            XPathDocument doc = new XPathDocument(@"C:\Users\Johannes_2\Desktop\test\Personen.xml");
            XPathNavigator navigator = doc.CreateNavigator();
            navigator.MoveToChild("Personen", "");
            navigator.MoveToChild("Person", "");

            // Alle Untergeordneten Elemente einer 'Person'
            XPathNodeIterator descendant =
                navigator.SelectDescendants(""
                , "", false);
            Console.WriteLine("Alle untergeordneten Elemente von 'Person':");
            Console.WriteLine(trenner);
            while (descendant.MoveNext())
                Console.WriteLine(descendant.Current.Name);
            
            // Alle übergeordneten Elemenete von 'Zuname'
            navigator.MoveToChild("Zuname", "");
            XPathNodeIterator ancestors = navigator.SelectAncestors("", "", false);
            Console.WriteLine("\nÜbergeordnete Elemente von 'Surname'");
            Console.WriteLine(trenner);
            while (ancestors.MoveNext())
                Console.WriteLine(ancestors.Current.Name);

            Console.ReadLine();
        }
    }
}
