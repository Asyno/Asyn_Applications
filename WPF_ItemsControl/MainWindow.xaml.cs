using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_Data_Binding;

namespace WPF_ItemsControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listBox1.ItemsSource = GetPerson();
            listBox1.SelectedIndex = 0;
            listBox1.Focus();
        }

        ObservableCollection<Person> liste = new ObservableCollection<Person>();

        public ObservableCollection<Person> GetPerson()
        {
            liste.Add(new Person { Name = "Schulze", Alter = 56, Adresse = "Bremen" });
            liste.Add(new Person { Name = "Hans", Alter = 30, Adresse = "Hamburg" });
            liste.Add(new Person { Name = "Franz", Alter = 45, Adresse = "Köln" });
            liste.Add(new Person { Name = "Müller", Alter = 26, Adresse = "Hannover" });

            return liste;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            liste.Add(new Person { Name = "Walter", Alter = 74, Adresse = "München" });
        }

        private void btnEditAge_Click(object sender, RoutedEventArgs e)
        {
            ((Person)listBox1.SelectedItem).Alter = 69;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            liste.Remove((Person)listBox1.SelectedItem);
        }
    }
}
