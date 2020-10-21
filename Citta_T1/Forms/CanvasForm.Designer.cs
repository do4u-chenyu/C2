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
            this.topToolBarControl = new C2.Controls.Top.TopToolBarControl();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.currentModelFinLab = new System.Windows.Forms.Label();
            this.currentModelRunBackLab = new System.Windows.Forms.Label();
            this.currentModelRunLab = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.leftFoldButton = new System.Windows.Forms.PictureBox();
            this.remarkControl = new C2.Controls.Flow.RemarkControl();
            this.flowControl = new C2.Controls.Flow.FlowControl();
            this.rightHideButton = new C2.Controls.Flow.RightHideButton();
            this.rightShowButton = new C2.Controls.Flow.RightShowButton();
            this.resetButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.naviViewControl = new C2.Controls.Flow.NaviViewControl();
            this.canvasPanel.SuspendLayout();
            this.currentModelRunBackLab.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).BeginInit();
            this.SuspendLayout();
            // 
            // canvasPanel
            // 
            this.canvasPanel.AllowDrop = true;
            this.canvasPanel.BackColor = System.Drawing.Color.White;
            this.canvasPanel.Controls.Add(this.topToolBarControl);
            this.canvasPanel.Controls.Add(this.progressBarLabel);
            this.canvasPanel.Controls.Add(this.progressBar1);
            this.canvasPanel.Controls.Add(this.currentModelFinLab);
            this.canvasPanel.Controls.Add(this.currentModelRunBackLab);
            this.canvasPanel.Controls.Add(this.panel11);
            this.canvasPanel.Controls.Add(this.remarkControl);
            this.canvasPanel.Controls.Add(this.flowControl);
            this.canvasPanel.Controls.Add(this.rightHideButton);
            this.canvasPanel.Controls.Add(this.rightShowButton);
            this.canvasPanel.Controls.Add(this.resetButton);
            this.canvasPanel.Controls.Add(this.stopButton);
            this.canvasPanel.Controls.Add(this.runButton);
            this.canvasPanel.Controls.Add(this.naviViewControl);
            this.canvasPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.canvasPanel.DelEnable = false;
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.EndC = null;
            this.canvasPanel.EndP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.EndP")));
            this.canvasPanel.LeftButtonDown = false;
            this.canvasPanel.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(1106, 508);
            this.canvasPanel.StartC = null;
            this.canvasPanel.StartP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.StartP")));
            this.canvasPanel.TabIndex = 8;
            // 
            // topToolBarControl
            // 
            this.topToolBarControl.BackColor = System.Drawing.Color.GhostWhite;
            this.topToolBarControl.Location = new System.Drawing.Point(27, 0);
            this.topToolBarControl.Name = "topToolBarControl";
            this.topToolBarControl.Size = new System.Drawing.Size(1275, 32);
            this.topToolBarControl.TabIndex = 24;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressBarLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progressBarLabel.ForeColor = System.Drawing.Color.Black;
            this.progressBarLabel.Location = new System.Drawing.Point(953, 245);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(24, 16);
            this.progressBarLabel.TabIndex = 32;
            this.progressBarLabel.Text = "0%";
            this.progressBarLabel.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.progressBar1.ForeColor = System.Drawing.Color.Transparent;
            this.progressBar1.Location = new System.Drawing.Point(823, 245);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(125, 10);
            this.progressBar1.Step = 30;
            this.progressBar1.TabIndex = 31;
            this.progressBar1.Visible = false;
            // 
            // currentModelFinLab
            // 
            this.currentModelFinLab.Image = global::C2.Properties.Resources.currentModelFin;
            this.currentModelFinLab.Location = new System.Drawing.Point(498, 174);
            this.currentModelFinLab.Name = "currentModelFinLab";
            this.currentModelFinLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelFinLab.TabIndex = 30;
            this.currentModelFinLab.Visible = false;
            // 
            // currentModelRunBackLab
            // 
            this.currentModelRunBackLab.Controls.Add(this.currentModelRunLab);
            this.currentModelRunBackLab.Image = global::C2.Properties.Resources.currentModelRunningBack;
            this.currentModelRunBackLab.Location = new System.Drawing.Point(498, 174);
            this.currentModelRunBackLab.Name = "currentModelRunBackLab";
            this.currentModelRunBackLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelRunBackLab.TabIndex = 29;
            this.currentModelRunBackLab.Visible = false;
            // 
            // currentModelRunLab
            // 
            this.currentModelRunLab.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.currentModelRunLab.Image = global::C2.Properties.Resources.currentModelRunning;
            this.currentModelRunLab.Location = new System.Drawing.Point(40, 20);
            this.currentModelRunLab.Name = "currentModelRunLab";
            this.currentModelRunLab.Size = new System.Drawing.Size(73, 47);
            this.currentModelRunLab.TabIndex = 28;
            this.currentModelRunLab.Visible = false;
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
            // remarkControl
            // 
            this.remarkControl.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.remarkControl.BackColor = System.Drawing.Color.Transparent;
            this.remarkControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.remarkControl.Location = new System.Drawing.Point(694, 111);
            this.remarkControl.Margin = new System.Windows.Forms.Padding(4);
            this.remarkControl.Name = "remarkControl";
            this.remarkControl.RemarkDescription = "";
            this.remarkControl.Size = new System.Drawing.Size(160, 160);
            this.remarkControl.TabIndex = 26;
            this.remarkControl.Visible = false;
            // 
            // flowControl
            // 
            this.flowControl.BackColor = System.Drawing.Color.Transparent;
            this.flowControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flowControl.BackgroundImage")));
            this.flowControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flowControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowControl.Location = new System.Drawing.Point(687, 50);
            this.flowControl.Margin = new System.Windows.Forms.Padding(4);
            this.flowControl.Name = "flowControl";
            this.flowControl.SelectDrag = false;
            this.flowControl.SelectFrame = false;
            this.flowControl.SelectRemark = false;
            this.flowControl.Size = new System.Drawing.Size(220, 51);
            this.flowControl.TabIndex = 25;
            // 
            // rightHideButton
            // 
            this.rightHideButton.BackColor = System.Drawing.Color.Transparent;
            this.rightHideButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightHideButton.BackgroundImage")));
            this.rightHideButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightHideButton.Location = new System.Drawing.Point(909, 111);
            this.rightHideButton.Margin = new System.Windows.Forms.Padding(4);
            this.rightHideButton.Name = "rightHideButton";
            this.rightHideButton.Size = new System.Drawing.Size(55, 55);
            this.rightHideButton.TabIndex = 23;
            // 
            // rightShowButton
            // 
            this.rightShowButton.BackColor = System.Drawing.Color.Transparent;
            this.rightShowButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightShowButton.BackgroundImage")));
            this.rightShowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightShowButton.Location = new System.Drawing.Point(909, 50);
            this.rightShowButton.Margin = new System.Windows.Forms.Padding(4);
            this.rightShowButton.Name = "rightShowButton";
            this.rightShowButton.Size = new System.Drawing.Size(55, 55);
            this.rightShowButton.TabIndex = 22;
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
            this.resetButton.Location = new System.Drawing.Point(507, 354);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(52, 53);
            this.resetButton.TabIndex = 21;
            this.resetButton.UseVisualStyleBackColor = true;
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
            this.stopButton.Location = new System.Drawing.Point(450, 354);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(52, 53);
            this.stopButton.TabIndex = 20;
            this.stopButton.UseVisualStyleBackColor = true;
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
            this.runButton.Location = new System.Drawing.Point(398, 354);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(52, 53);
            this.runButton.TabIndex = 20;
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // naviViewControl
            // 
            this.naviViewControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.naviViewControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviViewControl.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.naviViewControl.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.naviViewControl.Location = new System.Drawing.Point(752, 190);
            this.naviViewControl.Margin = new System.Windows.Forms.Padding(4);
            this.naviViewControl.Name = "naviViewControl";
            this.naviViewControl.Size = new System.Drawing.Size(205, 105);
            this.naviViewControl.TabIndex = 0;
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 508);
            this.Controls.Add(this.canvasPanel);
            this.Name = "CanvasForm";
            this.Text = "CanvasForm";
            this.canvasPanel.ResumeLayout(false);
            this.canvasPanel.PerformLayout();
            this.currentModelRunBackLab.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CanvasPanel canvasPanel;
        private Controls.Top.TopToolBarControl topToolBarControl;
        private System.Windows.Forms.Label progressBarLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label currentModelFinLab;
        private System.Windows.Forms.Label currentModelRunBackLab;
        private System.Windows.Forms.Label currentModelRunLab;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.PictureBox leftFoldButton;
        private Controls.Flow.RemarkControl remarkControl;
        private Controls.Flow.FlowControl flowControl;
        private Controls.Flow.RightHideButton rightHideButton;
        private Controls.Flow.RightShowButton rightShowButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button runButton;
        private Controls.Flow.NaviViewControl naviViewControl;
    }
}