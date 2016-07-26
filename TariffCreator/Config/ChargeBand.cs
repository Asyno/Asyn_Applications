using System.Collections.Generic;

namespace TariffCreator.Config
{
    public class ChargeBand
    {
        public float PriceMin { get; set; }
        public float PriceCall { get; set; }
        public float MinimumPrice { get; set; }
        public int PriceFor { get; set; }
        public int PricePer { get; set; }
        public string CBName { get; set; }
        public string CBShortName { get; set; }
        public string CCName { get; set; }
        public string CCShortName { get; set; }
        public List<Country> Countrys { get; set; }
    }
}
