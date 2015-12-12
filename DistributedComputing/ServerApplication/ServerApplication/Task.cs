using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Task
    {
        /* Класс, описывающий задание.
         * Содержит проверяемое на простоту число, результат проверки и список подзаданий.
         * Результат - делитель, если такой есть; IS_PRIME - если число простое; NOT_CALCULATED - результат еще не вычислен. */
        public static readonly long NOT_CALCULATED = 0;
        public static readonly long IS_PRIME = 1;

        private long number;
        private long result;
        private List<Subtask> subtasks;

        public Task(long number)
        {
            this.number = number;
            this.result = NOT_CALCULATED;

            subtasks = new List<Subtask>();
            long sqrNum = (long)Math.Floor(Math.Sqrt(number));
            long rangeLen = Settings.GetInstance().GetRangeLength();
            long rangeTo;
            int subtaskId = 0;
            for (long rangeFrom = 2; rangeFrom <= sqrNum; rangeFrom = rangeTo + 1)
            {
                rangeTo = (rangeFrom + rangeLen - 1 <= sqrNum) ? (rangeFrom + rangeLen - 1) : sqrNum;
                subtasks.Add(new Subtask(subtaskId, rangeFrom, rangeTo));
                subtaskId++;
            }
            GUIHelper.LogSubTasksCreated(subtasks);
        }

        /* Известен ли результат проверки */
        public bool IsCalculated()
        {
            return result != NOT_CALCULATED;
        }

        /* Функция возвращает подзадание в виде массива байт */
        public byte[] GetSubtaskAsBytes(Subtask subtask)
        {
            return subtask.GetInfoAsBytes(number);
        }

        /* Функция возвращает еще не вычисленное подзадание */
        public Subtask GetSubtask()
        {
            Subtask subtask;
            for (int i = 0; i < subtasks.Count; i++)
            {
                subtask = subtasks[i];
                if (subtask.IsAvailable())
                {
                    return subtask;
                }
            }
            return null;
        }

        /* Изменение статуса подзадания на "вычисляется" */
        public void SetSubtaskInProcess(Subtask subtask)
        {
            subtask.SetInProcess();
        }

        /* Установка результата подзадания с порядковым номер subtaskId */
        public void SetResultForSubtask(Subtask subtask, long result)
        {
            subtask.SetResult(result);
        }

        /* Поиск подзадания по порядковому номеру */
        public Subtask GetSubtaskById(long id)
        {
            int intId = (int)id;
            return subtasks[intId];
        }

        /* Вычисление результата задания.
         * Возвращает true, если вычисление окончено. */
        public bool CalculateResult()
        {
            bool allSubtasksAreCalculated = true;
            foreach (Subtask subtask in subtasks)
            {
                // Если на каком-то отрезке нашелся делитель
                if (subtask.DivisorsFound())
                {
                    result = subtask.GetResult();
                    return true;
                }

                // Если какое-то подзадание еще не вычислено
                if (!subtask.IsCalculated())
                    allSubtasksAreCalculated = false;
            }

            // Если все подзадание вычислены, и делители не найдены
            if (allSubtasksAreCalculated)
            {
                result = IS_PRIME;
                return true;
            }

            return false;
        }

        /* Getters */
        public long GetNumber()
        {
            return number;
        }
        public long GetResult()
        {
            return result;
        }
        public List<Subtask> GetSubtasks()
        {
            return subtasks;
        }

        public class Subtask
        {
            /* Класс, описывающий подзадание.
             * Содержит информацию об порядковом номере подзадания, об отрезке проверяемых делителей и результате подзадания.
             * Результат - делитель, если такой есть; NO_DIVISORS - делители не найдены;
             *  NOT_CALCULATED - результат еще не вычислен; IN_PROCESS - результат вычисляется */
            public static readonly long NOT_CALCULATED = 0;
            public static readonly long IN_PROCESS = -1;
            public static readonly long NO_DIVISORS = 1;

            private int id;
            private long rangeFrom, rangeTo;
            private long result;
            public Subtask(int id, long from, long to)
            {
                this.id = id;
                this.rangeFrom = from;
                this.rangeTo = to;
                this.result = NOT_CALCULATED;
            }

            /* Возвращает информацию о подзадании в виде массива чисел:
             * id,rangeFrom,rangeTo,taskNumber
             * переведенного в массив байтов
             * taskNumber - проверямое число в основном задании */
            public byte[] GetInfoAsBytes(long taskNumber)
            {
                long[] data = { id, rangeFrom, rangeTo, taskNumber };

                return ByteConverter.ConvertLongArrayToByteArray(data);
            }

            /* Доступно ли подзадание для вычисления */
            public bool IsAvailable()
            {
                return result == NOT_CALCULATED;
            }

            /* Вычислено ли задание */
            public bool IsCalculated()
            {
                return result != NOT_CALCULATED && result != IN_PROCESS;
            }

            /* Пометить подзадание как выполняемое */
            public void SetInProcess()
            {
                result = IN_PROCESS;
            }

            /* Установка результата подзадания */
            public void SetResult(long result)
            {
                this.result = result;
            }

            /* Найдены ли делители */
            public bool DivisorsFound()
            {
                return IsCalculated() && result != NO_DIVISORS;
            }

            /* Getters */
            public long GetResult()
            {
                return result;
            }
            public long[] getRange()
            {
                long[] range = { rangeFrom, rangeTo };
                return range;
            }
            public int getId()
            {
                return id;
            }
        }
    }
}
