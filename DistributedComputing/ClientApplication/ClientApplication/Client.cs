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

        /* Индикатор рабочего состония */
        private static bool IsWorking = false;

        /* Поток работы сервера */
        private static Thread clientThread;

        /* Удаленная точка */
        private static IPEndPoint ipEndPoint;

        /* Сокет для соединения с сервером */
        private static Socket sender;

        /* Выполняемое клиентом задание */
        private static Task task;

        /* Запуск потока работы клиента */
        public static void Start()
        {
            // Убеждаемся, что клиент не работает
            while (clientThread != null && clientThread.IsAlive) ;
            IsWorking = true;
            clientThread = new Thread(StartThread);
            clientThread.Start();
        }

        /* Запуск клиента */
        public static void StartThread()
        {
            Settings.GetInstance().SetDelayInterval(GUIHelper.GetDelayInterval());
            Settings.GetInstance().SetPermissibleCPULimit(GUIHelper.GetPermissibleCPULimit());

            // Если осталось задание, выполняем его и посылаем результат
            if (task != null && task.IsProper())
            {
                task.CalculateResult();
                SendResult(task);
            }

            while (true)
            {
                if (RequestIsPermitted())
                {
                    task = SendRequestAndReceiveTask();
                    if (task != null && task.IsProper())
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
            float CPUUsage = SystemHelper.GetCPUUsage();
            if (SystemHelper.GetCPUUsage() > Settings.GetInstance().GetPermissibleCPULimit())
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

                sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

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
            catch (SocketException ex)
            {
                // Недоступен сервер
                if (ex.ErrorCode == (int)SocketError.ConnectionRefused)
                {
                    GUIHelper.LogServerIsUnavailable(true);

                    // Пауза на две секунды
                    Thread.Sleep(2000);

                    // Если вызвано завершение работы клиента
                    if (!IsWorking)
                        return null;

                    // Повторная отправка запроса
                    return SendRequestAndReceiveTask(); ;
                }
                return null;
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

                sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Соединение сокета с удаленным устройством
                sender.Connect(ipEndPoint);

                byte[] message = MakeMessage(task);

                GUIHelper.LogSendingResult(task);
                sender.Send(message);

                /* Освобождение сокета */
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (SocketException ex)
            {
                // Недоступен сервер
                if (ex.ErrorCode == (int)SocketError.ConnectionRefused)
                {
                    GUIHelper.LogServerIsUnavailable(true);

                    // Пауза на две секунды
                    Thread.Sleep(2000);

                    // Если вызвано завершение работы клиента
                    if (!IsWorking)
                        return;

                    // Повторная отправка результата
                    SendResult(task);
                }
                return;
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

        /* Остановка работы клиента */
        public static void Stop()
        {
            if (IsWorking)
            {
                IsWorking = false;
                if (sender!=null)
                    sender.Close();
                clientThread.Abort();
                GUIHelper.LogClientStopped();
            }
        }
    }
}
