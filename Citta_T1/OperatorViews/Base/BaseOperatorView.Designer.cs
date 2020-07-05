using Citta_T1.Core;

namespace Citta_T1.OperatorViews.Base
{
    partial class BaseOperatorView
    {

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
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.dataSourceTB0 = new System.Windows.Forms.TextBox();
            this.dataSourceTB1 = new System.Windows.Forms.TextBox();
            this.outListCCBL0 = new Citta_T1.Controls.Common.ComCheckBoxList();
            this.outListCCBL1 = new Citta_T1.Controls.Common.ComCheckBoxList();
            this.comboBox0 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Margin = new System.Windows.Forms.Padding(2);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(0, 0);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.confirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 152);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(0, 0);
            this.bottomPanel.TabIndex = 1;
            // 
            // keyPanel
            // 
            this.keyPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.keyPanel.Location = new System.Drawing.Point(0, 18);
            this.keyPanel.Margin = new System.Windows.Forms.Padding(2);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(0, 0);
            this.keyPanel.TabIndex = 2;
            // 
            // valuePanel
            // 
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(116, 18);
            this.valuePanel.Margin = new System.Windows.Forms.Padding(2);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(0, 0);
            this.valuePanel.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(0, 0);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(0, 0);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSourceTB0.Location = new System.Drawing.Point(187, 2);
            this.dataSourceTB0.Margin = new System.Windows.Forms.Padding(2);
            this.dataSourceTB0.Name = "dataSourceTB0";
            this.dataSourceTB0.ReadOnly = true;
            this.dataSourceTB0.Size = new System.Drawing.Size(144, 23);
            this.dataSourceTB0.TabIndex = 0;
            this.dataSourceTB0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataSourceTB0.MouseHover += new System.EventHandler(this.DataSourceTB0_MouseHover);
            // 
            // dataSourceTB1
            // 
            this.dataSourceTB1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSourceTB1.Location = new System.Drawing.Point(0, 0);
            this.dataSourceTB1.Margin = new System.Windows.Forms.Padding(2);
            this.dataSourceTB1.Name = "dataSourceTB1";
            this.dataSourceTB1.ReadOnly = true;
            this.dataSourceTB1.Size = new System.Drawing.Size(100, 21);
            this.dataSourceTB1.TabIndex = 1;
            this.dataSourceTB1.Visible = false;
            this.dataSourceTB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataSourceTB1.MouseHover += new System.EventHandler(this.DataSourceTB1_MouseHover);
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.DataSource = null;
            this.outListCCBL0.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outListCCBL0.Location = new System.Drawing.Point(0, 0);
            this.outListCCBL0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.outListCCBL0.Name = "outListCCBL0";
            this.outListCCBL0.Size = new System.Drawing.Size(149, 24);
            this.outListCCBL0.TabIndex = 0;
            // 
            // outListCCBL1
            // 
            this.outListCCBL1.DataSource = null;
            this.outListCCBL1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outListCCBL1.Location = new System.Drawing.Point(0, 0);
            this.outListCCBL1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.outListCCBL1.Name = "outListCCBL1";
            this.outListCCBL1.Size = new System.Drawing.Size(149, 24);
            this.outListCCBL1.TabIndex = 0;
            this.outListCCBL1.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.comboBox0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.comboBox0.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox0.FormattingEnabled = true;
            this.comboBox0.Location = new System.Drawing.Point(2, 4);
            this.comboBox0.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox0.Name = "comboBox0";
            this.comboBox0.Size = new System.Drawing.Size(86, 24);
            this.comboBox0.TabIndex = 2;
            this.comboBox0.Leave += new System.EventHandler(this.Control_Leave);
            this.comboBox0.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            this.comboBox0.SelectionChangeCommitted += new System.EventHandler(this.GetLeftSelectedItemIndex);
            this.comboBox0.TextUpdate += new System.EventHandler(LeftComboBox_TextUpdate);
            this.comboBox0.DropDownClosed += new System.EventHandler(LeftComboBox_ClosedEvent);
            // 
            // comboBox2
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(92, 4);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(86, 24);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Visible = false;
            this.comboBox1.Leave += new System.EventHandler(this.Control_Leave);
            this.comboBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.GetRightSelectedItemIndex);
            this.comboBox1.TextUpdate += new System.EventHandler(RightComboBox_TextUpdate);
            this.comboBox1.DropDownClosed += new System.EventHandler(RightComboBox_ClosedEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 0);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据信息：";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // BaseOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(457, 250);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BaseOperatorView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseOperatorView";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.TextBox dataSourceTB1; // 右表框
        protected System.Windows.Forms.TextBox dataSourceTB0; // 左表框
        protected System.Windows.Forms.Button cancelButton;   // 取消键
        protected System.Windows.Forms.Button confirmButton;  // 确认键
        protected System.Windows.Forms.ToolTip toolTip1;      // 浮动提示栏,一个就够了
        protected Citta_T1.Controls.Common.ComCheckBoxList outListCCBL0;  // 左表输出选项框
        protected Citta_T1.Controls.Common.ComCheckBoxList outListCCBL1;  // 右表输出选项框
        protected System.Windows.Forms.ComboBox comboBox0;    // 左侧第一行条件选择框
        protected System.Windows.Forms.ComboBox comboBox1;    // 右侧第一行条件选择框
        protected System.ComponentModel.IContainer components;

        protected System.Windows.Forms.Panel topPanel;        // 顶部面板，控制留白用 
        protected System.Windows.Forms.Panel bottomPanel;     // 底部面板，放置确认，取消两个按钮 
        protected System.Windows.Forms.Panel keyPanel;        // 左侧面板，放置输入框提示label
        protected System.Windows.Forms.Panel valuePanel;      // 右侧面板，放置各种选项输入框

        protected System.Windows.Forms.Label label1;          // 数据源提示label
        protected System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;  // 输入框所在的动态布局layout控件
    }
}