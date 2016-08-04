using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TariffCreator.Config;

namespace TariffCreator.NewTariff
{
    /// <summary>
    /// Interaction logic for CreateInf.xaml
    /// </summary>
    public partial class CreateInf : Page
    {
        ObservableCollection<ChargeBand> cbListe;

        public CreateInf(ObservableCollection<ChargeBand> chargeBandList)
        {
            cbListe = chargeBandList;
            InitializeComponent();
            comboDefault.ItemsSource = cbListe;
            comboDefault.DisplayMemberPath = "CBName";
            comboDefault.SelectedIndex = 0;
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            txtIdent.Text = "00" + rnd.Next(10, 99);
        }

        private void btnSaveInf_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdent.Text != "" && txtName.Text != "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = txtName.Text;
                dlg.DefaultExt = ".inf";
                dlg.Filter = "Tariff files (.inf)|*.inf";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                    SaveInf(dlg.FileName);
            }
            else
                MessageBox.Show("Please insert an Name and an Ident ID, before you save the inf.");
        }
    }
}
