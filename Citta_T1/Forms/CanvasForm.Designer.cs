namespace C2.Forms
{
    partial class CanvasForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasForm));
            this.canvasPanel = new C2.Controls.CanvasPanel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.leftFoldButton = new System.Windows.Forms.PictureBox();
            this.topToolBarControl = new C2.Controls.Top.TopToolBarControl();
            this.flowControl1 = new C2.Controls.Flow.FlowControl();
            this.naviViewControl = new C2.Controls.Flow.NaviViewControl();
            this.remarkControl = new C2.Controls.Flow.RemarkControl();
            this.runButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.bottomViewPane = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.minMaxPictureBox = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.logLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.previewLabel = new System.Windows.Forms.Label();
            this.canvasPanel.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).BeginInit();
            this.bottomViewPane.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).BeginInit();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvasPanel
            // 
            this.canvasPanel.AllowDrop = true;
            this.canvasPanel.BackColor = System.Drawing.Color.White;
            this.canvasPanel.Controls.Add(this.panel11);
            this.canvasPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.canvasPanel.DelEnable = false;
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.Document = null;
            this.canvasPanel.EndC = null;
            this.canvasPanel.EndP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.EndP")));
            this.canvasPanel.LeftButtonDown = false;
            this.canvasPanel.Location = new System.Drawing.Point(0, 33);
            this.canvasPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(1106, 475);
            this.canvasPanel.StartC = null;
            this.canvasPanel.StartP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.StartP")));
            this.canvasPanel.TabIndex = 8;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.leftFoldButton);
            this.panel11.Location = new System.Drawing.Point(0, 200);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(8, 101);
            this.panel11.TabIndex = 1;
            // 
            // leftFoldButton
            // 
            this.leftFoldButton.Image = ((System.Drawing.Image)(resources.GetObject("leftFoldButton.Image")));
            this.leftFoldButton.Location = new System.Drawing.Point(0, 0);
            this.leftFoldButton.Margin = new System.Windows.Forms.Padding(0);
            this.leftFoldButton.Name = "leftFoldButton";
            this.leftFoldButton.Size = new System.Drawing.Size(7, 100);
            this.leftFoldButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.leftFoldButton.TabIndex = 0;
            this.leftFoldButton.TabStop = false;
            // 
            // topToolBarControl1
            // 
            this.topToolBarControl.BackColor = System.Drawing.Color.GhostWhite;
            this.topToolBarControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.topToolBarControl.Location = new System.Drawing.Point(0, 0);
            this.topToolBarControl.Name = "topToolBarControl1";
            this.topToolBarControl.Size = new System.Drawing.Size(1106, 33);
            this.topToolBarControl.TabIndex = 9;
            // 
            // flowControl1
            // 
            this.flowControl1.BackColor = System.Drawing.Color.Transparent;
            this.flowControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flowControl1.BackgroundImage")));
            this.flowControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flowControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowControl1.Location = new System.Drawing.Point(798, 87);
            this.flowControl1.Name = "flowControl1";
            this.flowControl1.SelectDrag = false;
            this.flowControl1.SelectFrame = false;
            this.flowControl1.SelectRemark = false;
            this.flowControl1.Size = new System.Drawing.Size(218, 51);
            this.flowControl1.TabIndex = 10;
            // 
            // naviViewControl1
            // 
            this.naviViewControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.naviViewControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviViewControl.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.naviViewControl.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.naviViewControl.Location = new System.Drawing.Point(756, 99);
            this.naviViewControl.Name = "naviViewControl1";
            this.naviViewControl.Size = new System.Drawing.Size(233, 90);
            this.naviViewControl.TabIndex = 11;
            // 
            // remarkControl1
            // 
            this.remarkControl.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.remarkControl.BackColor = System.Drawing.Color.Transparent;
            this.remarkControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.remarkControl.Location = new System.Drawing.Point(590, 39);
            this.remarkControl.Name = "remarkControl1";
            this.remarkControl.RemarkDescription = "";
            this.remarkControl.Size = new System.Drawing.Size(160, 160);
            this.remarkControl.TabIndex = 12;
            // 
            // runButton
            // 
            this.runButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.runButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.BorderSize = 0;
            this.runButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.runButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.Location = new System.Drawing.Point(338, 88);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(52, 53);
            this.runButton.TabIndex = 21;
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(420, 88);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(52, 53);
            this.stopButton.TabIndex = 22;
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.resetButton.Image = global::C2.Properties.Resources.reset;
            this.resetButton.Location = new System.Drawing.Point(505, 88);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(52, 53);
            this.resetButton.TabIndex = 23;
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // bottomViewPanel
            // 
            this.bottomViewPane.Controls.Add(this.panel4);
            this.bottomViewPane.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomViewPane.Location = new System.Drawing.Point(0, 228);
            this.bottomViewPane.Name = "bottomViewPanel";
            this.bottomViewPane.Size = new System.Drawing.Size(1106, 280);
            this.bottomViewPane.TabIndex = 24;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1106, 39);
            this.panel4.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.minMaxPictureBox);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(958, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(148, 39);
            this.panel9.TabIndex = 2;
            // 
            // minMaxPictureBox
            // 
            this.minMaxPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("minMaxPictureBox.Image")));
            this.minMaxPictureBox.Location = new System.Drawing.Point(115, 12);
            this.minMaxPictureBox.Name = "minMaxPictureBox";
            this.minMaxPictureBox.Size = new System.Drawing.Size(25, 24);
            this.minMaxPictureBox.TabIndex = 1;
            this.minMaxPictureBox.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.logLabel);
            this.panel8.Controls.Add(this.errorLabel);
            this.panel8.Controls.Add(this.previewLabel);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(394, 39);
            this.panel8.TabIndex = 0;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.logLabel.Location = new System.Drawing.Point(120, 4);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(92, 27);
            this.logLabel.TabIndex = 3;
            this.logLabel.Text = "运行日志";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.errorLabel.Location = new System.Drawing.Point(226, 4);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(92, 27);
            this.errorLabel.TabIndex = 2;
            this.errorLabel.Text = "报错信息";
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.previewLabel.Location = new System.Drawing.Point(14, 4);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(92, 27);
            this.previewLabel.TabIndex = 0;
            this.previewLabel.Text = "数据预览";
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 508);
            this.Controls.Add(this.bottomViewPane);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.remarkControl);
            this.Controls.Add(this.naviViewControl);
            this.Controls.Add(this.flowControl1);
            this.Controls.Add(this.canvasPanel);
            this.canvasPanel.Controls.Add(this.topToolBarControl);
            this.Name = "CanvasForm";
            this.Text = "CanvasForm";
            this.canvasPanel.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).EndInit();
            this.bottomViewPane.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CanvasPanel canvasPanel;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.PictureBox leftFoldButton;
        private Controls.Top.TopToolBarControl topToolBarControl;
        private Controls.Flow.FlowControl flowControl1;
        private Controls.Flow.NaviViewControl naviViewControl;
        private Controls.Flow.RemarkControl remarkControl;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel bottomViewPane;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.PictureBox minMaxPictureBox;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label previewLabel;
    }
}