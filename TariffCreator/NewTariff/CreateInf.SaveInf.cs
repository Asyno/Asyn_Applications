using System;
using System.IO;
using System.Windows;
using TariffCreator.Config;

namespace TariffCreator.NewTariff
{
    partial class CreateInf
    {
        /// <summary>
         /// Create and Saves the Tariff as inf File
         /// </summary>
         /// <param name="path"></param>
        void SaveInf(String path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                // [Carrier]
                sw.WriteLine("[Carrier]");
                sw.WriteLine("Name=" + txtName.Text);
                sw.WriteLine("Ident=" + txtIdent.Text);
                if (txtMeter.Text != "") sw.WriteLine("MeterRate=" + txtMeter.Text);
                sw.WriteLine("DefaultChargeBand=" + ((ChargeBand)comboDefault.SelectedItem).CBShortName);
                sw.WriteLine();
                sw.Flush();

                // [CallCategories]
                sw.WriteLine("[CallCategories]");
                for (int i = 0; i < cbListe.Count; i++)
                    sw.WriteLine(cbListe[i].CCShortName + "=" + cbListe[i].CCName);
                sw.WriteLine();
                sw.Flush();

                // [ChargeBands]
                sw.WriteLine("[ChargeBands]");
                for (int i = 0; i < cbListe.Count; i++)
                    sw.WriteLine(cbListe[i].CBShortName + "=" + cbListe[i].CBName);
                sw.WriteLine();
                sw.Flush();

                // [ChargeRates]
                sw.WriteLine("[ChargeRates]");
                for(int i = 0; i < cbListe.Count; i++)
                {
                    sw.Write(cbListe[i].CBShortName);
                    sw.Write(",A=\"AllDay\",");
                    sw.Write((int)(cbListe[i].PriceCall * 100000) + ",");
                    sw.Write((cbListe[i].PricePer * 1000) + ",");
                    sw.Write((cbListe[i].PriceFor * 1000) + ",");
                    sw.Write((int)(cbListe[i].PriceMin * 100000) + ",");
                    sw.Write((int)(cbListe[i].MinimumPrice * 100000) + ",");
                    sw.WriteLine("None,0,0,0,0,0");
                }
                sw.WriteLine();
                sw.Flush();

                // [DailyRates]
                sw.WriteLine("[DailyRates]");
                for(int i=0;i<cbListe.Count;i++)
                {
                    for (int day = 0; day <= 7; day++)
                        sw.WriteLine(cbListe[i].CBShortName + "," + day + "=0000:A");
                }
                sw.WriteLine();
                sw.Flush();

                // [Destinations]
                sw.WriteLine("[Destinations]");
                for(int i = 0; i < cbListe.Count; i++)
                {
                    for(int countryCount = 0; countryCount < cbListe[i].Countrys.Count; countryCount++)
                    {
                        Country country = cbListe[i].Countrys[countryCount];
                        sw.Write(country.Prefix + "=\"");
                        sw.Write(country.Description + "\",");
                        sw.Write(cbListe[i].CCShortName + ",");
                        sw.WriteLine(cbListe[i].CBShortName);
                    }
                }

                sw.Dispose();
                fs.Dispose();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
    }
}
