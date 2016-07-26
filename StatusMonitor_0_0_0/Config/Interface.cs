using System;
using System.IO;

namespace StatusMonitor_0_0_0.Config
{
    [Serializable]
    public class Interface
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string QuitPath { get; set; }
        public string LogPath { get; set; }
        public string LogFile { get; set; }

        public Interface (string path)
        {
            Path = path;
            LoadIni();
        }

        public void LoadIni()
        {
            FileStream fs = new FileStream(Path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split('=');
                switch (line[0])
                {
                    case "RunInstance":
                        ID = line[1];
                        break;
                    case "ConnectedTo":
                        Name = line[1];
                        break;
                    case "QuitFile":
                        QuitPath = line[1];
                        break;
                    case "LogDir":
                        LogPath = line[1];
                        break;
                    case "CommsLogFile":
                        LogFile = line[1];
                        break;
                }
            }
            sr.Close();
            fs.Close();
        }
    }
}
