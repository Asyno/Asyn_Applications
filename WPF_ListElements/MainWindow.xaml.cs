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

namespace WPF_ListElements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lstView.ItemsSource = CreatePersonList();
            ((TreeViewItem)treeView1.Items[0]).IsSelected = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextListBox.Text = ((ListBoxItem)ListBox1.SelectedItem).Content.ToString();
        }

        private List<Person> CreatePersonList()
        {
            List<Person> liste = new List<Person>();
            liste.Add(new Person { Name = "Meier", Wohnort = "Celle", Alter = 35 });
            liste.Add(new Person { Name = "Hans", Wohnort = "Bochum", Alter = 20 });
            return liste;
        }
    }
}
