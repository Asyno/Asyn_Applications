using System;
using System.Xml.XPath;

namespace XMLPathNavigator
{
    class Program
    {
        static void Main(string[] args)
        {
            XPathDocument xPathDoc = new XPathDocument(@"C:\Users\TigerTMS\Desktop\test_C\Personen.xml");
            XPathNavigator navigator = xPathDoc.CreateNavigator();
            navigator.MoveToFirstChild();

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //Personen");
            XPathNodeIterator iterator = navigator.Select("//Personen");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: /Personen");
            iterator = navigator.Select("/Personen");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //Person");
            iterator = navigator.Select("//Person");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //Person[3]");
            iterator = navigator.Select("//Person[3]");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //Personen/*");
            iterator = navigator.Select("//Personen/*");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //Personen/Person/Adresse/@*");
            iterator = navigator.Select("//Personen/Person/Adresse/@*");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //Personen/Person/Adresse/@Ort");
            iterator = navigator.Select("//Personen/Person/Adresse/@Ort");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);
            
            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: Vorname");
            iterator = navigator.Select("Vorname");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //child::Vorname");
            iterator = navigator.Select("//child::Vorname");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: Person[Alter>40 AND Adresse[@Ort=Bonn]]");
            iterator = navigator.Select("Person[Alter>40 AND Adresse[@Ort=Bonn]]");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: Person[starts-width(Vorname,'P')]");
            iterator = navigator.Select("Person[starts-width(Vorname, 'P']");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.WriteLine();
            Console.WriteLine("XPath-Ausdruck: //Zuname[text()='Meier or text()='Schmidt']");
            iterator = navigator.Select("//Zuname[text()='Meier or text()='Schmidt']");
            while (iterator.MoveNext())
                Console.WriteLine(iterator.Current.Value);

            Console.ReadLine();
        }
    }
}
