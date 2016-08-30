using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TariffCreator.Config
{
    /// <summary>
    /// Holds Static Methodes for ChargeBands
    /// </summary>
    static class ChargeBandList
    {
        /// <summary>
        /// Create from a List of Countrys a List wit Chargebands
        /// </summary>
        /// <param name="countryListe"></param>
        /// <returns>ChargeBand List</returns>
        public static ObservableCollection<ChargeBand> GetChargeBands(ObservableCollection<Country> countryListe, bool defaultCB, Country defaultCountry)
        {
            ObservableCollection<ChargeBand> cbList = new ObservableCollection<ChargeBand>();
            // Insert default Charge Bands to the list, if requested
            if (defaultCB)
            {
                cbList = GetDefaultCB(defaultCountry);
            }

            // Insert all countrys with a price to the list
            for(int i = 0; i < countryListe.Count; i++)
            {
                Country count = countryListe[i];
                if (!(count.PriceCall == null) || !(count.PriceMin == null))
                {
                    if (count.PriceMin == null) count.PriceMin = 0;
                    if (count.PriceCall == null) count.PriceCall = 0;
                    bool newCB = true;
                    ChargeBand cb = new ChargeBand
                    {
                        PriceMin = (float)count.PriceMin,
                        PriceCall = (float)count.PriceCall,
                        CBName = "B" + cbList.Count + " - " + count.PriceMin.Value.ToString("F2"),
                        CBShortName = "B" + cbList.Count,
                        CCName = "C" + cbList.Count,
                        CCShortName = "C" + cbList.Count,
                        PriceFor = 60,
                        PricePer = 60,
                        Countrys = new List<Country>(),
                    };
                    if (count.PriceMin == 0)
                    {
                        if (count.PriceCall == 0) cb.CBName = "B" + cbList.Count;
                        else cb.CBName = "B" + cbList.Count + " - " + count.PriceCall.Value.ToString("F2") + "pCall";
                    }

                    cb.Countrys.Add(count);
                    for (int n = 0; n < cbList.Count; n++)
                    {
                        if (cbList[n].PriceMin == cb.PriceMin && cbList[n].PriceCall == cb.PriceCall)
                        {
                            newCB = false;
                            cbList[n].Countrys.Add(count);
                        }
                    }
                    if (newCB)
                        cbList.Add(cb);
                }
            }
            if (cbList.Count != 0)
                return cbList;
            else
                return null;
        }

        private static ObservableCollection<ChargeBand> GetDefaultCB(Country defaultCountry)
        {
            ObservableCollection<ChargeBand> cbList = new ObservableCollection<ChargeBand>();
            
            // Local
            ChargeBand cb = new ChargeBand
            {
                CBName = "Local",
                CBShortName = "L",
                CCName = "Local",
                CCShortName = "L",
                PriceFor = 60,
                PricePer = 60,
                Countrys = new List<Country>()
            };
            for(int i=0;i<=9;i++)
                cb.Countrys.Add(new Country("Local", ""+i));
            cbList.Add(cb);

            // National
            cb = new ChargeBand
            {
                CBName = "National",
                CBShortName = "N",
                CCName = "National",
                CCShortName = "N",
                PriceFor = 60,
                PricePer = 60,
                Countrys = new List<Country>()
            };
            cb.Countrys.Add(new Country("National", "0"));
            if (defaultCountry != null) cb.Countrys.Add(defaultCountry);
            cbList.Add(cb);
            
            // Mobile
            if(defaultCountry != null)
            {
                cb = new ChargeBand
                {
                    CBName = "Mobile",
                    CBShortName = "M",
                    CCName = "Mobile",
                    CCShortName = "M",
                    PriceFor = 60,
                    PricePer = 60,
                    Countrys = new List<Country>()
                };
                List<Country> mobile = new List<Country>();
                foreach( Country item in CountryList.GetCountryList())
                {
                    if(item.Description == defaultCountry.Description+" - Mobile")
                    {
                        mobile.Add(item);
                        cb.Countrys.Add(item);
                    }
                }

                foreach (Country item in mobile)
                    cb.Countrys.Add(new Country(item.Description, 0+item.Prefix.Replace(defaultCountry.Prefix,"")));
                cbList.Add(cb);
            }

            // Free
            cb = new ChargeBand
            {
                CBName = "Free",
                CBShortName = "F",
                CCName = "Free",
                CCShortName = "F",
                PriceFor = 60,
                PricePer = 60,
                Countrys = new List<Country>()
            };
            cb.Countrys.Add(new Country("Free", "0800"));
            cb.Countrys.Add(new Country("Emergency", "112"));
            cb.Countrys.Add(new Country("Emergency", "110"));
            cb.Countrys.Add(new Country("Emergency", "911"));
            cbList.Add(cb);

            return cbList;
        }
    }
}
