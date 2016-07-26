using System;
using System.Collections.Generic;
using System.Xml;

namespace XmlReaderToObject
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlReader reader = XmlReader.Create(@"C:\Users\TigerTMS\Desktop\test_C\Personen.xml");
            List<Person> liste = new List<Person>();
            Person person = null;
            Adresse adresse = null;

            while (reader.Read())
            {
                // Prüfen, ob es sich aktuell um ein Element handelt
                if (reader.NodeType == XmlNodeType.Element)
                {
                    // Alle relevanten Elemente untersuchen
                    switch (reader.Name)
                    {
                        case "Person":
                            // Neue Person erzeuchen und in Liste eintragen
                            person = new Person();
                            liste.Add(person);
                            break;
                        case "Vorname":
                            person.Firstname = reader.ReadString();
                            break;
                        case "Zuname":
                            person.Lastname = reader.ReadString();
                            break;
                        case "Alter":
                            person.Alter = reader.ReadElementContentAsInt();
                            break;
                        case "Adresse":
                            // Neue Adresse erzeugen und der Person zuordnen
                            adresse = new Adresse();
                            person.Adresse = adresse;
                            if (reader.HasAttributes)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "Ort")
                                        adresse.Ort = reader.Value;
                                    else if (reader.Name == "Strasse")
                                        adresse.Strasse = reader.Value;
                                }
                            }
                            break;
                    }
                }
            }

            // Liste an der Konsole ausgeben
            GetList(liste);
            reader.Close();

            Console.ReadLine();
        }

        // Ausgabe der Listeneinträge
        static void GetList(List<Person> liste)
        {
            foreach (Person temp in liste)
            {
                Console.WriteLine("Vornmae: {0}\nZuname: {1}\nAlter: {2}", temp.Firstname, temp.Lastname, temp.Alter);
                Console.WriteLine("Ort: {0}\nStrasse: {1}", temp.Adresse.Ort, temp.Adresse.Strasse);
            }
        }
    }
}
