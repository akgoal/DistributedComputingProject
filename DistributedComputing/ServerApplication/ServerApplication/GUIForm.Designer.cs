namespace ServerApplication
{
    partial class GUIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.button_state = new System.Windows.Forms.Button();
            this.textBox_task = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_range = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
<<<<<<< HEAD
            this.richTextBox.Enabled = false;
            this.richTextBox.Location = new System.Drawing.Point(24, 103);
=======
            this.richTextBox.Location = new System.Drawing.Point(12, 12);
>>>>>>> origin/master
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(268, 210);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            // 
            // button_state
            // 
            this.button_state.Image = global::ServerApplication.Properties.Resources.start;
            this.button_state.Location = new System.Drawing.Point(303, 12);
            this.button_state.Name = "button_state";
            this.button_state.Size = new System.Drawing.Size(133, 61);
            this.button_state.TabIndex = 2;
            this.button_state.UseVisualStyleBackColor = true;
            this.button_state.Click += new System.EventHandler(this.button_state_Click);
            // 
            // textBox_task
            // 
            this.textBox_task.Location = new System.Drawing.Point(107, 237);
            this.textBox_task.Name = "textBox_task";
            this.textBox_task.Size = new System.Drawing.Size(214, 20);
            this.textBox_task.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Число";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Длина Отрезка";
            // 
            // textBox_range
            // 
            this.textBox_range.Location = new System.Drawing.Point(107, 281);
            this.textBox_range.Name = "textBox_range";
            this.textBox_range.Size = new System.Drawing.Size(214, 20);
            this.textBox_range.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Число может находится в диапазоне ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 348);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(300, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "-9223372036854775808 : 9223372036854775807";
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 380);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_range);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_task);
            this.Controls.Add(this.button_state);
            this.Controls.Add(this.richTextBox);
            this.Name = "GUIForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUIForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button button_state;
        private System.Windows.Forms.TextBox textBox_task;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_range;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;

    }
}