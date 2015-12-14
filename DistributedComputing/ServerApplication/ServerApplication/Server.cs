using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Server
    {
        /* Класс описания работы сервера */

        /* Поток работы сервера */
        private static Thread serverThread;

        /* Сокет для прослушивания соединений */
        private static Socket socketListener;

        /* Выполняемое задание */
        private static Task task;

        /* Запуск потока работы сервера */
        public static void Start()
        {
            // Убеждаемся, что сервер не работает
            while (serverThread != null && serverThread.IsAlive) ;
            serverThread = new Thread(StartThread);
            serverThread.Start();
        }

        /* Запуск сервера */
        public static void StartThread()
        {
            Settings.GetInstance().SetRangeLength(GUIHelper.GetRangeLength());
            task = new Task(GUIHelper.GetTaskNumber());

            SetSocket();

            try
            {
                StartListening();
            }
            catch (Exception ex)
            {
                // Поток работы сервера был остановлен. Ничего страшного
            }
        }

        /* Настройка сокета */
        private static void SetSocket()
        {
            GUIHelper.LogSocketSetting();
            //Установка локальной конечной точки для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            int port = 11000; //номер порта
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            //Создание сокета Tcp/Ip
            socketListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначение сокета локальной конечной точке
            socketListener.Bind(ipEndPoint);

            GUIHelper.LogSocketIsSet(ipAddr, port);
        }

        /* Прослушивание входящих соединений */
        private static void StartListening()
        {
            socketListener.Listen(10);

            while (true)
            {
                GUIHelper.LogWaitingForConnections();

                // Приостановка программы в ожидании входящего соединения
                Socket handler = socketListener.Accept();

                // Соединение с клиентом
                byte[] bytes = new byte[1024];
                int byteRec = handler.Receive(bytes);

                bool subtaskRequested = ProcessData(bytes);
                if (subtaskRequested)
                {
                    GUIHelper.LogRequestReceived();
                    Task.Subtask subtask = task.GetSubtask();
                    if (subtask != null)
                    {
                        GUIHelper.LogSendingSubtask(subtask);
                        byte[] msg = task.GetSubtaskAsBytes(subtask);
                        handler.Send(msg);
                        task.SetSubtaskInProcess(subtask);
                    }
                    else
                        GUIHelper.LogNoAvailableSubtasks();
                }

                // Закрытие соединения
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }

        /* Обработка данных, пришедших от клиента.
         * Функция возращает true, если клиент просит подзадание.
         * Сообщение клиента: [0,0] если клиент просит подзадание, иначе
         *  [номер подзадания, результат]. 
         *  Результат = делитель или 1, если делителей не найдено */
        private static bool ProcessData(byte[] bytes)
        {
            long[] data = ByteConverter.ConvertByteArrayToLongArray(bytes);
            if (data[1] == 0) return true;

            // Значит, клиент прислал результат подзадания
            Task.Subtask subtask = task.GetSubtaskById(data[0]);
            GUIHelper.LogSubtaskResultReceived(subtask, data[1]);
            task.SetResultForSubtask(subtask, data[1]);
            if (task.CalculateResult())
            {
                GUIHelper.LogTaskIsCalculated(task);
                Stop();
            }

            return false;
        }

        /* Остановка работы сервера */
        public static void Stop()
        {
            socketListener.Close();
            serverThread.Abort();
            GUIHelper.LogServerStopped();
        }
    }
}
