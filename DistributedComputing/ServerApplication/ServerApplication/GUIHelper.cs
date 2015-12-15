using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApplication
{
    class GUIHelper
    {
        /* Запуск сервера */
        public static void StartServer()
        {
            Server.Start();
        }

        /* Остановка сервера */
        public static void StopServer()
        {
            Server.Stop();
        }

        /* Получение длины отрезка чисел, отправляемого клиенту */
        public static long GetRangeLength()
        {
            return Settings.GetInstance().GetForm().GetRange();
           // return 100;
        }

        /* Получение проверямого числа */
        public static long GetTaskNumber()
        {
            //return 15485867;
            return Settings.GetInstance().GetForm().GetNumber();
        }


        /* Следующие методы связаны с журналированием,
         * т.е. фиксированием определенных событий в работе сервера */

        /* Сообщение общего характера для отладки. */
        public static void Log(string str)
        {
            Settings.GetInstance().GetForm().InvokeLog(str);
        }

        //public static void WaitLog()
        //{
        //    Settings.GetInstance().GetForm().InvokeWaitLog();
        //}

        /* Вычисление задания task окончено. */
        public static void LogTaskIsCalculated(Task task)
        {
            string result;
            if (task.GetResult() == 1)
                result = " простое";
            else
                result = " непростое";
            Log("Задание завершено: " + GetTaskNumber()+result);
        }

        /* Подзадания subtasks сформированы. */
        public static void LogSubTasksCreated(List<Task.Subtask> subtasks)
        {
            Log("Подзадания сформированы успешно! \r\nКоличество подзаданий=" + subtasks.Count());
        }

        /* Ошибка */
        public static void LogException(Exception ex)
        {
            throw ex;
        }

        /* Установка сокета */
        public static void LogSocketSetting()
        {
            Log("Установка сокета...");
        }

        /* Сокет установлен; ipAddr - ip-адрес, port - номер порта */
        public static void LogSocketIsSet(System.Net.IPAddress ipAddr, int port)
        {
            Log("Сокет установлен!");
        }

        /* Ожидание соединения */
        public static void LogWaitingForConnections()
        {
            Log("Ожидание соединения...");
           // WaitLog();
        }

        /* Получен запрос от клиента на получение подзадания */
        public static void LogRequestReceived()
        {
            Log("Запрос получен!");
        }

        /* Отправка подзадания subtask клиенту */
        public static void LogSendingSubtask(Task.Subtask subtask)
        {
            Log("Отправка подзадания id=" + subtask.getId() + ": Отрезок=[" + subtask.getRange()[0] 
                + ";" + subtask.getRange()[1] + "]");
        }

        /* Получен результат result вычисления подзадания subtask */
        public static void LogSubtaskResultReceived(Task.Subtask subtask, long result)
        {
            if (result == 1)
                Log("Получен результат подзадания id=" + subtask.getId() + ": Делителей нет");
            else
                Log("Получен результат подзадания id=" + subtask.getId() + ": Найден делитель " + result);
        }

        /* Сервер остановлен */
        public static void LogServerStopped()
        {
            Log("Сервер остановлен");
        }

        /* Нет доступных подзаданий */
        public static void LogNoAvailableSubtasks()
        {
            Log("Нет доступных подзаданий для отправки");
        }
    }
}
