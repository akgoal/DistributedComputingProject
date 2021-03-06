﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace ClientApplication
{
    public partial class GUIForm : Form
    {
        private bool state = false;

        delegate void LogDelegate(string str);
        delegate void ShowButtonsDelegate();
        delegate int GetCPULimitDelegate();
        delegate void ShowCPULoadDelegate();
        delegate void UpdateCPULabelDelegate(float value);
        delegate void PaintCPULoadDelegate(float value);

        Graphics g;

        public GUIForm()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
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
                trackBar1.Enabled = true;
            }
            else
            {
                GUIHelper.StartClient();
                state = true;
                button_state.Image = Properties.Resources.stop;
                trackBar1.Enabled = false;
            }
        }

        private void GUIForm_Load(object sender, EventArgs e)
        {
            GUIHelper.ConfigurateClient();
        }


        public void ShowButtons()
        {
            button_state.Visible = true;
            pictureBox1.Visible = false;
            trackBar1.Visible = true;
            label1.Visible = true;
            label2.Text = "";
            label2.Visible = true;
            label3.Visible = true;
            panel1.Visible = true;
        }

        public void InvokeShowButtons()
        {
            this.Invoke(new ShowButtonsDelegate(ShowButtons));
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            string value = trackBar1.Value.ToString();
            label1.Text = "Предельное допустимое значение\nзагруженности процессора (" +value+ "%)";
        }

        public int GetCPULimit()
        {
            return trackBar1.Value;
        }

        public int InvokeGetCPULimit()
        {
           return (int)this.Invoke(new GetCPULimitDelegate(GetCPULimit));
        }

        private void UpdateCPUUsage(Object source, ElapsedEventArgs e)
        {
            float CPUUsage = GUIHelper.GetCPUUsage();
            GUIHelper.UpdateCPULabel(CPUUsage);
            GUIHelper.PaintCPULoad(CPUUsage);
        }

        private void ShowCPULoad()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += UpdateCPUUsage;
            timer.Enabled = true;
            timer.AutoReset = true;
        }

        public void InvokeShowCPULoad()
        {
            this.Invoke(new ShowCPULoadDelegate(ShowCPULoad));
        }


        private void UpdateCPULabel(float value)
        {
            label2.Text = value.ToString() + " %";
        }
        public void InvokeUpdateCPULabel(float value)
        {
            this.Invoke(new UpdateCPULabelDelegate(UpdateCPULabel), value);
        }

        private void PaintCPULoad(float value)
        {
            g.Clear(Color.White);
            g.FillRectangle(Brushes.Green, 0, panel1.Height - value, panel1.Width, panel1.Height);
        }

        public void InvokePaintCPULoad(float value)
        {
            this.Invoke(new PaintCPULoadDelegate(PaintCPULoad), value);
        }
    }
}
