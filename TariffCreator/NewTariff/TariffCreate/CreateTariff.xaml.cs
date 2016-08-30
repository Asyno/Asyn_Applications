using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TariffCreator.NewTariff.TariffCreate
{
    /// <summary>
    /// Interaction logic for CreateTariff.xaml
    /// </summary>
    public partial class CreateTariff : Page
    {
        ObservableCollection<Config.Country> countryListe = Config.CountryList.GetCountryList();
        CollectionView view;

        public CreateTariff()
        {
            InitializeComponent();
            // ListCountry Bind
            listCountry.ItemsSource = countryListe;
            listCountry.SelectedIndex = 0;
            listCountry.Focus();
            // ListCountry Sort
            view = (CollectionView)CollectionViewSource.GetDefaultView(listCountry.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Prefix", ListSortDirection.Ascending));
            // txtSearchBox changed events
            txtSearchBox.TextChanged += new TextChangedEventHandler(TxtSearchChanged);
            // listCountry Multi Select
            listCountry.SelectionChanged += new SelectionChangedEventHandler(MultibleSelection);
            // comboCountry;
            comboCountry.ItemsSource = Config.CountryList.GetCountryList();
            comboCountry.DisplayMemberPath = "Description";
            view = (CollectionView)CollectionViewSource.GetDefaultView(comboCountry.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Description", ListSortDirection.Ascending));
            view.Filter = new Predicate<object>(FilterComboCountry);
        }

        public bool FilterComboCountry (object item)
        {
            Config.Country coun = (Config.Country)item;
            return (coun.Description.IndexOf(" - Mobile") == -1);
        }

        /// <summary>
        /// Activate the Buttons to Add a new Country to the Country List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            BtnDeactivate();
        }

        /// <summary>
        /// Save a new Country to the Country List and deactivate the Button to add a new Country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text != "" && txtPrefix.Text != "")
            {
                float pMin, pCall;
                NumberStyles style = NumberStyles.Any;
                CultureInfo culture = CultureInfo.InvariantCulture;
                Config.Country dublicat = null;
                Config.Country country = new Config.Country(txtDescription.Text, txtPrefix.Text);

                if (float.TryParse(txtPriceMin.Text, style, culture, out pMin))
                    country.PriceMin = pMin;
                else
                    country.PriceMin = null;
                if (float.TryParse(txtPriceCall.Text, style, culture, out pCall))
                {
                    country.PriceCall = pCall;
                    if (country.PriceMin == null) country.PriceMin = 0;
                }
                else
                    country.PriceCall = null;
                foreach(Config.Country item in countryListe)
                {
                    if (item.Prefix == country.Prefix)
                        dublicat = item;
                }
                countryListe.Remove(dublicat);
                countryListe.Add(country);
                view.Refresh();
            }
            BtnActivate();
        }

        /// <summary>
        /// Stopes the Add Country Event and deactivate the buttons for it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            BtnActivate();
        }

        /// <summary>
        /// Delete an Country at the listCountry list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            if (listCountry.Items.Count != 0)
            {
                Config.Country index = (Config.Country)listCountry.SelectedItem;
                countryListe.Remove(index);
                if (countryListe.Count == 0) btnDelet.IsEnabled = false;
                else listCountry.SelectedIndex = 0;
                view.Refresh();
            }
            else
                btnDelet.IsEnabled = false;
        }

        /// <summary>
        /// Create the Chargebands and go to CreateChargeband.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateCB_Click(object sender, RoutedEventArgs e)
        {
            // Should Default ChargeBands be created?
            MessageBoxResult result = MessageBox.Show("Should default Charge Bands be created?", "Deafault ChargeBands",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result != MessageBoxResult.Cancel)
            {
                ObservableCollection<Config.ChargeBand> cbListe = 
                    Config.ChargeBandList.GetChargeBands(countryListe, (result == MessageBoxResult.Yes), (Config.Country)comboCountry.SelectedItem);
                if (cbListe != null)
                {
                    CreateCB.CreateChargeband createChargeband = new CreateCB.CreateChargeband(cbListe);
                    this.NavigationService.Navigate(createChargeband);
                }
                else MessageBox.Show("Please insert as first the Prices", "No Prices configured",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            ImportCountrys();
        }

        private void btnImportFile_Click(object sender, RoutedEventArgs e)
        {
            ImportInf();
        }
    }
}
