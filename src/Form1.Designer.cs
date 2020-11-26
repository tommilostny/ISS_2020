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
            this.plotViewMaskOffTone = new OxyPlot.WindowsForms.PlotView();
            this.plotViewMaskOnTone = new OxyPlot.WindowsForms.PlotView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.minTrackBar = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.maxTrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrackBar)).BeginInit();
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
            // plotViewMaskOffTone
            // 
            this.plotViewMaskOffTone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotViewMaskOffTone.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotViewMaskOffTone.Location = new System.Drawing.Point(3, 3);
            this.plotViewMaskOffTone.Name = "plotViewMaskOffTone";
            this.plotViewMaskOffTone.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewMaskOffTone.Size = new System.Drawing.Size(823, 303);
            this.plotViewMaskOffTone.TabIndex = 5;
            this.plotViewMaskOffTone.Text = "plotViewMaskOffTone";
            this.plotViewMaskOffTone.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewMaskOffTone.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewMaskOffTone.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewMaskOnTone
            // 
            this.plotViewMaskOnTone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotViewMaskOnTone.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotViewMaskOnTone.Location = new System.Drawing.Point(3, 3);
            this.plotViewMaskOnTone.Name = "plotViewMaskOnTone";
            this.plotViewMaskOnTone.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewMaskOnTone.Size = new System.Drawing.Size(823, 309);
            this.plotViewMaskOnTone.TabIndex = 6;
            this.plotViewMaskOnTone.Text = "plotView1";
            this.plotViewMaskOnTone.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewMaskOnTone.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewMaskOnTone.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(241, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.plotViewMaskOffTone);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.plotViewMaskOnTone);
            this.splitContainer1.Size = new System.Drawing.Size(829, 629);
            this.splitContainer1.SplitterDistance = 309;
            this.splitContainer1.TabIndex = 8;
            // 
            // minTrackBar
            // 
            this.minTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.minTrackBar.LargeChange = 50;
            this.minTrackBar.Location = new System.Drawing.Point(41, 241);
            this.minTrackBar.Maximum = 99;
            this.minTrackBar.Name = "minTrackBar";
            this.minTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.minTrackBar.Size = new System.Drawing.Size(56, 378);
            this.minTrackBar.SmallChange = 5;
            this.minTrackBar.TabIndex = 7;
            this.minTrackBar.ValueChanged += new System.EventHandler(this.MinTrackBar_ValueChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 161);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 33);
            this.button2.TabIndex = 6;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ZoomPlusButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(106, 161);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 33);
            this.button3.TabIndex = 7;
            this.button3.Text = "-";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ZoomMinusButton_Click);
            // 
            // maxTrackBar
            // 
            this.maxTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.maxTrackBar.LargeChange = 50;
            this.maxTrackBar.Location = new System.Drawing.Point(106, 241);
            this.maxTrackBar.Maximum = 100;
            this.maxTrackBar.Minimum = 1;
            this.maxTrackBar.Name = "maxTrackBar";
            this.maxTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.maxTrackBar.Size = new System.Drawing.Size(56, 378);
            this.maxTrackBar.SmallChange = 5;
            this.maxTrackBar.TabIndex = 10;
            this.maxTrackBar.Value = 100;
            this.maxTrackBar.ValueChanged += new System.EventHandler(this.MaxTrackBar_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "From 0s to 1s.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxTrackBar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.minTrackBar);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComboBox comboBox1;
        private Label label1;
        private Button buttonPlay;
        private Button buttonStop;
        private OxyPlot.WindowsForms.PlotView plotViewMaskOffTone;
        private OxyPlot.WindowsForms.PlotView plotViewMaskOnTone;
        private SplitContainer splitContainer1;
        private TrackBar minTrackBar;
        private Button button2;
        private Button button3;
        private TrackBar maxTrackBar;
        private Label label3;
    }
}

