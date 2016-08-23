using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TariffCreator.Config;

namespace TariffCreator.ReadInf.LoadFile
{
    partial class LoadFile
    {
        /// <summary>
        /// Load the *.inf file and create the ChargeBand List
        /// </summary>
        /// <param name="path"></param>
        private void LoadInf(string path)
        {
            try
            {
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
                                txtName.Text = line.Split('=')[1];
                                break;
                            case "Ident":
                                txtIdent.Text = line.Split('=')[1];
                                break;
                            case "DefaultChargeBand":
                                txtDefault.Text = line.Split('=')[1];
                                break;
                            case "MeterRate":
                                txtMeter.Text = line.Split('=')[1];
                                break;
                        }
                        line = sr.ReadLine();
                    }

                    while (line != "[ChargeBands]")
                        line = sr.ReadLine();

                    // Read ChargeBand Name & Description
                    CbList = new ObservableCollection<ChargeBand>();
                    while (line != "[ChargeRates]")
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                            CbList.Add(new ChargeBand
                            {
                                CBShortName = line.Split('=')[0],
                                CCShortName = line.Split('=')[0],
                                CBName = line.Split('=')[1],
                                CCName = line.Split('=')[1]
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
                            foreach(ChargeBand cb in CbList)
                            {
                                if (cb.CBShortName == rate[0])
                                {
                                    float minPrice = 0;
                                    if (float.TryParse(rate[2], out minPrice) && minPrice != 0)
                                        minPrice = minPrice / 100000;
                                    cb.MinimumPrice = minPrice;
                                }
                            }
                        }
                        line = sr.ReadLine();
                    }

                    string test = "";
                    foreach (ChargeBand cb in CbList)
                        test += cb.CBName + " - " + cb.MinimumPrice+"\r\n";
                    MessageBox.Show(test);
                }
                else
                    NoTariff();

                sr.Dispose();
                fs.Dispose();
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
