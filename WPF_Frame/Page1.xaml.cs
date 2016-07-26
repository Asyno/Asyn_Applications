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

namespace WPF_Frame
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private int Color { get; set; }

        public Page1()
        {
            InitializeComponent();
            Color = 1;
            this.KeepAlive = true;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            NavigationService.Navigate(e.Uri);
            e.Handled = true;
        }

        private void GoogleClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"http://google.com", UriKind.Absolute));
        }

        private void LinkPage2(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            Uri uri = new Uri("Page2.xaml", UriKind.RelativeOrAbsolute);
            nav.Navigate(uri);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (Color == 1)
            {
                Background = new SolidColorBrush(Colors.Yellow);
                Color++;
                return;
            }
            if (Color == 2)
            {
                Background = new SolidColorBrush(Colors.Blue);
                Color++;
                return;
            }
            else
            {
                Background = new SolidColorBrush(Colors.Red);
                Color = 1;
            }
        }
    }
}
