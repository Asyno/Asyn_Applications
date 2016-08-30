using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ObservableCollection<ChargeBand> cbList;

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
                            foreach (ChargeBand cb in cbList)
                            {
                                if (cb.CBShortName == rate[0])
                                {
                                    float minPrice = 0;
                                    if (float.TryParse(rate[2], out minPrice) && minPrice != 0)
                                        minPrice = minPrice / 100000;
                                    cb.MinimumPrice = minPrice;
                                    // Restlichen Preise auslesen!!
                                }
                            }
                        }
                        line = sr.ReadLine();
                    }

                    string test = "";
                    foreach (ChargeBand cb in cbList)
                        test += cb.CBName + " - " + cb.MinimumPrice + "\r\n";
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
