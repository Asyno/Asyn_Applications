using System.Windows.Data;
using System.Windows.Controls;

namespace TariffCreator.NewTariff.TariffCreate
{
    partial class CreateTariff
    {
		void BtnActivate()
        {
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            if (countryListe.Count > 0) btnDelet.IsEnabled = true;
            txtDescription.IsEnabled = false;
            txtPrefix.IsEnabled = false;

            Binding binding = new Binding("Description");
            binding.Mode = BindingMode.OneWay;
            txtDescription.SetBinding(TextBox.TextProperty, binding);
            binding = new Binding("Prefix");
            binding.Mode = BindingMode.OneWay;
            txtPrefix.SetBinding(TextBox.TextProperty, binding);
            binding = new Binding("PriceMin");
            binding.StringFormat = "F2";
            txtPriceMin.SetBinding(TextBox.TextProperty, binding);
            binding = new Binding("PriceCall");
            binding.StringFormat = "F2";
            txtPriceCall.SetBinding(TextBox.TextProperty, binding);
            listCountry.SelectedIndex = 0;
        }

		void BtnDeactivate()
        {
            txtDescription.Text = "";
            txtPrefix.Text = "";
            txtPriceCall.Text = "";
            txtPriceMin.Text = "";
            txtDescription.IsEnabled = true;
            txtPrefix.IsEnabled = true;
            txtPrefix.Focus();

            BindingOperations.ClearBinding(txtDescription, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtPrefix, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtPriceCall, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtPriceMin, TextBox.TextProperty);

            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnDelet.IsEnabled = false;
        }
    }
}
