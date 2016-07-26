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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_ElementBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Binding binding = new Binding("SelectedItem.Content");
            // binding.ElementName = "listBox1";
            // textBox1.SetBinding(TextBox.TextProperty, binding);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //BindingExpression binding = txtTarget.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        private void txtTarget_SourceUpdate(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("Aktualisierung durchgeführt.");
        }
    }
}
