using System;
using System.Windows.Controls;
using System.Windows.Data;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.TariffCreate
{
    partial class CreateTariff
    {
        /// <summary>
        /// Handler for Search entrys at the Search TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSearchChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listCountry.ItemsSource);
            if (txtSearchBox.Text == "")
                view.Filter = null;
            else
                view.Filter = new Predicate<object>(FilterListCountry);
        }

        private bool FilterListCountry (object item)
        {
            Country count = (Country)item;
            string search = "" + txtSearchBox.Text;
            return (count.Description.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) != -1) || count.Prefix.StartsWith(search);
        }
    }
}
