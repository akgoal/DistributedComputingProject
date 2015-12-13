using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication
{
    class SystemHelper
    {
        /* Класс, предназначенный для облегчения работы с системой и системными службами */

        /* Счетчик загруженности процессора */
        private PerformanceCounter totalCPU;
        public SystemHelper()
        {
            totalCPU = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            totalCPU.NextValue();
            Thread.Sleep(1000);
        }
        /* Загруженность процессора в процентах */
        public float GetCPUUsage() 
        {
            return totalCPU.NextValue();
        }
    }
}
