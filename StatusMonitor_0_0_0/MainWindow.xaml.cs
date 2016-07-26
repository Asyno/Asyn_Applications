using Microsoft.Win32;
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
using StatusMonitor_0_0_0.Config;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace StatusMonitor_0_0_0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Interface> Interfaces = new ObservableCollection<Interface>();
        private LogMonitor monitor;
        private CollectionView view;
        public MainWindow()
        {
            InitializeComponent();
            LoadInterfaces();
            list_Interface.ItemsSource = Interfaces;
            list_Interface.Focus();
            view = (CollectionView)CollectionViewSource.GetDefaultView(list_Interface.ItemsSource);
        }

        /// <summary>
        /// Add a new Interface ini and create a new interface at the Overview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".ini";
            dlg.Filter = "Interface config files (.ini)|*.ini";
            Nullable<bool> result = dlg.ShowDialog();
            Interface newint = new Config.Interface(dlg.FileName);
            Interfaces.Add(newint);
            SaveInterfaces();
        }

        private void btn_Logging_Click(object sender, RoutedEventArgs e)
        {
            if(list_Interface.SelectedItem != null)
            {
                if (monitor == null || !monitor.IsVisible)
                {
                    monitor = new LogMonitor((Interface)list_Interface.SelectedItem);
                    monitor.Show();

                }
                else
                {
                    monitor.Activate();
                }
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (list_Interface.SelectedItem != null)
                Interfaces.Remove((Interface)list_Interface.SelectedItem);
            view.Refresh();
        }
    }
}
