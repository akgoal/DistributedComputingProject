using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Task
    {
        /* Класс, описывающий задание, выполняемое клиентом.
        * Содержит проверяемое число, границы отрезка возможных делителей и результат проверки.
        * Результат - делитель, если такой есть; DIVISOR_NOT_FOUND - делители не найдены; NOT_CALCULATED - результат еще не вычислен. */
        public static readonly long NOT_CALCULATED = 0;
        public static readonly long DIVISOR_NOT_FOUND = 1;

        private long id;
        private long number;
        private long result;
        private long rangeFrom;
        private long rangeTo;

        public Task(long id, long number, long rangeFrom, long rangeTo)
        {
            this.id = id;
            this.number = number;
            this.rangeFrom = rangeFrom;
            this.rangeTo = rangeTo;
            this.result = NOT_CALCULATED;
        }

        /* Конструктор по следующих данным:
         * id,rangeFrom,rangeTo,number в виде массива байтов */
        public Task(byte[] bytes)
        {
            long[] data = ByteConverter.ConvertByteArrayToLongArray(bytes);
            this.id = data[0];
            this.number = data[3];
            this.rangeFrom = data[1];
            this.rangeTo = data[2];
            this.result = NOT_CALCULATED;
        }

        /* Известен ли результат проверки */
        public bool IsCalculated()
        {
            return result != NOT_CALCULATED;
        }

        /* Вычисление задания */
        public void CalculateResult()
        {
            Thread.Sleep(1000); // ДЛЯ ОТЛАДКИ


            result = DIVISOR_NOT_FOUND;
            for (long i = rangeFrom; i <= rangeTo; i++)
            {
                if (number % i == 0)
                {
                    result = i;
                    return;
                }
            }
        }

        /* Корректность задания */
        public bool IsProper()
        { 
            return (rangeFrom!=0 && rangeTo!=0);
        }

        /* Getters */
        public long GetNumber()
        {
            return number;
        }

        public long GetId()
        {
            return id;
        }
        public long GetResult()
        {
            return result;
        }

        public long[] GetRange()
        {
            long[] range = { rangeFrom, rangeTo };
            return range;
        }
    }
}
