using System;
using System.IO;

namespace FileDirectoryFileStream
{
    class Program
    {
        static void Main(string[] args)
        {
            // Schreibvorgang
            byte[] arr = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            string path = @"C:\Users\Jan\Desktop\Testfile.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(arr, 0, arr.Length);

            // Lesevorgang
            byte[] arrRead = new byte[10];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(arrRead, 0, 10);
            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine(arrRead[i]);
            fs.Close();

            Console.WriteLine();

            //Benutzereingabe anfordern
            Console.WriteLine("Geben Sie die zu öffnende Datei an: ");
            string strFile = @"C:\Users\Jan\Desktop\Test\" + Console.ReadLine();

            // Schreiben mit StreamWriter

            StreamWriter sw = new StreamWriter(strFile);
            sw.WriteLine("Hallo, hier");
            sw.WriteLine("steht ganz viel");
            sw.WriteLine("Text");
            sw.Close();

            // prüfen ob die angegebene Datei existiert
            if (!File.Exists(strFile))
            {
                Console.WriteLine("Die Datei {0} existiert nicht", strFile);
                Console.ReadLine();
                return;
            }

            // Datei 
            fs = File.Open(strFile, FileMode.Open);
            // Byte Array, in das die Daten aus dem Datenstrom eingelesen werden
            byte[] puffer = new byte[fs.Length];
            // Die Zeichen aus der Datei lesen und in das Array
            // schreiben, der Lesevorgang beginnt mit dem ersten Zeichen
            fs.Read(puffer, 0, (int)fs.Length);
            // das Byte Array elementeweise einlesen und jedes Array-Element
            // in Char konvertieren
            for (int i = 0; i < fs.Length; i++)
                Console.Write(Convert.ToChar(puffer[i]));

            Console.ReadLine();
        }
    }
}
