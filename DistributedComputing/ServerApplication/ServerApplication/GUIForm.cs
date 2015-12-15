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
        private bool state = false;

        delegate void LogDelegate(string str);
        //delegate void WaitLogDelegate();
       
        public GUIForm()
        {
            InitializeComponent();
            //GUIHelper.StartServer();
            textBox_task.Text = Convert.ToString(15485867); //для ОТЛАДКИ
            textBox_range.Text = Convert.ToString(100); //для ОТЛАДКИ
        }

        public void InvokeLog(string str)
        {
            this.Invoke(new LogDelegate(Log), str);
        }

        public void Log(string str)
        {
            richTextBox.Text += str + "\n";
        }

        //public void InvokeWaitLog()
        //{
        //    this.Invoke(new WaitLogDelegate(WaitLog));
        //}

        //public void WaitLog()
        //{
        //    richTextBox.AppendText(".");
        //}

        private void GUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
         //   GUIHelper.StopServer();
        }

        private void button_state_Click(object sender, EventArgs e)
        {
            if (state)
            {
                GUIHelper.StopServer();
               // button_state.Text = "Start";
                state = false;
                button_state.Image = Properties.Resources.start;
                textBox_range.Enabled = true;
                textBox_task.Enabled = true;
            }
            else
            {
                if (textBox_task.Text == "" || textBox_range.Text == "")
                    MessageBox.Show("Пожалуйста заполните все поля!");
                else
                {
                    GUIHelper.StartServer();
                    //button_state.Text = "Stop";
                    state = true;
                    button_state.Image = Properties.Resources.stop;
                    textBox_range.Enabled = false;
                    textBox_task.Enabled = false;
                }
            }
           //
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

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
        }


    }
}
