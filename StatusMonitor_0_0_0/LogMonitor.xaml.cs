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
using StatusMonitor_0_0_0.Config;

namespace StatusMonitor_0_0_0
{
    /// <summary>
    /// Interaction logic for LogMonitor.xaml
    /// </summary>
    public partial class LogMonitor : Window
    {
        public LogMonitor(Interface inter)
        {
            InitializeComponent();
            txt_Logging.Text = inter.LogPath + inter.LogFile;
        }
    }
}
