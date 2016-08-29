using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CG_DatabaseReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private ObservableCollection<Invoice> InvoiceList = new ObservableCollection<Invoice>();
        private ObservableCollection<Customer> CustomerList = new ObservableCollection<Customer>();
        private CollectionView view;
        private InvoicesOverview InvoiceOverview;
        private string ExcelPath = @"Database.xls";

        public MainWindow()
        {
            InitializeComponent();
            // Create Lists
            try
            {
                ReadInvoiceList();
                ReadCustomerList();
            }
            catch(Exception) { MessageBox.Show("Database couldn't be found!", "File not found", MessageBoxButton.OK, MessageBoxImage.Error); this.Close(); }
            gridCustomer.ItemsSource = CustomerList;
            view = (CollectionView)CollectionViewSource.GetDefaultView(gridCustomer.ItemsSource);
            // txtSearchBox changed events
            txtSearchBox.TextChanged += new TextChangedEventHandler(TxtSearchChanged);
            gridCustomer.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(ShowInvoice);
            this.Closing += new CancelEventHandler(Window_Closing);

            FileInfo databaseFile = new FileInfo("Database.xls");
            txtDatabaseVersion.Text = "Last Update: " + databaseFile.CreationTime;
        }

        private void ShowInvoice(Object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(((Customer)gridCustomer.SelectedItem).Debitor))
            {
                if (InvoiceOverview == null)
                    InvoiceOverview = new InvoicesOverview(InvoiceList, (Customer)gridCustomer.SelectedItem);
                else
                {
                    InvoiceOverview.Close();
                    InvoiceOverview = new InvoicesOverview(InvoiceList, (Customer)gridCustomer.SelectedItem);
                }
                InvoiceOverview.Show();
            }
            else
            {
                if(InvoiceOverview != null) InvoiceOverview.Close();
                MessageBox.Show("Claudia Fragen, wieso keine Debitor ID hinterlegt ist! \r\n gez. Leo", "No Debitor ID", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        

        /// <summary>
        /// Handler for Search entrys at the Search TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSearchChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchBox.Text == "")
                view.Filter = null;
            else
                view.Filter = new Predicate<object>(FilterListCustomer);
        }

        private bool FilterListCustomer(object item)
        {
            Customer inv = (Customer)item;
            string search = "" + txtSearchBox.Text;
            return ((inv.Projects.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) != -1) ||
                (inv.Name.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) != -1) ||
                (inv.Zusatz.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) != -1) ||
                (inv.Country.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) != -1) ||
                inv.Debitor.StartsWith(search));
        }
        
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (InvoiceOverview != null)
                InvoiceOverview.Close();
        }
    }
}
