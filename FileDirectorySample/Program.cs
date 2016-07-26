using System;
using System.IO;

namespace FileDirectorySample
{
    class Program
    {
        static void Main(string[] args)
        {
            Program dirTest = new Program();
            FileInfo myFile;

            // Benutzereingabe anfordern

            string path = dirTest.PathInput();
            int len = path.Length;

            // alle Ordner und dateien holen

            string[] str = Directory.GetFileSystemEntries(path);
            Console.WriteLine();
            Console.WriteLine("Ordner und Dateien im Verzeichnis {0}", path);
            Console.WriteLine(new string('-', 80));
            for (int i = 0; i <= str.GetUpperBound(0); i++)
            {
                // prüfen, ob der Eintrag ein Verzeichnis oder eine Datei ist
                if (0 == (File.GetAttributes(str[i]) & FileAttributes.Directory))
                {
                    // str(i) ist kein Verzeichnis
                    myFile = new FileInfo(str[i]);
                    string fileAttr = dirTest.GetFileAttributes(myFile);
                    Console.WriteLine("{0,-30}{1,25} kb {2,-10} ",
                                        str[i].Substring(len - 1),
                                        myFile.Length / 1024,
                                        fileAttr);
                }
                else
                    Console.WriteLine("{0,-30}{1,-15}", str[i].Substring(len), "Dateiordner");
            }
            Console.ReadLine();
        }

        // Benutzer zur Eingabe eines Pfades auffordern
        string PathInput()
        {
            Console.Write("Geben Sie den zu durchsuchenden ");
            Console.Write("Ordner an: ");
            string searchPath = Console.ReadLine();

            // wenn die Benutzereingabe als letztes Zeichen kein '\'
            // enthält, muss dieses angehängt werden

            if (searchPath.Substring(searchPath.Length - 1) != "\\")
                searchPath += "\\";
            return searchPath;
        }

        // Feststellung, welche Dateiattribute gesetzt sind, und
        // Rückgabe eines Strings, der die gesetzten Attribute enthält
        string GetFileAttributes(FileInfo strFile)
        {
            string strAttr;

            // prüfen, ob das Archive-Attribute gesetzt ist
            if (0 != (strFile.Attributes & FileAttributes.Archive))
                strAttr = "A ";
            else
                strAttr = "  ";

            // prüfen, ob das Hidden-Attribute gesetzt ist
            if (0 != (strFile.Attributes & FileAttributes.Hidden))
                strAttr += "H ";
            else
                strAttr += "  ";

            // prüfen, ob das ReadOnly-Attribut gesetzt ist
            if (0 != (strFile.Attributes & FileAttributes.ReadOnly))
                strAttr += "R ";
            else
                strAttr += "  ";

            // prüfen, ob das System-Attribute gesetzt ist
            if (0 != (strFile.Attributes & FileAttributes.System))
                strAttr += "S ";
            else
                strAttr += "  ";
            return strAttr;
        }
    }
}
