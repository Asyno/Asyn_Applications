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

namespace WPF_Data_Binding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList winBindingList;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = pers2;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            pers2.Name = "Hans Frisch";
            pers2.Alter = 35;
            pers2.Adresse = "Köln, Hauptstraße";
        }

        private void bindingList_Click(object sender, RoutedEventArgs e)
        {
            if (winBindingList==null || !winBindingList.IsVisible)
            {
                winBindingList = new BindingList();
                winBindingList.Show();
                
            }
            else
            {
                winBindingList.Activate();
            }
        }
    }
}
