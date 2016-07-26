﻿using System;
using System.Collections.ObjectModel;
using TariffCreator.NewTariff.TariffImport;
using TariffCreator.Config;

namespace TariffCreator.NewTariff
{
    partial class CreateTariff
    {
        ImportTariff import = null;
        private void ImportCountrys()
        {
            if (import != null) import.Close();
            import = new ImportTariff();
            import.Show();
            import.TariffListCreated += new EventHandler(GetNewCountryList);
        }

        private void GetNewCountryList(object sender, EventArgs e)
        {
            ObservableCollection<Config.Country> newList = ((TariffListEventArgs)e).TariffList;
            if (((TariffListEventArgs)e).IsOverride != null && ((bool)((TariffListEventArgs)e).IsOverride))
                    countryListe = newList;
            else
                foreach (Country item in newList)
                    countryListe.Add(item);
            
            listCountry.ItemsSource = countryListe;
            listCountry.SelectedIndex = 0;
            listCountry.Focus();
            view.Refresh();
        }
    }
}
