using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ClientApplication
{
    class SystemHelper
    {
        /* Класс, предназначенный для облегчения работы с системой и системными службами.
         * Требуется подождать секунду, прежде чем использовать функции класса.
         * Для этого в метод инициализации передается слушатель,
         *  реализующий интерфейс InitListener с единственным методом OnInit(), вызываемым по завершении инициализации. */

        /* Счетчик загруженности процессора */
        private static PerformanceCounter totalCPU;

        /* Слушатель завершения инициализации */
        private static InitListener initListener;

        /* Инициализация. */
        public static void Init(InitListener listener)
        {
            initListener = listener;
            totalCPU = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            totalCPU.NextValue();
            System.Timers.Timer timer = new System.Timers.Timer(4000);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
            timer.AutoReset = false;
        }

        /* Оповещение слушателя о завершении инициализации */
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            initListener.OnInit();
        }

        /* Загруженность процессора в процентах */
        public static float GetCPUUsage() 
        {
            return totalCPU.NextValue();
        }

        /* Интерфейс, реализуемый классом, желающим получить информацию
         *  о завершении процесса инициализации */
        public interface InitListener
        {
            void OnInit();
        }
    }
}
