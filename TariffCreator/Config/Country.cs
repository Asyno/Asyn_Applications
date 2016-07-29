using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffCreator.Config
{
    public class Country
    {
        public string Description { get; set; }
        public string Prefix { get; set; }
        public float? PriceMin { get; set; }
        public float? PriceCall { get; set; }

        public Country() { }
        public Country(string desc, string prefix)
        {
            Description = desc;
            Prefix = prefix;
        }
    }
}
