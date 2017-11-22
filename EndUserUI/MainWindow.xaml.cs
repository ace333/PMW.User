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
using OxyPlot;
using EndUser.APIParser;
using EndUser.Constants;

namespace EndUserUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;

            InitializeComponent();
            Test();
        }

        private async void Test()
        {
            var id = await HttpRequestParser.GetId(APIValues.HeartRateAddress);
            var values = await HttpRequestParser.GetMeasurement(APIValues.HeartRateAddress);
            Console.WriteLine(id);
        }
    }
}
