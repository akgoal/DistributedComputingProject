using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class GUIForm : Form
    {
        private bool state = false;

        delegate void LogDelegate(string str);
        public GUIForm()
        {
            InitializeComponent();
        }

        public void InvokeLog(string str)
        {
            this.Invoke(new LogDelegate(Log), str);
        }

        public void Log(string str)
        {
            richTextBox.AppendText(str+"\n");
        }

        private void GUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GUIHelper.StopClient();
        }

        private void button_state_Click(object sender, EventArgs e)
        {
            if (state)
            {
                GUIHelper.StopClient();
                state = false;
                button_state.Image = Properties.Resources.start;
            }
            else
            {
                GUIHelper.StartClient();
                state = true;
                button_state.Image = Properties.Resources.stop;
            }
        }

        private void GUIForm_Load(object sender, EventArgs e)
        {
            GUIHelper.ConfigurateClient();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GUIHelper.Log("Загрузка ЦП: " + GUIHelper.GetCPUUsage());
        }

        public void ShowButtons()
        {
            button_state.Visible = true;
            button3.Visible = true;
        }
    }
}
