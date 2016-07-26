using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization_Single
{
    class Program
    {
        static BinaryFormatter formatter;
        static FileStream stream;

        static void Main(string[] args)
        {
            formatter = new BinaryFormatter();
            Person pers = new Person(67, "Fischer");
            SerializeObject(pers);
            Person oldPerson = DeserializeObject();
            Console.WriteLine("Ergebnis der Deserialisierung:");
            Console.WriteLine(oldPerson.Alter);
            Console.WriteLine(oldPerson.Name);

            Console.ReadLine();
        }

        // Objekt serialisieren
        public static void SerializeObject(Object obj)
        {
            stream = new FileStream(@"C:\Users\Jan\Desktop\Test\serialize_single.dat", FileMode.Create);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        // Objekt deserialisieren
        public static Person DeserializeObject()
        {
            stream = new FileStream(@"C:\Users\Jan\Desktop\Test\serialize_single.dat", FileMode.Open);
            return (Person)formatter.Deserialize(stream);
            stream.Close();
        }
    }
}
