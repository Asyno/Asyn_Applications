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

namespace WPF_NavigationDatasend
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        public void SetLoadCompleteHandler (NavigationService nav)
        {
            nav.LoadCompleted += new LoadCompletedEventHandler(nav_LoadCompleted);
        }

        void nav_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData != null && (e.ExtraData is String))
                TextBox1.Text = (string)e.ExtraData;
            this.NavigationService.LoadCompleted -= nav_LoadCompleted;
        }
    }
}
