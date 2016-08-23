using System.Windows;

namespace TariffCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NewTariff.NewTariff newTariff;
        private ReadInf.ReadInf readInf;

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
                newTariff.Activate();
        }

        private void btnOverview_Click(object sender, RoutedEventArgs e)
        {
            if (readInf == null || !readInf.IsVisible)
            {
                readInf = new ReadInf.ReadInf();
                readInf.Show();
            }
            else
                readInf.Activate();
        }
    }
}
