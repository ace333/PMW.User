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
using EndUser.BackgroundServices;

namespace EndUserUI
{
    public partial class MainWindow : Window
    {
        private HeartService _heartService;
        private AcceleroService _acceleroService;

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;

            InitializeComponent();

            InitializeServices();
            RunServices();
        }

        private void InitializeServices()
        {
            _heartService = new HeartService();
            _heartService.OnValuesUpdated += _heartService_OnValuesUpdated;

            _acceleroService = new AcceleroService();
            _acceleroService.OnValuesUpdated += _acceleroService_OnValuesUpdated;
        }

        private void RunServices()
        {
            _heartService.RunService();
            _acceleroService.RunService();
        }

        private void _acceleroService_OnValuesUpdated(IList<double[]> values)
        {
            Console.WriteLine(values);
        }

        private void _heartService_OnValuesUpdated(double[] values)
        {
            Console.WriteLine(values);
        }
    }
}
