using System;
using System.Windows.Forms;

namespace src
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "maskoff_tone",
            "maskon_tone"});
            this.comboBox1.Location = new System.Drawing.Point(12, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(179, 28);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.OnButtonStopClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select file:";
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(12, 89);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(85, 33);
            this.buttonPlay.TabIndex = 3;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.Click += new System.EventHandler(this.OnButtonPlayClick);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(106, 89);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(85, 33);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop";
            this.buttonStop.Click += new System.EventHandler(this.OnButtonStopClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComboBox comboBox1;
        private Label label1;
        private Button buttonPlay;
        private Button buttonStop;
    }
}

