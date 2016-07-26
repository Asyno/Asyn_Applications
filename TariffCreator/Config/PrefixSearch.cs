using System.IO;
using System.Linq;
using System.Reflection;

namespace TariffCreator.Config
{
    class PrefixSearch
    {
        public static string[] SearchPrefix(string description)
        {
            Country c = new Country();
            Assembly assem = c.GetType().Assembly;
            Stream stream = assem.GetManifestResourceStream("TariffCreator.Config.Prefix_Search.txt");
            StreamReader sr = new StreamReader(stream);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if(line.StartsWith(description, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    string[] split = line.Split('\t');
                    return new string[] { split[split.Count() - 1], split[0] };
                }
            }

            return null;
        }
    }
}
