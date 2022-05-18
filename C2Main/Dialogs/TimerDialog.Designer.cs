﻿namespace C2.Dialogs
{
    partial class TimerDialog
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
            this.components = new System.ComponentModel.Container();
            this.BtnReset = new C2.Controls.PushButton();
            this.BtnStartStop = new C2.Controls.PushButton();
            this.NudHour = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LabHours = new System.Windows.Forms.Label();
            this.LabMinutes = new System.Windows.Forms.Label();
            this.LabSeconds = new System.Windows.Forms.Label();
            this.NudMinute = new System.Windows.Forms.NumericUpDown();
            this.NudSecond = new System.Windows.Forms.NumericUpDown();
            this.LabHourValue = new System.Windows.Forms.Label();
            this.LabMinuteValue = new System.Windows.Forms.Label();
            this.LabSecondValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.NudHour)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudSecond)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(279, 176);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(112, 38);
            this.BtnReset.TabIndex = 2;
            this.BtnReset.Text = "Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnStartStop
            // 
            this.BtnStartStop.Location = new System.Drawing.Point(16, 176);
            this.BtnStartStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnStartStop.Name = "BtnStartStop";
            this.BtnStartStop.Size = new System.Drawing.Size(112, 38);
            this.BtnStartStop.TabIndex = 1;
            this.BtnStartStop.Text = "Start";
            this.BtnStartStop.UseVisualStyleBackColor = true;
            this.BtnStartStop.Click += new System.EventHandler(this.BtnStartStop_Click);
            // 
            // NudHour
            // 
            this.NudHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NudHour.Location = new System.Drawing.Point(4, 39);
            this.NudHour.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NudHour.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NudHour.Name = "NudHour";
            this.NudHour.Size = new System.Drawing.Size(100, 28);
            this.NudHour.TabIndex = 3;
            this.NudHour.ValueChanged += new System.EventHandler(this.Nud_ValueChanged);
            this.NudHour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NudHour_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.LabHours, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.NudHour, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabMinutes, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabSeconds, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.NudMinute, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.NudSecond, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabHourValue, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.LabMinuteValue, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.LabSecondValue, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(375, 123);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // LabHours
            // 
            this.LabHours.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabHours.AutoSize = true;
            this.LabHours.Location = new System.Drawing.Point(27, 8);
            this.LabHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabHours.Name = "LabHours";
            this.LabHours.Size = new System.Drawing.Size(53, 18);
            this.LabHours.TabIndex = 0;
            this.LabHours.Text = "Hours";
            // 
            // LabMinutes
            // 
            this.LabMinutes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabMinutes.AutoSize = true;
            this.LabMinutes.Location = new System.Drawing.Point(151, 8);
            this.LabMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabMinutes.Name = "LabMinutes";
            this.LabMinutes.Size = new System.Drawing.Size(71, 18);
            this.LabMinutes.TabIndex = 1;
            this.LabMinutes.Text = "Minutes";
            // 
            // LabSeconds
            // 
            this.LabSeconds.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabSeconds.AutoSize = true;
            this.LabSeconds.Location = new System.Drawing.Point(285, 8);
            this.LabSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabSeconds.Name = "LabSeconds";
            this.LabSeconds.Size = new System.Drawing.Size(71, 18);
            this.LabSeconds.TabIndex = 2;
            this.LabSeconds.Text = "Seconds";
            // 
            // NudMinute
            // 
            this.NudMinute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NudMinute.Location = new System.Drawing.Point(137, 39);
            this.NudMinute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NudMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.NudMinute.Name = "NudMinute";
            this.NudMinute.Size = new System.Drawing.Size(100, 28);
            this.NudMinute.TabIndex = 4;
            this.NudMinute.ValueChanged += new System.EventHandler(this.Nud_ValueChanged);
            this.NudMinute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NudHour_KeyDown);
            // 
            // NudSecond
            // 
            this.NudSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NudSecond.Location = new System.Drawing.Point(270, 39);
            this.NudSecond.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NudSecond.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.NudSecond.Name = "NudSecond";
            this.NudSecond.Size = new System.Drawing.Size(101, 28);
            this.NudSecond.TabIndex = 5;
            this.NudSecond.ValueChanged += new System.EventHandler(this.Nud_ValueChanged);
            this.NudSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NudHour_KeyDown);
            // 
            // LabHourValue
            // 
            this.LabHourValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabHourValue.AutoSize = true;
            this.LabHourValue.Location = new System.Drawing.Point(41, 87);
            this.LabHourValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabHourValue.Name = "LabHourValue";
            this.LabHourValue.Size = new System.Drawing.Size(26, 18);
            this.LabHourValue.TabIndex = 6;
            this.LabHourValue.Text = "00";
            // 
            // LabMinuteValue
            // 
            this.LabMinuteValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabMinuteValue.AutoSize = true;
            this.LabMinuteValue.Location = new System.Drawing.Point(174, 87);
            this.LabMinuteValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabMinuteValue.Name = "LabMinuteValue";
            this.LabMinuteValue.Size = new System.Drawing.Size(26, 18);
            this.LabMinuteValue.TabIndex = 7;
            this.LabMinuteValue.Text = "00";
            // 
            // LabSecondValue
            // 
            this.LabSecondValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabSecondValue.AutoSize = true;
            this.LabSecondValue.Location = new System.Drawing.Point(307, 87);
            this.LabSecondValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabSecondValue.Name = "LabSecondValue";
            this.LabSecondValue.Size = new System.Drawing.Size(26, 18);
            this.LabSecondValue.TabIndex = 8;
            this.LabSecondValue.Text = "00";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = ":";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = ":";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(112, 87);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = ":";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(245, 87);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = ":";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 148);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(375, 12);
            this.progressBar1.TabIndex = 3;
            // 
            // TimerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 230);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BtnStartStop);
            this.Controls.Add(this.BtnReset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TimerDialog";
            this.Text = "Timer";
            ((System.ComponentModel.ISupportInitialize)(this.NudHour)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudSecond)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PushButton BtnReset;
        private Controls.PushButton BtnStartStop;
        private System.Windows.Forms.NumericUpDown NudHour;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LabHours;
        private System.Windows.Forms.Label LabMinutes;
        private System.Windows.Forms.Label LabSeconds;
        private System.Windows.Forms.NumericUpDown NudMinute;
        private System.Windows.Forms.NumericUpDown NudSecond;
        private System.Windows.Forms.Label LabHourValue;
        private System.Windows.Forms.Label LabMinuteValue;
        private System.Windows.Forms.Label LabSecondValue;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}