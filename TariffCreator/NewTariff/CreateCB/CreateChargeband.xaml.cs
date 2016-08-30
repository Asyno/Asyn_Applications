using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.CreateCB
{
    /// <summary>
    /// Interaction logic for CreateChargeband.xaml
    /// </summary>
    public partial class CreateChargeband : Page
    {
        private ObservableCollection<ChargeBand> ChargeBandListe;
        private string[] InfoText;

        public CreateChargeband(ObservableCollection<ChargeBand> chargeBandList) : this(chargeBandList, null) {}

        public CreateChargeband(ObservableCollection<ChargeBand> chargeBandList, string[] info)
        {
            ChargeBandListe = chargeBandList;
            InitializeComponent();
            comboCB.ItemsSource = ChargeBandListe;
            comboCB.DisplayMemberPath = "CBName";
            comboCB.SelectedIndex = 0;
            comboCB.Focus();
            InfoText = info;
        }



        /// <summary>
        /// Start the Menu to save the Tariff as inf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCB_Click(object sender, RoutedEventArgs e)
        {
            CreateInfFile.CreateInf createInf = new CreateInfFile.CreateInf(ChargeBandListe, InfoText);
            this.NavigationService.Navigate(createInf);
        }

        /// <summary>
        /// Delete a Country from the Charge Band
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCountry_Click(object sender, RoutedEventArgs e)
        {
            if(listCB.Items.Count != 0)
            {
                Country index = (Country)listCB.SelectedItem;
                ((ChargeBand)comboCB.SelectedItem).Countrys.Remove(index);
                ListRefresh();
            }
        }

        /// <summary>
        /// Refreshed the View of listCB
        /// </summary>
        private void ListRefresh()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listCB.ItemsSource);
            try { view.Refresh(); } catch (Exception) { }
        }
    }
}
