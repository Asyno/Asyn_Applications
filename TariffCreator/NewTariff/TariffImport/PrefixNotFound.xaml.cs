using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TariffCreator.NewTariff.TariffImport
{
    /// <summary>
    /// Interaction logic for PrefixNotFound.xaml
    /// </summary>
    public partial class PrefixNotFound : Window
    {
        public PrefixNotFound(string description)
        {
            InitializeComponent();
            txt_Description.Text = description;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_Prefix.Text))
                Close();
            else
                label_Error.Content = "Please insert a Prefix.";
        }
    }
}
