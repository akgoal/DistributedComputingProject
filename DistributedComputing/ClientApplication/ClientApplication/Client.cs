using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ClientApplication
{
    class Client
    {
        /* Класс описания работы клиента */

        /* Поток работы сервера */
        private static Thread clientThread;

        /* Помощник в работе с системой */
        private static SystemHelper system;

        /* Удаленная точка */
        private static IPEndPoint ipEndPoint;

        /* Запуск потока работы клиента */
        public static void Start()
        {
            clientThread = new Thread(StartThread);
            clientThread.Start();
        }

        /* Запуск клиента */
        public static void StartThread()
        {
            Thread.Sleep(100);

            GUIHelper.LogConfiguratingClient();

            system = new SystemHelper();
            Settings.GetInstance().SetDelayInterval(GUIHelper.GetDelayInterval());
            Settings.GetInstance().SetPermissibleCPULimit(GUIHelper.GetPermissibleCPULimit());

            while (true)
            {
                if (RequestIsPermitted())
                {
                    Task task = SendRequestAndReceiveTask();
                    if (task.IsProper())
                    {
                        task.CalculateResult();
                        SendResult(task);
                    }
                    else
                        GUIHelper.LogImproperTask();
                }
                else
                {
                    Delay();
                }
            }
        }


        /* Проверка на возможность получения задания.
         * Конкретно, проверяется загруженность процессора. */
        private static bool RequestIsPermitted()
        {
            float CPUUsage = system.GetCPUUsage();
            if (system.GetCPUUsage() > Settings.GetInstance().GetPermissibleCPULimit())
            {
                GUIHelper.LogHighCPUUsage(CPUUsage);
                return false;
            }
            else
                return true;
        }

        /* Отправка запроса на получения подзадания. Получение подзадания */
        private static Task SendRequestAndReceiveTask()
        {
            try
            {
                // Установка удаленной точки для сокета
                IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                IPAddress ipAddr = ipHost.AddressList[0];
                int port = 11000;
                ipEndPoint = new IPEndPoint(ipAddr, port);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Соединение сокета с удаленным устройством
                sender.Connect(ipEndPoint);

                byte[] message = MakeMessage(null);

                GUIHelper.LogSendingRequest();
                sender.Send(message);

                byte[] bytes = new byte[1024];
                sender.Receive(bytes);
                Task task = new Task(bytes);
                GUIHelper.LogTaskReceived(task);

                /* Освобождение сокета */
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

                return task;
            }
            catch (Exception ex)
            {
                GUIHelper.LogServerIsUnavailable(true);
                Thread.Sleep(2000);
                return SendRequestAndReceiveTask();
            }
        }

        /* Формирование сообщения.
         * Формируется сообщение вида [id, res], где id - идентификатор задания task, res - результат
         * Если task==null, то формируется запрос на получение задания [0,0]. */
        private static byte[] MakeMessage(Task task)
        {
            long[] data;
            if (task == null)
            {
                data = new long[] { 0, 0 };
            }
            else
            {
                data = new long[] { task.GetId(), task.GetResult() };
            }
            return ByteConverter.ConvertLongArrayToByteArray(data); ;
        }

        /* Отправка результата вычисления на сервер */
        private static void SendResult(Task task)
        {
            try
            {
                // Установка удаленной точки для сокета
                IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                IPAddress ipAddr = ipHost.AddressList[0];
                int port = 11000;
                ipEndPoint = new IPEndPoint(ipAddr, port);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Соединение сокета с удаленным устройством
                sender.Connect(ipEndPoint);

                byte[] message = MakeMessage(task);

                GUIHelper.LogSendingResult(task);
                sender.Send(message);

                /* Освобождение сокета */
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception ex)
            {
                GUIHelper.LogServerIsUnavailable(false);
                Thread.Sleep(2000);
                SendResult(task);
            }
        }

        /* Ожидание до следующей попытки отправить запрос */
        private static void Delay()
        {
            int interval = Settings.GetInstance().GetDelayInterval();

            while (interval > 0)
            {
                GUIHelper.LogDelayBeforeRequest(interval);
                Thread.Sleep(1000);
                interval--;
            }
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            GUIHelper.Log("sfsdfs");
        }

        /* Остановка работы клиента */
        public static void Stop()
        {
            clientThread.Abort();
            clientThread = null;
            GUIHelper.LogClientStopped();
        }
    }
}
