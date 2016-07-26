using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TariffCreator.Config;

namespace TariffCreator.NewTariff
{
    /// <summary>
    /// Interaction logic for CreateChargeband.xaml
    /// </summary>
    public partial class CreateChargeband : Page
    {
        private ObservableCollection<ChargeBand> ChargeBandListe;

        public CreateChargeband(ObservableCollection<ChargeBand> chargeBandList)
        {
            ChargeBandListe = chargeBandList;
            InitializeComponent();
            comboCB.ItemsSource = ChargeBandListe;
            comboCB.DisplayMemberPath = "CBName";
            comboCB.SelectedIndex = 0;
            comboCB.Focus();
        }

        private void btnSaveCB_Click(object sender, RoutedEventArgs e)
        {
            CreateInf createInf = new CreateInf(this.ChargeBandListe);
            this.NavigationService.Navigate(createInf);
        }
    }
}
