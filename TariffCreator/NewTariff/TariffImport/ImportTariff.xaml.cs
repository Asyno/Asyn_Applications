using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using TariffCreator.Config;

namespace TariffCreator.NewTariff.TariffImport
{
    /// <summary>
    /// Interaction logic for ImportTariff.xaml
    /// </summary>
    public partial class ImportTariff : Window
    {
        ObservableCollection<Country> tariffList = new ObservableCollection<Country>();

        public ImportTariff()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Import the insert datas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Import_Click(object sender, RoutedEventArgs e)
        {
            CreateList();
            ObservableCollection<Country> mobile = new ObservableCollection<Country>();
            foreach (Country item in CountryList.GetCountryList())
            {
                foreach (Country c in tariffList)
                    if (item.Description.EndsWith(" - Mobile") && item.Prefix.StartsWith(c.Prefix))
                    {
                        item.PriceCall = c.PriceCall;
                        item.PriceMin = c.PriceMin;
                        mobile.Add(item);
                    }
            }
            foreach (Country item in mobile)
                tariffList.Add(item);
            TariffListEventArgs list = new TariffListEventArgs { TariffList = tariffList, IsOverride = tick_overwrite.IsChecked };
            TariffListCreated(this, list);
            Close();
        }

        /// <summary>
        /// Close the Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event Handler for receiving the TariffList
        /// </summary>
        public event EventHandler TariffListCreated;

        protected virtual void OnTariffListCreated(EventArgs e)
        {
            TariffListCreated?.Invoke(this, e);
        }

        private void CreateList()
        {
            string pat = @"^(\d*)\W*([^;]*)(;|$)\W*(\d*[,.]?\d*)\W*(\d*[,.]?\d*)";
            Regex r = new Regex(pat);
            int maxLines = txtTariff.LineCount;

            for (int i = 0; i < maxLines; i++)
            {
                string line = txtTariff.GetLineText(i).TrimEnd(new Char[] { '\r', '\n' });
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Match m = r.Match(line);
                    if (!(string.IsNullOrWhiteSpace(m.Groups[2] + "")))
                    {
                        string prefix = m.Groups[1] + "";
                        string description = m.Groups[2] + "";
                        if (string.IsNullOrWhiteSpace(prefix))
                        {
                            string[] c = PrefixSearch.SearchPrefix(description);
                            if (c != null)
                            {
                                prefix = c[0];
                                description = c[1];
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(prefix))
                        {
                            float pMin, pCall;
                            if (string.IsNullOrWhiteSpace(m.Groups[4] + "") && string.IsNullOrWhiteSpace(m.Groups[5] + ""))
                            {
                                float.TryParse(txtDefPriceMin.Text, out pMin);
                                float.TryParse(txtDefPriceCall.Text, out pCall);
                            }
                            else
                            {
                                float.TryParse(m.Groups[4] + "", out pMin);
                                float.TryParse(m.Groups[5] + "", out pCall);
                            }
                            tariffList.Add(new Country(description, prefix)
                            { PriceMin = pMin, PriceCall = pCall });
                        }
                    }
                }
            }
        }

        private void btn_Check_Click(object sender, RoutedEventArgs e)
        {
            string pat = @"^(\d*)\W*([^;]*)(;|$)\W*(\d*[,.]?\d*)\W*(\d*[,.]?\d*)";
            Regex r = new Regex(pat);
            int maxLines = txtTariff.LineCount;

            for (int i = 0; i < maxLines; i++)
            {
                string line = txtTariff.GetLineText(i).TrimEnd(new Char[] { '\r', '\n' });
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Match m = r.Match(line);
                    string prefix = m.Groups[1] + "";
                    string description = m.Groups[2] + "";
                    if (string.IsNullOrWhiteSpace(prefix))
                    {
                        int position = txtTariff.GetCharacterIndexFromLineIndex(i);
                        string[] c = PrefixSearch.SearchPrefix(description);
                        if (c != null)
                        {
                            prefix = c[0];
                            description = c[1];
                            txtTariff.SelectionStart = position;
                            txtTariff.SelectionLength = (m.Groups[2] + "").Length;
                            txtTariff.SelectedText = prefix + " " + description;
                        }
                        else
                        {
                            txtTariff.Focus();
                            txtTariff.SelectionStart = position;
                            txtTariff.SelectionLength = line.Length;
                            return;
                        }
                    }
                }
            }
        }
    }

    public class TariffListEventArgs : EventArgs
    {
        public ObservableCollection<Country> TariffList { get; set; }
        public bool? IsOverride { get; set; }
    }
}
