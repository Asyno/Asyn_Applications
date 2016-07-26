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

namespace WPF_Data_Binding
{
    /// <summary>
    /// Interaction logic for BindingList.xaml
    /// </summary>
    public partial class BindingList : Window
    {
        public BindingList()
        {
            InitializeComponent();
            listBox1.ItemsSource = GetPerson();
            listBox1.DisplayMemberPath = "Name";
            listBox1.SelectedIndex = 0;
            listBox1.Focus();

            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            txtAdresse.Text = "";
            txtAlter.Text = "";
            txtName.Focus();

            // Binding lösen
            BindingOperations.ClearBinding(txtName, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtAdresse, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtAlter, TextBox.TextProperty);

            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnDelete.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person { Name = txtName.Text, Adresse = txtAdresse.Text };
            if (txtAlter.Text == "") person.Alter = null;
            else person.Alter = (txtAlter.Text).To<int>();
            liste.Add(person);
            SetBinding();
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnDelete.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetBinding();
            listBox1.SelectedIndex = 0;
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            if(liste.Count>0) btnDelete.IsEnabled = true;
        }

        private void SetBinding()
        {
            Binding binding = new Binding("Name");
            txtName.SetBinding(TextBox.TextProperty, binding);
            binding = new Binding("Adresse");
            txtAdresse.SetBinding(TextBox.TextProperty, binding);
            binding = new Binding("Alter");
            txtAlter.SetBinding(TextBox.TextProperty, binding);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            liste.RemoveAt(index);
            if (liste.Count == 0) btnDelete.IsEnabled = false;
            else listBox1.SelectedIndex = 0;
        }
    }
}
