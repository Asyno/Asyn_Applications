using System.Windows;

namespace TariffCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NewTariff.NewTariff newTariff;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTariff_Click(object sender, RoutedEventArgs e)
        {
            if (newTariff == null || !newTariff.IsVisible)
            {
                newTariff = new NewTariff.NewTariff();
                newTariff.Show();

            }
            else
            {
                newTariff.Activate();
            }
        }
    }
}
