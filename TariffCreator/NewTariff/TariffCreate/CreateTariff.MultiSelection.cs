using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.TariffCreate
{
    partial class CreateTariff
    {
		private void MultibleSelection(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (ListBox)sender;
            if (list.SelectedItems.Count > 1)
            {
                BindingOperations.ClearAllBindings(txtPrefix);
                BindingOperations.ClearAllBindings(txtDescription);
                BindingOperations.ClearAllBindings(txtPriceMin);
                BindingOperations.ClearAllBindings(txtPriceCall);

                txtDescription.Text = "Multi Select";

                txtPriceMin.LostFocus += ChangePriceMinHandler;
                txtPriceCall.LostFocus += ChangePriceCallHandler;
            }
            else
            {
                BindingOperations.ClearAllBindings(txtPriceMin);
                BindingOperations.ClearAllBindings(txtPriceCall);

                Binding binding = new Binding("Prefix");
                binding.Mode = BindingMode.OneWay;
                txtPrefix.SetBinding(TextBox.TextProperty, binding);
                binding = new Binding("Description");
                binding.Mode = BindingMode.OneWay;
                txtDescription.SetBinding(TextBox.TextProperty, binding);
                binding = new Binding("PriceMin");
                binding.StringFormat = "F2";
                txtPriceMin.SetBinding(TextBox.TextProperty, binding);
                binding = new Binding("PriceCall");
                binding.StringFormat = "F2";
                txtPriceCall.SetBinding(TextBox.TextProperty, binding);

                txtPriceMin.LostFocus -= ChangePriceMinHandler;
                txtPriceCall.LostFocus -= ChangePriceCallHandler;
            }
        }

		private void ChangePriceMinHandler (object sender, EventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listCountry.ItemsSource);
            float pMin;
            NumberStyles style = NumberStyles.Any;
            CultureInfo culture = CultureInfo.InvariantCulture;
            if (float.TryParse(txtPriceMin.Text, style, culture, out pMin))
                for (int i = 0; i < listCountry.SelectedItems.Count; i++)
                {
                    ((Country)listCountry.SelectedItems[i]).PriceMin = pMin;
                }
            view.Refresh();
        }

		private void ChangePriceCallHandler (object sender, EventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listCountry.ItemsSource);
            float pCall;
            NumberStyles style = NumberStyles.Any;
            CultureInfo culture = CultureInfo.InvariantCulture;
            if (float.TryParse(txtPriceCall.Text, style, culture, out pCall))
                for (int i = 0; i < listCountry.SelectedItems.Count; i++)
                    (((Country)listCountry.SelectedItems[i])).PriceCall = pCall;
            view.Refresh();
        }
    }
}
