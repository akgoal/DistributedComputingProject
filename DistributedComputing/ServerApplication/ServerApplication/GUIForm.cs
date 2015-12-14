using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServerApplication
{
    public partial class GUIForm : Form
    {
        delegate void LogDelegate(string str);
       
        public GUIForm()
        {
            InitializeComponent();
            //GUIHelper.StartServer();
        }

        public void InvokeLog(string str)
        {
            this.Invoke(new LogDelegate(Log), str);
        }

        public void Log(string str)
        {
            richTextBox.Text += str + "\n";
        }

        private void GUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
         //   GUIHelper.StopServer();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (textBox_task.Text == "" || textBox_range.Text == "")
                MessageBox.Show("Empty fields! Please enter the data");
            else
            {
                GUIHelper.StartServer();
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            GUIHelper.StopServer();
        }

        public long GetNumber()
        {
            long num = Convert.ToInt64(textBox_task.Text);
            return num;
        }

        public int GetRange()
        {
            int range = Convert.ToInt32(textBox_range.Text);
            return range;
        }


    }
}
