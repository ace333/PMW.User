using EndUser.APIParser;
using EndUser.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace EndUser.BackgroundServices
{
    public class AcceleroService : IMeasureService
    {
        public delegate void ValuesUpdated(IList<double[]> values);
        public event ValuesUpdated OnValuesUpdated;

        private BackgroundWorker _worker;
        private int _acceleroId;

        public AcceleroService()
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

                var id = await HttpRequestParser.GetId(APIValues.AcceleroAddress);
                if (id != _acceleroId)
                {
                    _acceleroId++;
                    var values = await HttpRequestParser.GetListedMeasurement(APIValues.AcceleroAddress);

                    OnValuesUpdated(values);
                }
            }
        }
    }
}
