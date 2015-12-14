using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication
{
    class GUIHelper
    {
        /* Запуск клиента */
        public static void StartClient()
        {
            Client.Start();
        }

        /* Остановка клиента */
        public static void StopClient()
        {
            Client.Stop();
        }

        /* Предварительная настройка клиента */
        public static void ConfigurateClient() 
        {
            Log("Настройка клиента");
            /* Инициализация класса SystemHelper, позволяющего получить доступ к системной информации, 
             *  в том числе к данным о загруженности процессора*/
            SystemHelper.Init(new InitHandler());
        }

        /* Действия по завершении предварительной настройки клиента */
        public static void ConfigurationComplete()
        {
            Log("Настройка клиента завершена");
        }

        /* Получение времени задержки между попытками отправить запрос на сервер (в секундах) */
        public static int GetDelayInterval()
        {
            return 5;
        }

        /* Получение предельного допустимого значения загруженности процессора, 
         * при котором выполняется запрос задания (в процентах) */
        public static float GetPermissibleCPULimit()
        {
            return 80;
        }

        /* Загруженность процессора в процентах.
         * Вызывается в GUI.
         * Выбрасывает ошибку, если SystemHelper не инициализирован. */
        public static float GetCPUUsage()
        {
            return SystemHelper.GetCPUUsage();
        }

        /* Следующие методы связаны с журналированием,
         * т.е. фиксированием определенных событий в работе клиента */

        /* Сообщение общего характера для отладки. */
        public static void Log(string str)
        {
            Settings.GetInstance().GetForm().InvokeLog(str);
        }

        /* Клиент завершил работу */
        public static void LogClientStopped()
        {
            Log("Клиент остановлен");   
        }

        /* Ошибка */
        public static void LogException(Exception ex)
        {
            throw ex;
        }

        /* Загруженность процессора больше предельного значения.
         * CPUUsage - загруженность процессора в процентах */
        public static void LogHighCPUUsage(float CPUUsage)
        {
            Log("Загруженный процессор");
        }

        /* Отправка запроса задания */
        public static void LogSendingRequest()
        {
            Log("Отправка запроса");
        }

        /* Отправка результата задания task */
        public static void LogSendingResult(Task task)
        {
            Log("Отправка результата: res=" + task.GetResult()+" id="+task.GetId());
        }

        /* Получено задание task */
        public static void LogTaskReceived(Task task)
        {
            Log("Задание получено: from=" + task.GetRange()[0] +" id="+task.GetId());
        }

        /* Сокет установлен; ipAddr - ip-адрес, port - номер порта */
        public static void LogSocketIsSet(System.Net.IPAddress ipAddr, int port)
        {
            Log("Сокет установлен");
        }

        /* Настройка клиента */
        public static void LogConfiguratingClient()
        {
            Log("Настройка клиента");
        }

        /* Задержки в interval секунд до отправки запроса на сервер */
        public static void LogDelayBeforeRequest(int interval)
        {
            Log("Задержка в " + interval + " сек");
        }

        /* Сервер недоступен.
         * isRequest - является ли сообщение, отправляемое серверу, запросом на задание.
         * Повторная попытка отправки */
        public static void LogServerIsUnavailable(bool isRequest)
        {
            if (isRequest)
                Log("Сервер недоступен. Запрос на получение задания не отправлен. Повторная попытка");
            else
                Log("Сервер недоступен. Результаты задания не отправлены. Повторная попытка");
        }

        /* Некорректное задание */
        public static void LogImproperTask()
        {
            Log("Некорректное задание");
        }


        public class InitHandler : SystemHelper.InitListener
        {
            public void OnInit()
            {
                GUIHelper.ConfigurationComplete();
            }
        }
    }
}
