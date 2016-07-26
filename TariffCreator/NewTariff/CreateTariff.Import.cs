using System;
using System.Collections.ObjectModel;
using TariffCreator.NewTariff.TariffImport;

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
            countryListe = newList;
            listCountry.ItemsSource = countryListe;
            listCountry.SelectedIndex = 0;
            listCountry.Focus();
        }
    }
}
