using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.TariffCreate
{
    partial class CreateTariff
    {
        /// <summary>
        /// Start File Open Dialog and the Import process
        /// </summary>
        private void ImportInf()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".inf";
            dlg.Filter = "Tariff files (.inf)|*.inf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
                LoadInf(dlg.FileName);
        }

        private void LoadInf(string path)
        {
            try
            {
                string[] tariffInfo = new string[4];
                ObservableCollection<ChargeBand> cbList = null;

                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                if (sr.ReadLine() == "[Carrier]")
                {
                    String line = sr.ReadLine();

                    // Read the Header
                    while (line != "[CallCategories]")
                    {
                        switch (line.Split('=')[0])
                        {
                            case "Name":
                                tariffInfo[0] = line.Split('=')[1];
                                break;
                            case "Ident":
                                tariffInfo[1] = line.Split('=')[1];
                                break;
                            case "DefaultChargeBand":
                                tariffInfo[2] = line.Split('=')[1];
                                break;
                            case "MeterRate":
                                tariffInfo[3] = line.Split('=')[1];
                                break;
                        }
                        line = sr.ReadLine();
                    }

                    while (line != "[ChargeBands]")
                        line = sr.ReadLine();
                    line = sr.ReadLine();

                    // Read ChargeBand Name & Description
                    cbList = new ObservableCollection<ChargeBand>();
                    while (line != "[ChargeRates]")
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            cbList.Add(new ChargeBand
                            {
                                CBShortName = line.Split('=')[0],
                                CCShortName = line.Split('=')[0],
                                CBName = line.Split('=')[1],
                                CCName = line.Split('=')[1],
                                Countrys = new System.Collections.Generic.List<Country>()
                            });
                        line = sr.ReadLine();
                    }
                    line = sr.ReadLine();

                    // Read ChargeBand Prices
                    while (line != "[DailyRates]")
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] rate = line.Split(',');
                            foreach (ChargeBand cb in cbList)
                            {
                                if (cb.CBShortName == rate[0])
                                {
                                    float callPrice = 0;
                                    float minPrice = 0;
                                    float minimumPrice = 0;
                                    int pricePer = 0;
                                    int priceFor = 0;
                                    if (float.TryParse(rate[2], out callPrice) && callPrice != 0) callPrice = callPrice / 100000;
                                    if (float.TryParse(rate[5], out minPrice) && minPrice != 0) minPrice = minPrice / 100000;
                                    if (float.TryParse(rate[6], out minimumPrice) && minimumPrice != 0) minimumPrice = minimumPrice / 100000;
                                    if (int.TryParse(rate[3], out pricePer) && pricePer != 0) pricePer = pricePer / 1000;
                                    if (int.TryParse(rate[4], out priceFor) && priceFor != 0) priceFor = priceFor / 1000;
                                    cb.PriceCall = callPrice;
                                    cb.PriceMin = minPrice;
                                    cb.MinimumPrice = minimumPrice;
                                    cb.PricePer = pricePer;
                                    cb.PriceFor = priceFor;
                                }
                            }
                        }
                        line = sr.ReadLine();
                    }
                    
                    // Daily Rates überspringen
                    while (line != "[Destinations]")
                        line = sr.ReadLine();
                    line = sr.ReadLine();

                    // Destinations auslesen bis zum Ende des Files
                    while (!sr.EndOfStream)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] split = line.Split(',');
                            string chargeID = split[split.Length - 1];
                            foreach (ChargeBand item in cbList)
                            {
                                if (item.CBShortName == chargeID)
                                    item.Countrys.Add(new Country { Prefix = line.Split('=')[0], Description = line.Split('"')[1] });
                            }
                        }
                        line = sr.ReadLine();
                    }
                }
                else
                    NoTariff();

                sr.Dispose();
                fs.Dispose();
                if(cbList != null)
                {
                    CreateCB.CreateChargeband createChargeband = new CreateCB.CreateChargeband(cbList, tariffInfo);
                    this.NavigationService.Navigate(createChargeband);
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        /// <summary>
        /// Showes a MessageBox when the inf misses important infos
        /// </summary>
        private void NoTariff()
        {
            MessageBox.Show("The inf didn't contain a tariff", "No tariff info", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
