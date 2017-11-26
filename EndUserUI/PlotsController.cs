using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndUserUI
{
    public partial class PlotsController
    {
        public IList<DataPoint> HeartPoints { get; private set; }
        public IList<DataPoint> XAcceleroPoints { get; private set; }
        public IList<DataPoint> YAcceleroPoints { get; private set; }
        public IList<DataPoint> ZAcceleroPoints { get; private set; }


        public PlotsController()
        {
            HeartPoints = new List<DataPoint>();
            XAcceleroPoints = new List<DataPoint>();
            YAcceleroPoints = new List<DataPoint>();
            ZAcceleroPoints = new List<DataPoint>();
        }

        public void ReshapeValuesToDataPointsAccelero(IList<double[]> values)
        {
            for (var i = 0; i < values[0].Length; i++)
            {
                XAcceleroPoints.Add(new DataPoint(XAcceleroPoints.Count + 1, values[0].ElementAt(i)));
                YAcceleroPoints.Add(new DataPoint(YAcceleroPoints.Count + 1, values[1].ElementAt(i)));
                ZAcceleroPoints.Add(new DataPoint(ZAcceleroPoints.Count + 1, values[2].ElementAt(i)));
            }
        }

        public void ReshapeValuesToDataPointsHeart(double[] values)
        {
            foreach (var item in values)
                HeartPoints.Add(new DataPoint(HeartPoints.Count + 1, item));
        }
    }
}
