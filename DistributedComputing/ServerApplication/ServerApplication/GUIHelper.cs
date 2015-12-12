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
            return 1000000000;
        }

        /* Получение проверямого числа */
        public static long GetTaskNumber()
        {
            return 1001229804444444433;
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

        }

        /* Подзадания subtasks сформированы. */
        public static void LogSubTasksCreated(List<Task.Subtask> subtasks)
        {

        }

        /* Ошибка */
        public static void LogException(Exception ex)
        {

        }

        /* Установка сокета */
        public static void LogSocketSetting()
        {

        }

        /* Сокет установлен; ipAddr - ip-адрес, port - номер порта */
        public static void LogSocketIsSet(System.Net.IPAddress ipAddr, int port)
        {

        }

        /* Ожидание соединения */
        public static void LogWaitingForConnections()
        {

        }

        /* Получен запрос от клиента на получение подзадания */
        public static void LogRequestReceived()
        {

        }

        /* Отправка подзадания subtask клиенту */
        public static void LogSendingSubtask(Task.Subtask subtask)
        {

        }

        /* Получен результат result вычисления подзадания subtask */
        public static void LogSubtaskResultReceived(Task.Subtask subtask, long result)
        {

        }

        /* Сервер остановлен */
        public static void LogServerStopped()
        { 
            
        }
    }
}
