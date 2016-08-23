using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using TariffCreator.Config;

namespace TariffCreator.ReadInf.LoadFile
{
    /// <summary>
    /// Interaction logic for LoadFile.xaml
    /// </summary>
    public partial class LoadFile : Page
    {
        private ObservableCollection<ChargeBand> CbList;

        public LoadFile()
        {
            InitializeComponent();
        }

        private void btn_LoadInf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".inf";
            dlg.Filter = "Tariff files (.inf)|*.inf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
                LoadInf(dlg.FileName);
        }

        private void btnSaveExcl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOverview_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveInf_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
