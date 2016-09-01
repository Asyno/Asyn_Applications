using System;
using System.IO;
using System.Windows;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.CreateInfFile
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
                for (int i = 0; i < CbListe.Count; i++)
                    sw.WriteLine(CbListe[i].CCShortName + "=" + CbListe[i].CCName);
                sw.WriteLine();
                sw.Flush();

                // [ChargeBands]
                sw.WriteLine("[ChargeBands]");
                for (int i = 0; i < CbListe.Count; i++)
                    sw.WriteLine(CbListe[i].CBShortName + "=" + CbListe[i].CBName);
                sw.WriteLine();
                sw.Flush();

                // [ChargeRates]
                sw.WriteLine("[ChargeRates]");
                for(int i = 0; i < CbListe.Count; i++)
                {
                    sw.Write(CbListe[i].CBShortName);
                    sw.Write(",A=\"AllDay\",");
                    sw.Write((int)(CbListe[i].PriceCall * 100000) + ",");
                    sw.Write((CbListe[i].PricePer * 1000) + ",");
                    sw.Write((CbListe[i].PriceFor * 1000) + ",");
                    sw.Write((int)(CbListe[i].PriceMin * 100000) + ",");
                    sw.Write((int)(CbListe[i].MinimumPrice * 100000) + ",");
                    sw.WriteLine("None,0,0,0,0,0");
                }
                sw.WriteLine();
                sw.Flush();

                // [DailyRates]
                sw.WriteLine("[DailyRates]");
                for(int i=0;i<CbListe.Count;i++)
                {
                    for (int day = 0; day <= 7; day++)
                        sw.WriteLine(CbListe[i].CBShortName + "," + day + "=0000:A");
                }
                sw.WriteLine();
                sw.Flush();

                // [Destinations]
                sw.WriteLine("[Destinations]");
                for(int i = 0; i < CbListe.Count; i++)
                {
                    foreach(Country item in CbListe[i].Countrys)
                    {
                        if (!(string.IsNullOrWhiteSpace(item.Description) || string.IsNullOrWhiteSpace(item.Prefix)))
                        {
                            sw.Write(item.Prefix + "=\"");
                            sw.Write(item.Description + "\",");
                            sw.Write(CbListe[i].CCShortName + ",");
                            sw.WriteLine(CbListe[i].CBShortName);
                        }
                    }
                }

                sw.Dispose();
                fs.Dispose();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
    }
}
