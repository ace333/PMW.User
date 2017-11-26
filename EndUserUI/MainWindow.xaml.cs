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
        private PlotsController _controller;


        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;

            _controller = new PlotsController();

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

        private void InvalidateMeasurementPlot(string key)
        {
            if (key == MeasurementKeys.Accelero)
            {
                acceleroPlot.Series[0].ItemsSource = _controller.XAcceleroPoints;
                acceleroPlot.Series[1].ItemsSource = _controller.YAcceleroPoints;
                acceleroPlot.Series[2].ItemsSource = _controller.ZAcceleroPoints;
                acceleroPlot.InvalidatePlot(true);

                if (CheckIfMaximumIsOverflew(acceleroPlot.Axes[0].Maximum, _controller.XAcceleroPoints))
                {
                    acceleroPlot.Axes[0].Minimum = _controller.XAcceleroPoints.Count / 2;
                    acceleroPlot.Axes[0].Maximum = acceleroPlot.Axes[0].Minimum + 100;
                }
            }
            else
            {
                heartPlot.Series[0].ItemsSource = _controller.HeartPoints;
                heartPlot.InvalidatePlot(true);

                if (CheckIfMaximumIsOverflew(heartPlot.Axes[0].Maximum, _controller.HeartPoints))
                {
                    heartPlot.Axes[0].Minimum = _controller.HeartPoints.Count;
                    heartPlot.Axes[0].Maximum = heartPlot.Axes[0].Minimum + 100;
                }
            }
        }

        private bool CheckIfMaximumIsOverflew(double maximum, IList<DataPoint> measurements)
        {
            return measurements.Count >= maximum;                
        }

        private void _acceleroService_OnValuesUpdated(IList<double[]> values)
        {
            _controller.ReshapeValuesToDataPointsAccelero(values);

            acceleroPlot.Dispatcher.Invoke(new Action(() => { InvalidateMeasurementPlot(MeasurementKeys.Accelero); }));
        }

        private void _heartService_OnValuesUpdated(double[] values)
        {            
            _controller.ReshapeValuesToDataPointsHeart(values);

            acceleroPlot.Dispatcher.Invoke(new Action(() => { InvalidateMeasurementPlot(MeasurementKeys.HeartRate); }));
        }

       
    }

}
