using System;
using System.Collections.Generic;
using GeometricObjects_Solution;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Serialize_multi
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GeometricObject> liste = new List<GeometricObject>();
            liste.Add(new Circle(100, -50, 75));
            liste.Add(new Rectangle(120, 46, 310, 210));
            liste.Add(new Circle(69, 70, -200));
            liste.Add(new GraphicRectangle(58, 45, -10, -20));

            // Liste serialisieren
            SaveList(liste);

            // Liste deserialisieren
            List<GeometricObject> newList = GetListObjects();

            foreach(var item in newList)
            {
                Circle circle = item as Circle;
                Rectangle rect = item as Rectangle;

                if (circle != null)
                    Console.WriteLine("Circle: Radius = {0,-5}X={1,-5}Y={2}",
                        circle.Radius, circle.XCoordinate, circle.YCoordinate);
                else if (rect != null)
                    Console.WriteLine("Rectangle: Length = {0,-5}Width = {1,-5}X={2,-5}Y={3}",
                        rect.Height, rect.Width, rect.XCoordinate, rect.YCoordinate);
                
            }
            Console.ReadLine();
        }

        public static void SaveList(IList<GeometricObject> elements)
        {
            FileStream stream = new FileStream(@"C:\Users\Jan\Desktop\Test\serialize_multi.dat", FileMode.Create);
            BinaryFormatter binFormatter = new BinaryFormatter();
            binFormatter.Serialize(stream, elements);
            stream.Close();
        }

        public static List<GeometricObject> GetListObjects()
        {
            FileStream stream = new FileStream(@"C:\Users\Jan\Desktop\Test\serialize_multi.dat", FileMode.Open);
            List<GeometricObject> oldList = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                oldList = (List<GeometricObject>)formatter.Deserialize(stream);
            }
            catch (SerializationException e) { Console.WriteLine(e.Message); }
            catch (IOException e) { Console.WriteLine(e.Message); }

            return oldList;
        }
    }
}
