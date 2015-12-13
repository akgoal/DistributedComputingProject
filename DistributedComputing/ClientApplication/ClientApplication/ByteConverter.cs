using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class ByteConverter
    {
        /* Класс для работы с массивом байтов */

        /* Конвертирование int[] в byte[] */
        public static byte[] ConvertIntArrayToByteArray(int[] intArray)
        {
            byte[] byteArray = new byte[intArray.Length * 4];
            for (int i = 0; i < intArray.Length; i++)
                Array.Copy(BitConverter.GetBytes(intArray[i]), 0, byteArray, i * 4, 4);
            return byteArray;
        }

        /* Конвертирование byte[] в int[] */
        public static int[] ConvertByteArrayToIntArray(byte[] byteArray)
        {
            int[] intArray = new int[byteArray.Length / 4];
            for (int i = 0; i < byteArray.Length; i += 4)
                intArray[i / 4] = BitConverter.ToInt32(byteArray, i);
            return intArray;
        }

        /* Конвертирование long[] в byte[] */
        public static byte[] ConvertLongArrayToByteArray(long[] longArray)
        {
            int sizeOfLong = sizeof(long);
            byte[] byteArray = new byte[longArray.Length * sizeOfLong];
            for (int i = 0; i < longArray.Length; i++)
                Array.Copy(BitConverter.GetBytes(longArray[i]), 0, byteArray, i * sizeOfLong, sizeOfLong);
            return byteArray;
        }

        /* Конвертирование byte[] в long[] */
        public static long[] ConvertByteArrayToLongArray(byte[] byteArray)
        {
            int sizeOfLong = sizeof(long);
            long[] longArray = new long[byteArray.Length / sizeOfLong];
            for (int i = 0; i < byteArray.Length; i += sizeOfLong)
                longArray[i / sizeOfLong] = BitConverter.ToInt64(byteArray, i);
            return longArray;
        }
    }

}
