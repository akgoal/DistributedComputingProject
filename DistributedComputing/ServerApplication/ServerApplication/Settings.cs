using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Settings
    {
        /* Класс-синглетон для хранения настроек */

        private static Settings settings;

        /* Размер отрезка проверяемых делителей */
        private long rangeLength;

        /* GUI форма */
        private GUIForm form;

        public static Settings GetInstance()
        {
            if (settings == null)
                settings = new Settings();
            return settings;
        }

        private Settings()
        {

        }

        public long GetRangeLength()
        {
            return rangeLength;
        }

        public void SetRangeLength(long length)
        {
            rangeLength = length;
        }

        public GUIForm GetForm()
        {
            return form;
        }

        public void SetForm(GUIForm form)
        {
            this.form = form;
        }
    }
}
