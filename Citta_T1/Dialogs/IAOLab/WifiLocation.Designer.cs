﻿namespace C2.Dialogs.IAOLab
{
    partial class WifiLocation
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
            this.inputAndResult = new System.Windows.Forms.RichTextBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.Button();
            this.cancle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tipLable = new System.Windows.Forms.Label();
            this.sixTransform = new System.Windows.Forms.Panel();
            this.wgs_gcj = new System.Windows.Forms.RadioButton();
            this.bd_gcj = new System.Windows.Forms.RadioButton();
            this.wgs_bd = new System.Windows.Forms.RadioButton();
            this.gcj_wgs = new System.Windows.Forms.RadioButton();
            this.gcj_bd = new System.Windows.Forms.RadioButton();
            this.bd_wgs = new System.Windows.Forms.RadioButton();
            this.methodPanel = new System.Windows.Forms.Panel();
            this.computeDistance = new System.Windows.Forms.RadioButton();
            this.xyTtransform = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.sixTransform.SuspendLayout();
            this.methodPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputAndResult
            // 
            this.inputAndResult.BackColor = System.Drawing.SystemColors.ControlLight;
            this.inputAndResult.Location = new System.Drawing.Point(9, 144);
            this.inputAndResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputAndResult.Name = "inputAndResult";
            this.inputAndResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputAndResult.Size = new System.Drawing.Size(570, 142);
            this.inputAndResult.TabIndex = 0;
            this.inputAndResult.Text = "";
            this.inputAndResult.WordWrap = false;
            this.inputAndResult.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Font = new System.Drawing.Font("宋体", 11F);
            this.inputLabel.Location = new System.Drawing.Point(19, 14);
            this.inputLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(151, 15);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "请在下方输入MAC地址";
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(424, 10);
            this.confirm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(56, 24);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "查询";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // cancle
            // 
            this.cancle.Location = new System.Drawing.Point(522, 10);
            this.cancle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cancle.Name = "cancle";
            this.cancle.Size = new System.Drawing.Size(56, 24);
            this.cancle.TabIndex = 4;
            this.cancle.Text = "取消";
            this.cancle.UseVisualStyleBackColor = true;
            this.cancle.Click += new System.EventHandler(this.Cancle_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.confirm);
            this.panel1.Controls.Add(this.cancle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 300);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 44);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 6;
            // 
            // tipLable
            // 
            this.tipLable.AutoSize = true;
            this.tipLable.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tipLable.Location = new System.Drawing.Point(22, 107);
            this.tipLable.Name = "tipLable";
            this.tipLable.Size = new System.Drawing.Size(323, 24);
            this.tipLable.TabIndex = 7;
            this.tipLable.Text = "单次输入格式：04a1518006c2\r\n批量查询格式：多个mac间用\\n换行，最多支1000条同时查询";
            this.tipLable.Click += new System.EventHandler(this.tipLable_Click);
            // 
            // sixTransform
            // 
            this.sixTransform.Controls.Add(this.wgs_gcj);
            this.sixTransform.Controls.Add(this.bd_gcj);
            this.sixTransform.Controls.Add(this.wgs_bd);
            this.sixTransform.Controls.Add(this.gcj_wgs);
            this.sixTransform.Controls.Add(this.gcj_bd);
            this.sixTransform.Controls.Add(this.bd_wgs);
            this.sixTransform.Location = new System.Drawing.Point(25, 44);
            this.sixTransform.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sixTransform.Name = "sixTransform";
            this.sixTransform.Size = new System.Drawing.Size(512, 61);
            this.sixTransform.TabIndex = 8;
            this.sixTransform.Visible = false;
            // 
            // wgs_gcj
            // 
            this.wgs_gcj.AutoSize = true;
            this.wgs_gcj.Location = new System.Drawing.Point(359, 26);
            this.wgs_gcj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.wgs_gcj.Name = "wgs_gcj";
            this.wgs_gcj.Size = new System.Drawing.Size(161, 16);
            this.wgs_gcj.TabIndex = 5;
            this.wgs_gcj.TabStop = true;
            this.wgs_gcj.Text = "wgsgcj:国际坐标系转国标";
            this.wgs_gcj.UseVisualStyleBackColor = true;
            // 
            // bd_gcj
            // 
            this.bd_gcj.AutoSize = true;
            this.bd_gcj.Location = new System.Drawing.Point(185, 26);
            this.bd_gcj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bd_gcj.Name = "bd_gcj";
            this.bd_gcj.Size = new System.Drawing.Size(155, 16);
            this.bd_gcj.TabIndex = 4;
            this.bd_gcj.TabStop = true;
            this.bd_gcj.Text = "bdgcj:百度坐标系转国标";
            this.bd_gcj.UseVisualStyleBackColor = true;
            // 
            // wgs_bd
            // 
            this.wgs_bd.AutoSize = true;
            this.wgs_bd.Location = new System.Drawing.Point(4, 27);
            this.wgs_bd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.wgs_bd.Name = "wgs_bd";
            this.wgs_bd.Size = new System.Drawing.Size(155, 16);
            this.wgs_bd.TabIndex = 3;
            this.wgs_bd.TabStop = true;
            this.wgs_bd.Text = "wgsbd:国际坐标系转百度";
            this.wgs_bd.UseVisualStyleBackColor = true;
            // 
            // gcj_wgs
            // 
            this.gcj_wgs.AutoSize = true;
            this.gcj_wgs.Location = new System.Drawing.Point(359, 6);
            this.gcj_wgs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcj_wgs.Name = "gcj_wgs";
            this.gcj_wgs.Size = new System.Drawing.Size(161, 16);
            this.gcj_wgs.TabIndex = 2;
            this.gcj_wgs.TabStop = true;
            this.gcj_wgs.Text = "gcjwgs:国标转国际坐标系";
            this.gcj_wgs.UseVisualStyleBackColor = true;
            // 
            // gcj_bd
            // 
            this.gcj_bd.AutoSize = true;
            this.gcj_bd.Location = new System.Drawing.Point(185, 7);
            this.gcj_bd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcj_bd.Name = "gcj_bd";
            this.gcj_bd.Size = new System.Drawing.Size(155, 16);
            this.gcj_bd.TabIndex = 1;
            this.gcj_bd.TabStop = true;
            this.gcj_bd.Text = "gcjbd:国标坐标系转百度";
            this.gcj_bd.UseVisualStyleBackColor = true;
            // 
            // bd_wgs
            // 
            this.bd_wgs.AutoSize = true;
            this.bd_wgs.Location = new System.Drawing.Point(3, 7);
            this.bd_wgs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bd_wgs.Name = "bd_wgs";
            this.bd_wgs.Size = new System.Drawing.Size(155, 16);
            this.bd_wgs.TabIndex = 0;
            this.bd_wgs.TabStop = true;
            this.bd_wgs.Text = "bdwgs:百度坐标系转国际";
            this.bd_wgs.UseVisualStyleBackColor = true;
            // 
            // methodPanel
            // 
            this.methodPanel.Controls.Add(this.computeDistance);
            this.methodPanel.Controls.Add(this.xyTtransform);
            this.methodPanel.Location = new System.Drawing.Point(25, 10);
            this.methodPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.methodPanel.Name = "methodPanel";
            this.methodPanel.Size = new System.Drawing.Size(512, 25);
            this.methodPanel.TabIndex = 9;
            this.methodPanel.Visible = false;
            this.methodPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.methodPanel_Paint);
            // 
            // computeDistance
            // 
            this.computeDistance.AutoSize = true;
            this.computeDistance.Location = new System.Drawing.Point(279, 2);
            this.computeDistance.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.computeDistance.Name = "computeDistance";
            this.computeDistance.Size = new System.Drawing.Size(107, 16);
            this.computeDistance.TabIndex = 2;
            this.computeDistance.TabStop = true;
            this.computeDistance.Text = "两坐标间距查询";
            this.computeDistance.UseVisualStyleBackColor = true;
            this.computeDistance.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ComputeDistance_MouseClick);
            // 
            // xyTtransform
            // 
            this.xyTtransform.AutoSize = true;
            this.xyTtransform.Location = new System.Drawing.Point(2, 2);
            this.xyTtransform.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.xyTtransform.Name = "xyTtransform";
            this.xyTtransform.Size = new System.Drawing.Size(107, 16);
            this.xyTtransform.TabIndex = 1;
            this.xyTtransform.TabStop = true;
            this.xyTtransform.Text = "经纬度坐标转换";
            this.xyTtransform.UseVisualStyleBackColor = true;
            this.xyTtransform.MouseClick += new System.Windows.Forms.MouseEventHandler(this.XYTtransform_MouseClick);
            // 
            // WifiLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(602, 344);
            this.Controls.Add(this.methodPanel);
            this.Controls.Add(this.sixTransform);
            this.Controls.Add(this.tipLable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.inputAndResult);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(618, 383);
            this.MinimumSize = new System.Drawing.Size(618, 383);
            this.Name = "WifiLocation";
            this.Text = "Wifi查询";
            this.Load += new System.EventHandler(this.WifiLocation_Load);
            this.panel1.ResumeLayout(false);
            this.sixTransform.ResumeLayout(false);
            this.sixTransform.PerformLayout();
            this.methodPanel.ResumeLayout(false);
            this.methodPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox inputAndResult;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tipLable;
        private System.Windows.Forms.Panel sixTransform;
        private System.Windows.Forms.RadioButton gcj_bd;
        private System.Windows.Forms.RadioButton bd_wgs;
        private System.Windows.Forms.Panel methodPanel;
        private System.Windows.Forms.RadioButton computeDistance;
        private System.Windows.Forms.RadioButton xyTtransform;
        private System.Windows.Forms.RadioButton wgs_gcj;
        private System.Windows.Forms.RadioButton bd_gcj;
        private System.Windows.Forms.RadioButton wgs_bd;
        private System.Windows.Forms.RadioButton gcj_wgs;
    }
}