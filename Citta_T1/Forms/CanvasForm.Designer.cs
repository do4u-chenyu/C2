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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasForm));
            this.canvasPanel = new C2.Controls.CanvasPanel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.leftFoldButton = new System.Windows.Forms.PictureBox();
            this.bottomViewPanel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.minMaxPictureBox = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.logLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.previewLabel = new System.Windows.Forms.Label();
            this.bottomLogControl = new C2.Controls.Bottom.BottomLogControl();
            this.bottomPyConsole = new C2.Controls.Bottom.BottomConsoleControl();
            this.bottomPreview = new C2.Controls.Bottom.BottomPreviewControl();
            this.topToolBarControl = new C2.Controls.Top.TopToolBarControl();
            this.currentModelRunBackLab = new System.Windows.Forms.Label();
            this.currentModelFinLab = new System.Windows.Forms.Label();
            this.currentModelRunLab = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.resetButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.rightShowButton = new C2.Controls.Flow.RightShowButton();
            this.rightHideButton = new C2.Controls.Flow.RightHideButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.remarkControl = new C2.Controls.Flow.RemarkControl();
            this.flowControl = new C2.Controls.Flow.FlowControl();
            this.leftToolBoxPanel = new System.Windows.Forms.Panel();
            this.canvasPanel.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).BeginInit();
            this.bottomViewPanel.SuspendLayout();
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
            this.canvasPanel.Size = new System.Drawing.Size(1106, 195);
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
            // bottomViewPanel
            // 
            this.bottomViewPanel.Controls.Add(this.panel4);
            this.bottomViewPanel.Controls.Add(this.bottomLogControl);
            this.bottomViewPanel.Controls.Add(this.bottomPyConsole);
            this.bottomViewPanel.Controls.Add(this.bottomPreview);
            this.bottomViewPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomViewPanel.Location = new System.Drawing.Point(0, 228);
            this.bottomViewPanel.Name = "bottomViewPanel";
            this.bottomViewPanel.Size = new System.Drawing.Size(1106, 280);
            this.bottomViewPanel.TabIndex = 24;
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
            // bottomLogControl
            // 
            this.bottomLogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLogControl.Location = new System.Drawing.Point(0, 0);
            this.bottomLogControl.Name = "bottomLogControl";
            this.bottomLogControl.Size = new System.Drawing.Size(1106, 280);
            this.bottomLogControl.TabIndex = 3;
            // 
            // bottomPyConsole
            // 
            this.bottomPyConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPyConsole.Location = new System.Drawing.Point(0, 0);
            this.bottomPyConsole.Name = "bottomPyConsole";
            this.bottomPyConsole.Size = new System.Drawing.Size(1106, 280);
            this.bottomPyConsole.TabIndex = 2;
            // 
            // bottomPreview
            // 
            this.bottomPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPreview.Location = new System.Drawing.Point(0, 0);
            this.bottomPreview.Name = "bottomPreview";
            this.bottomPreview.Size = new System.Drawing.Size(1106, 280);
            this.bottomPreview.TabIndex = 1;
            // 
            // topToolBarControl
            // 
            this.topToolBarControl.BackColor = System.Drawing.Color.GhostWhite;
            this.topToolBarControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.topToolBarControl.Location = new System.Drawing.Point(0, 0);
            this.topToolBarControl.Name = "topToolBarControl";
            this.topToolBarControl.Size = new System.Drawing.Size(1106, 33);
            this.topToolBarControl.TabIndex = 25;
            // 
            // currentModelRunBackLab
            // 
            this.currentModelRunBackLab.Image = global::C2.Properties.Resources.currentModelRunningBack;
            this.currentModelRunBackLab.Location = new System.Drawing.Point(300, 68);
            this.currentModelRunBackLab.Name = "currentModelRunBackLab";
            this.currentModelRunBackLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelRunBackLab.TabIndex = 26;
            this.currentModelRunBackLab.Visible = false;
            // 
            // currentModelFinLab
            // 
            this.currentModelFinLab.Image = global::C2.Properties.Resources.currentModelFin;
            this.currentModelFinLab.Location = new System.Drawing.Point(612, 68);
            this.currentModelFinLab.Name = "currentModelFinLab";
            this.currentModelFinLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelFinLab.TabIndex = 27;
            this.currentModelFinLab.Visible = false;
            // 
            // currentModelRunLab
            // 
            this.currentModelRunLab.Image = global::C2.Properties.Resources.currentModelRunning;
            this.currentModelRunLab.Location = new System.Drawing.Point(456, 68);
            this.currentModelRunLab.Name = "currentModelRunLab";
            this.currentModelRunLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelRunLab.TabIndex = 28;
            this.currentModelRunLab.Visible = false;
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.White;
            this.resetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.resetButton.Image = global::C2.Properties.Resources.reset;
            this.resetButton.Location = new System.Drawing.Point(223, 133);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(52, 53);
            this.resetButton.TabIndex = 32;
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.White;
            this.stopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(138, 133);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(52, 53);
            this.stopButton.TabIndex = 31;
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // runButton
            // 
            this.runButton.BackColor = System.Drawing.Color.White;
            this.runButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.runButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.BorderSize = 0;
            this.runButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.runButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.Location = new System.Drawing.Point(56, 133);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(52, 53);
            this.runButton.TabIndex = 30;
            this.runButton.UseVisualStyleBackColor = false;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // rightShowButton
            // 
            this.rightShowButton.BackColor = System.Drawing.Color.Transparent;
            this.rightShowButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightShowButton.BackgroundImage")));
            this.rightShowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightShowButton.Location = new System.Drawing.Point(122, 39);
            this.rightShowButton.Name = "rightShowButton";
            this.rightShowButton.Size = new System.Drawing.Size(55, 55);
            this.rightShowButton.TabIndex = 34;
            // 
            // rightHideButton
            // 
            this.rightHideButton.BackColor = System.Drawing.Color.Transparent;
            this.rightHideButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightHideButton.BackgroundImage")));
            this.rightHideButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightHideButton.Location = new System.Drawing.Point(183, 39);
            this.rightHideButton.Name = "rightHideButton";
            this.rightHideButton.Size = new System.Drawing.Size(55, 55);
            this.rightHideButton.TabIndex = 33;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(806, 107);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(125, 10);
            this.progressBar1.TabIndex = 35;
            this.progressBar1.Visible = false;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressBarLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.progressBarLabel.ForeColor = System.Drawing.Color.Black;
            this.progressBarLabel.Location = new System.Drawing.Point(873, 133);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(0, 16);
            this.progressBarLabel.TabIndex = 36;
            this.progressBarLabel.Visible = false;
            // 
            // remarkControl
            // 
            this.remarkControl.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.remarkControl.BackColor = System.Drawing.Color.Transparent;
            this.remarkControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.remarkControl.Location = new System.Drawing.Point(918, 33);
            this.remarkControl.Name = "remarkControl";
            this.remarkControl.RemarkDescription = "";
            this.remarkControl.Size = new System.Drawing.Size(160, 160);
            this.remarkControl.TabIndex = 38;
            // 
            // flowControl
            // 
            this.flowControl.BackColor = System.Drawing.Color.Transparent;
            this.flowControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flowControl.BackgroundImage")));
            this.flowControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flowControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowControl.Location = new System.Drawing.Point(841, 33);
            this.flowControl.Name = "flowControl";
            this.flowControl.SelectDrag = false;
            this.flowControl.SelectFrame = false;
            this.flowControl.SelectRemark = false;
            this.flowControl.Size = new System.Drawing.Size(218, 51);
            this.flowControl.TabIndex = 37;
            // 
            // leftToolBoxPanel
            // 
            this.leftToolBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftToolBoxPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftToolBoxPanel.Location = new System.Drawing.Point(0, 33);
            this.leftToolBoxPanel.Name = "leftToolBoxPanel";
            this.leftToolBoxPanel.Size = new System.Drawing.Size(10, 195);
            this.leftToolBoxPanel.TabIndex = 39;
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 508);
            this.Controls.Add(this.leftToolBoxPanel);
            this.Controls.Add(this.remarkControl);
            this.Controls.Add(this.flowControl);
            this.Controls.Add(this.progressBarLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.rightShowButton);
            this.Controls.Add(this.rightHideButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.currentModelRunLab);
            this.Controls.Add(this.currentModelFinLab);
            this.Controls.Add(this.currentModelRunBackLab);
            this.Controls.Add(this.canvasPanel);
            this.Controls.Add(this.topToolBarControl);
            this.Controls.Add(this.bottomViewPanel);
            this.Name = "CanvasForm";
            this.Text = "CanvasForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CanvasForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CanvasForm_FormClosed);
            this.SizeChanged += new System.EventHandler(this.CanvasForm_SizeChanged);
            this.canvasPanel.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).EndInit();
            this.bottomViewPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CanvasPanel canvasPanel;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.PictureBox leftFoldButton;
        private System.Windows.Forms.Panel bottomViewPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.PictureBox minMaxPictureBox;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label previewLabel;
        private Controls.Top.TopToolBarControl topToolBarControl;
        private Controls.Bottom.BottomLogControl bottomLogControl;
        private Controls.Bottom.BottomConsoleControl bottomPyConsole;
        private Controls.Bottom.BottomPreviewControl bottomPreview;
        private System.Windows.Forms.Label currentModelRunBackLab;
        private System.Windows.Forms.Label currentModelFinLab;
        private System.Windows.Forms.Label currentModelRunLab;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button runButton;
        private Controls.Flow.RightShowButton rightShowButton;
        private Controls.Flow.RightHideButton rightHideButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressBarLabel;
        private Controls.Flow.RemarkControl remarkControl;
        private Controls.Flow.FlowControl flowControl;
        private System.Windows.Forms.Panel leftToolBoxPanel;
    }
}