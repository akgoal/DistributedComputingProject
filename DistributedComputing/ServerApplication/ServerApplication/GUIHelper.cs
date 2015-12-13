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
            return 100;
        }

        /* Получение проверямого числа */
        public static long GetTaskNumber()
        {
            return 15485867;
        }


        /* Следующие методы связаны с журналированием,
         * т.е. фиксированием определенных событий в работе сервера */

        /* Сообщение общего характера для отладки. */
        public static void Log(string str)
        {
            Settings.GetInstance().GetForm().InvokeLog(str);
        }

        /* Вычисление задания task окончено. */
        public static void LogTaskIsCalculated(Task task)
        {
            Log("Задание завершено " + task.GetResult());
        }

        /* Подзадания subtasks сформированы. */
        public static void LogSubTasksCreated(List<Task.Subtask> subtasks)
        {
            Log("Подзадания сформированы: количество=" + subtasks.Count());
        }

        /* Ошибка */
        public static void LogException(Exception ex)
        {
            throw ex;
        }

        /* Установка сокета */
        public static void LogSocketSetting()
        {
            Log("Установка сокета");
        }

        /* Сокет установлен; ipAddr - ip-адрес, port - номер порта */
        public static void LogSocketIsSet(System.Net.IPAddress ipAddr, int port)
        {
            Log("Сокет установлен");
        }

        /* Ожидание соединения */
        public static void LogWaitingForConnections()
        {
            Log("Ожидание соединения");
        }

        /* Получен запрос от клиента на получение подзадания */
        public static void LogRequestReceived()
        {
            Log("Запрос получен");
        }

        /* Отправка подзадания subtask клиенту */
        public static void LogSendingSubtask(Task.Subtask subtask)
        {
            Log("Отправка подзадания: from=" + subtask.getRange()[0] + " id=" + subtask.getId());
        }

        /* Получен результат result вычисления подзадания subtask */
        public static void LogSubtaskResultReceived(Task.Subtask subtask, long result)
        {
            Log("Получен результат: res=" + result+" id="+subtask.getId());
        }

        /* Сервер остановлен */
        public static void LogServerStopped()
        {
            Log("Сервер остановлен");
        }

        /* Нет доступных подзаданий */
        public static void LogNoAvailableSubtasks()
        { 
            
        }
    }
}
