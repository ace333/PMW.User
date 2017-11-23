using EndUser.APIParser;
using EndUser.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace EndUser.BackgroundServices
{
    public class HeartService : IMeasureService
    {
        public delegate void ValuesUpdated(double[] values);
        public event ValuesUpdated OnValuesUpdated;

        private BackgroundWorker _worker;
        private int _heartId;

        public HeartService()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
        }

        public void RunService()
        {
            _worker.RunWorkerAsync();
        }

        public void StopService()
        {
            _worker.CancelAsync();
        }

        private async void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                await Task.Delay(100);

                var id = await HttpRequestParser.GetId(APIValues.HeartRateAddress);
                if(id != _heartId)
                {
                    _heartId++;
                    var values = await HttpRequestParser.GetMeasurement(APIValues.HeartRateAddress);

                    OnValuesUpdated(values);
                }
            }
        }
    }
}
