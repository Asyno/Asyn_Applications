using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.CreateInfFile
{
    /// <summary>
    /// Interaction logic for CreateInf.xaml
    /// </summary>
    public partial class CreateInf : Page
    {
        private ObservableCollection<ChargeBand> CbListe;

        /// <summary>
        /// Start the CreateInf Dialog
        /// </summary>
        /// <param name="chargeBandList">ChargeBand List</param>
        public CreateInf(ObservableCollection<ChargeBand> chargeBandList) : this(chargeBandList, null) {}

        /// <summary>
        /// Start the CreateInf Dialog with File Info
        /// </summary>
        /// <param name="chargeBandList">ChargeBand List</param>
        /// <param name="info">File Info - Tarif Name; Tarif ID; Default ChargeBand; Price for Meter Unit</param>
        public CreateInf(ObservableCollection<ChargeBand> chargeBandList, string[] info)
        {
            CbListe = chargeBandList;
            InitializeComponent();
            comboDefault.ItemsSource = CbListe;
            comboDefault.DisplayMemberPath = "CBName";
            comboDefault.SelectedIndex = 0;

            // Set File Info if already exist
            if(info != null)
            {
                txtName.Text = info[0];
                txtIdent.Text = info[1];
                txtMeter.Text = info[3];
                ChargeBand defaultCB = null;
                foreach(ChargeBand item in CbListe)
                {
                    if (item.CBShortName == info[2])
                        defaultCB = item;
                }
                comboDefault.SelectedItem = defaultCB;
            }
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
                dlg.Filter = "Tariff file (.inf)|*.inf|XML file for Excel (.xml)|*.xml|Excel Worksheed (.xlsx)|*.xlsx";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    if(dlg.FilterIndex == 1)
                        SaveInf(dlg.FileName);
                    if (dlg.FilterIndex == 2)
                        SaveXML(dlg.FileName);
                    if (dlg.FilterIndex == 3)
                        SaveXLSX(dlg.FileName);
                }
            }
            else
                MessageBox.Show("Please insert an Name and an Ident ID, before you save the inf.");
        }
    }
}
