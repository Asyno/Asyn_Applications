using System;
using System.Xml;

namespace XmlReaderSample
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlReader reader = XmlReader.Create(@"C:\Users\Jan\Desktop\Test\test.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        Console.WriteLine("{0,-20}<{1}>", "DEKLARATION", reader.Value);
                        break;
                    case XmlNodeType.CDATA:
                        Console.WriteLine("{0,-20}{1}", "CDATA", reader.Value);
                        break;
                    case XmlNodeType.Whitespace:
                        Console.WriteLine("{0,-20}", "WHITESPACE");
                        break;
                    case XmlNodeType.Comment:
                        Console.WriteLine("{0,-20}<!--{1}-->", "COMMENT", reader.Value);
                        break;
                    case XmlNodeType.Element:
                        if (!reader.IsEmptyElement)
                            Console.WriteLine("{0,-20}<{1}>", "ELEMENT", reader.Name);
                        else
                            Console.WriteLine("{0,-20}<{1} />", "EMPTY_ELEMENT", reader.Name);

                        // Prüfen ob das Element Attribute hat
                        if (reader.HasAttributes)
                        {
                            // Durch die Attribute navigieren
                            while (reader.MoveToNextAttribute())
                                Console.WriteLine("{0,-20}{1}",
                                    "ATTRIBUT", reader.Name + "=" + reader.Value);
                        }
                        break;
                    case XmlNodeType.EndElement:
                        Console.WriteLine("{0,-20}</ {1}>", "END_ELEMENT", reader.Name);
                        break;
                    case XmlNodeType.Text:
                        Console.WriteLine("{0,-20}{1}", "TEXT", reader.Value);
                        break;
                }
            }
            Console.ReadLine();
        }
    }
}
