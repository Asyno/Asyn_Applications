using System;

namespace BinaryStream
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Jan\Desktop\Test\Point.txt";
            // Point-Array erzeugen
            Point[] pArr =
            {
                new Point() { XPos = 10, YPos = 20, Color = 310 },
                new Point() { XPos = 40, YPos = 50, Color = 110 },
                new Point() { XPos = 20, YPos = 10, Color = 500 },
            };

            // Point-Array speichern
            PointReader.WriteToFile(path, pArr);

            // gespeichert Informationen aus der Datei einlesen
            Point[] x = PointReader.GetFromFile(path);

            // alle eingegebenen Point Dateien ausgeben
            for(int i = 0; i < x.Length; i++)
            {
                Console.WriteLine("Point-Objekt-Nr.{0}", i + 1);
                Console.WriteLine();
                Console.WriteLine("P[{0}].XPos  = {1}", i + 1, x[i].XPos);
                Console.WriteLine("P[{0}].YPos  = {1}", i + 1, x[i].YPos);
                Console.WriteLine("P[{0}].Color = {1}", i + 1, x[i].Color);
                Console.WriteLine(new String('=', 30));
            }

            // einen bestimmten Point einlesen
            Console.WriteLine("\nWelchen Punkt möchten Sie einlesen? ");
            int position = Convert.ToInt32(Console.ReadLine());
            try
            {
                Point myPoint = PointReader.GetPoint(path, position);
                Console.WriteLine("p.XPos  = {0}", myPoint.XPos);
                Console.WriteLine("p.YPos  = {0}", myPoint.YPos);
                Console.WriteLine("p.Color = {0}", myPoint.Color);
            }
            catch(Exception e) { Console.WriteLine(e.Message); }

            Console.ReadLine();
        }
    }
}
