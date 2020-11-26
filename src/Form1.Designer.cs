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
            this.plotsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.plotView2 = new OxyPlot.WindowsForms.PlotView();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.minTrackBar = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.maxTrackBar = new System.Windows.Forms.TrackBar();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.plotsSplitContainer)).BeginInit();
            this.plotsSplitContainer.Panel1.SuspendLayout();
            this.plotsSplitContainer.Panel2.SuspendLayout();
            this.plotsSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(106, 89);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(85, 33);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop";
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
            // plotsSplitContainer
            // 
            this.plotsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotsSplitContainer.Location = new System.Drawing.Point(241, 12);
            this.plotsSplitContainer.Name = "plotsSplitContainer";
            this.plotsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // plotsSplitContainer.Panel1
            // 
            this.plotsSplitContainer.Panel1.Controls.Add(this.plotViewMaskOffTone);
            this.plotsSplitContainer.Panel1.Controls.Add(this.label6);
            this.plotsSplitContainer.Panel1.Controls.Add(this.splitContainer2);
            this.plotsSplitContainer.Panel1.Controls.Add(this.label5);
            this.plotsSplitContainer.Panel1.Controls.Add(this.button5);
            this.plotsSplitContainer.Panel1.Controls.Add(this.button8);
            this.plotsSplitContainer.Panel1.Controls.Add(this.button6);
            this.plotsSplitContainer.Panel1.Controls.Add(this.button7);
            // 
            // plotsSplitContainer.Panel2
            // 
            this.plotsSplitContainer.Panel2.Controls.Add(this.plotViewMaskOnTone);
            this.plotsSplitContainer.Size = new System.Drawing.Size(829, 629);
            this.plotsSplitContainer.SplitterDistance = 309;
            this.plotsSplitContainer.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-119, 422);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Zoom:";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(-32, 255);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.plotView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.plotView2);
            this.splitContainer2.Size = new System.Drawing.Size(829, 629);
            this.splitContainer2.SplitterDistance = 309;
            this.splitContainer2.TabIndex = 8;
            // 
            // plotView1
            // 
            this.plotView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotView1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotView1.Location = new System.Drawing.Point(3, 3);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(823, 303);
            this.plotView1.TabIndex = 5;
            this.plotView1.Text = "plotViewMaskOffTone";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotView2
            // 
            this.plotView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotView2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotView2.Location = new System.Drawing.Point(3, 3);
            this.plotView2.Name = "plotView2";
            this.plotView2.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView2.Size = new System.Drawing.Size(823, 309);
            this.plotView2.TabIndex = 6;
            this.plotView2.Text = "plotView1";
            this.plotView2.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView2.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView2.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-119, 548);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Move:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(-119, 484);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(81, 33);
            this.button5.TabIndex = 7;
            this.button5.Text = "Out";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(-119, 610);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(81, 33);
            this.button8.TabIndex = 13;
            this.button8.Text = "Backward";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.MoveBackwardButton_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(-119, 445);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(81, 33);
            this.button6.TabIndex = 6;
            this.button6.Text = "In";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(-119, 571);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(81, 33);
            this.button7.TabIndex = 12;
            this.button7.Text = "Forward";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.MoveForwardButton_Click);
            // 
            // minTrackBar
            // 
            this.minTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.minTrackBar.LargeChange = 50;
            this.minTrackBar.Location = new System.Drawing.Point(12, 225);
            this.minTrackBar.Maximum = 99;
            this.minTrackBar.Name = "minTrackBar";
            this.minTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.minTrackBar.Size = new System.Drawing.Size(56, 394);
            this.minTrackBar.SmallChange = 5;
            this.minTrackBar.TabIndex = 7;
            this.minTrackBar.ValueChanged += new System.EventHandler(this.MinTrackBar_ValueChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(154, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 33);
            this.button2.TabIndex = 6;
            this.button2.Text = "In";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(154, 241);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 33);
            this.button3.TabIndex = 7;
            this.button3.Text = "Out";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // maxTrackBar
            // 
            this.maxTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.maxTrackBar.LargeChange = 50;
            this.maxTrackBar.Location = new System.Drawing.Point(56, 225);
            this.maxTrackBar.Maximum = 100;
            this.maxTrackBar.Minimum = 1;
            this.maxTrackBar.Name = "maxTrackBar";
            this.maxTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.maxTrackBar.Size = new System.Drawing.Size(56, 394);
            this.maxTrackBar.SmallChange = 5;
            this.maxTrackBar.TabIndex = 10;
            this.maxTrackBar.Value = 100;
            this.maxTrackBar.ValueChanged += new System.EventHandler(this.MaxTrackBar_ValueChanged);
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Location = new System.Drawing.Point(12, 202);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(95, 20);
            this.intervalLabel.TabIndex = 11;
            this.intervalLabel.Text = "from 0s to 1s";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 33);
            this.button1.TabIndex = 12;
            this.button1.Text = "Forward";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.MoveForwardButton_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(154, 367);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 33);
            this.button4.TabIndex = 13;
            this.button4.Text = "Backward";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.MoveBackwardButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Move:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Zoom:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Interval:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.intervalLabel);
            this.Controls.Add(this.maxTrackBar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.minTrackBar);
            this.Controls.Add(this.plotsSplitContainer);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ISS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.plotsSplitContainer.Panel1.ResumeLayout(false);
            this.plotsSplitContainer.Panel1.PerformLayout();
            this.plotsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotsSplitContainer)).EndInit();
            this.plotsSplitContainer.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
        private SplitContainer plotsSplitContainer;
        private TrackBar minTrackBar;
        private Button button2;
        private Button button3;
        private TrackBar maxTrackBar;
        private Label intervalLabel;
        private Button button1;
        private Button button4;
        private Label label2;
        private Label label4;
        private Label label6;
        private SplitContainer splitContainer2;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private Label label5;
        private Button button5;
        private Button button8;
        private Button button6;
        private Button button7;
        private Label label3;
    }
}

