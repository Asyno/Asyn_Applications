using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;

namespace TariffCreator.Config
{
    class CountryList
    {
        public static ObservableCollection<Country> GetCountryList()
        {
            ObservableCollection<Country> liste = new ObservableCollection<Country>();
            Country c = new Country();
            Assembly assem = c.GetType().Assembly;
            Stream stream = assem.GetManifestResourceStream("TariffCreator.Config.Countries.txt");
            StreamReader sr = new StreamReader(stream);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line != "" && line != "-" && line != "--")
                {
                    string[] split = line.Split('|');
                    liste.Add(new Country(split[1], split[0]));
                }
            }
            
            return liste;
        }
    }
}
