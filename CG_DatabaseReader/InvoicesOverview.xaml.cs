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
using System.Windows.Shapes;

namespace CG_DatabaseReader
{
    /// <summary>
    /// Interaction logic for InvoicesOverview.xaml
    /// </summary>
    public partial class InvoicesOverview : Window
    {
        private Customer Index;

        public InvoicesOverview(ObservableCollection<Invoice> invoiceList, Customer index)
        {
            InitializeComponent();
            Index = index;
            gridInvoice.ItemsSource = invoiceList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(gridInvoice.ItemsSource);
            view.Filter = new Predicate<object>(FilterListInvoice);
        }

        private bool FilterListInvoice(object item)
        {
            Invoice inv = (Invoice)item;
            string search = Index.Debitor;
            return inv.Debitor.StartsWith(search);
        }
    }
}
