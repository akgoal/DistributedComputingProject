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
        delegate void LogDelegate(string str);
        public GUIForm()
        {
            InitializeComponent();
          //  GUIHelper.StartClient();
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

        private void button1_Click(object sender, EventArgs e)
        {
            GUIHelper.StartClient();
        }
    }
}
