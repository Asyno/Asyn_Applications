using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using StatusMonitor_0_0_0.Config;
using System;
using System.Windows;

namespace StatusMonitor_0_0_0
{
    partial class MainWindow
    {
        private void SaveInterfaces()
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream("Interfaces.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, Interfaces);
                stream.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Status Monitor config couldn't be saved: " + e.Message,
                    "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (stream != null) stream.Close();
            }
        }

        private void LoadInterfaces()
        {
            if (File.Exists("Interfaces.bin"))
            {
                Stream stream = null;
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream("Interfaces.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Interfaces = (ObservableCollection<Interface>)formatter.Deserialize(stream);
                    stream.Close();
                    foreach (Interface i in Interfaces)
                        i.LoadIni();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Status Monitor config couldn't be loaded: " + e.Message,
                        "Config error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (stream != null) stream.Close();
                }
            }
        }
    }
}
