using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Settings
    {
        /* Класс-синглетон для хранения настроек */

        private static Settings settings;

        /* GUI форма */
        private GUIForm form;

        /* Допустимый предел загрузки CPU для получения задания */
        private float permissibleCPULimit;

        /* Время ожидания повторного запроса в секундах */
        private int delayInterval;
        public static Settings GetInstance()
        {
            if (settings == null)
                settings = new Settings();
            return settings;
        }

        private Settings()
        {

        }

        public GUIForm GetForm()
        {
            return form;
        }

        public void SetForm(GUIForm form)
        {
            this.form = form;
        }

        public float GetPermissibleCPULimit()
        {
            return permissibleCPULimit;
        }

        public void SetPermissibleCPULimit(float limit)
        {
            this.permissibleCPULimit = limit;
        }

        public int GetDelayInterval()
        {
            return delayInterval;
        }

        public void SetDelayInterval(int delay)
        {
            this.delayInterval = delay;
        }
    }
}
