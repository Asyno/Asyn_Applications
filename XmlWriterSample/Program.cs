using System;
using System.Xml;

namespace XmlWriterSample
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  "; // 2 Leerzeichen
            XmlWriter write = XmlWriter.Create(@"C:\Users\TigerTMS\Desktop\test_C\write_personen.xml", settings);
            write.WriteStartDocument();

            // Starttag des Stammelements
            write.WriteStartElement("x", "Personen", "http://www.MyNS.de");
            write.WriteAttributeString("xmlns", "http://www.MyDefaultNS.de");
            write.WriteComment("Die Datei wurde mit XmlWrite erzeugt");

            // Starttag von 'Person'
            write.WriteStartElement("x", "Person", "http://www.MyNS.de");
            write.WriteElementString("x", "Zuname", "http://www.MyNS.de", "Kleynen");
            string prefix = write.LookupPrefix("http://www.MyNS.de");
            write.WriteElementString(prefix, "Vorname", "http://www.MyNS.de", "Peter");

            // Element mit Attributen
            write.WriteStartElement("Adresse");
            write.WriteAttributeString("Ort", "Eifel");
            write.WriteAttributeString("Strasse", "Am Wald 1");
            write.WriteValue("Germany");
            write.WriteEndElement();

            // Endtag von 'Person'
            write.WriteEndElement();

            // Endtag des Stammelements
            write.WriteEndElement();
            write.WriteEndDocument();
            write.Close();
            Console.WriteLine(@"Datei 'C:\Users\TigerTMS\Desktop\test_C\write_personen.xml' erzeugt.");
            Console.ReadLine();
        }
    }
}
