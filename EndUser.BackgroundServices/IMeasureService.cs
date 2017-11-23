using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EndUser.BackgroundServices
{
    public interface IMeasureService
    {
        void RunService();
        void StopService();
    }
}
