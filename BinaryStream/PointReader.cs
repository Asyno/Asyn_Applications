using System.IO;

namespace BinaryStream
{
    public class PointReader
    {
        public static void WriteToFile (string path, Point[] array)
        {
            FileStream fileStr = new FileStream(path, FileMode.Create);
            BinaryWriter binWrite = new BinaryWriter(fileStr);

            // Anzahl der Punkte in die Datei schreiben
            binWrite.Write(array.Length);

            // Die Point Daten in die Datei schreiben
            for(int i = 0; i<array.Length;i++)
            {
                binWrite.Write(array[i].XPos);
                binWrite.Write(array[i].YPos);
                binWrite.Write(array[i].Color);
            }
            binWrite.Close();
        }

        public static Point[] GetFromFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            // liest die ersten 4 byte aus der Datei, die die Anzahl der
            // Point-Objecte enthält
            int anzahl = br.ReadInt32();

            // Lesen der Daten aus der Datei
            Point[] arrPoint = new Point[anzahl];
            for(int i=0;i<anzahl;i++)
            {
                arrPoint[i].XPos = br.ReadInt32();
                arrPoint[i].YPos = br.ReadInt32();
                arrPoint[i].Color = br.ReadInt64();
            }
            br.Close();
            return arrPoint;
        }

        public static Point GetPoint(string path, int pointNo)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            int pos = 4 + (pointNo - 1) * 16;
            BinaryReader br = new BinaryReader(fs);

            // Prüfen, ob der user eine gültige Position angegeben hat
            if (pointNo > br.ReadInt32() || pointNo == 0)
            {
                string message = "Unter der angegebenen Position ist";
                message += "kein \nPoint-Objekt gespeichert";
                throw new PositionException(message);
            }

            // den Zeiger positionieren
            fs.Seek(pos, SeekOrigin.Begin);

            // Daten des gewünschten Objekts einlesen
            Point savePoint = new Point();
            savePoint.XPos = br.ReadInt32();
            savePoint.YPos = br.ReadInt32();
            savePoint.Color = br.ReadInt64();
            br.Close();
            return savePoint;
        }
    }
}
