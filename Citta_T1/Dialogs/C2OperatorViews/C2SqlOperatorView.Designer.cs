using C2.Core;

namespace C2.Dialogs.C2OperatorViews
{
    partial class C2SqlOperatorView
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
            this.bnExecute = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControlEx();
            this.gridOutput = new System.Windows.Forms.DataGridView();
            this.comboBoxConnection = new System.Windows.Forms.ComboBox();
            this.comboBoxDataBase = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bnHelp = new System.Windows.Forms.Button();
            this.bnConnect = new System.Windows.Forms.Button();
            this.bnView = new System.Windows.Forms.Button();
            this.tableListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.allRadioButton = new System.Windows.Forms.RadioButton();
            this.partialRadioButton = new System.Windows.Forms.RadioButton();
            this.maxNumTextBox = new System.Windows.Forms.TextBox();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(791, 7);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(692, 7);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 441);
            this.bottomPanel.Size = new System.Drawing.Size(864, 40);
            // 
            // bnExecute
            // 
            this.bnExecute.Image = global::C2.Properties.Resources.control_play_blue;
            this.bnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnExecute.Location = new System.Drawing.Point(215, 9);
            this.bnExecute.Name = "bnExecute";
            this.bnExecute.Size = new System.Drawing.Size(80, 22);
            this.bnExecute.TabIndex = 4;
            this.bnExecute.Text = "执行SQL";
            this.bnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.bnExecute, "执行SQL的结果仅可预览前一千行数据");
            this.bnExecute.UseVisualStyleBackColor = true;
            this.bnExecute.Click += new System.EventHandler(this.bnExecute_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(215, 37);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textEditorControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridOutput);
            this.splitContainer1.Size = new System.Drawing.Size(642, 399);
            this.splitContainer1.SplitterDistance = 143;
            this.splitContainer1.TabIndex = 9;
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl1.FoldingStrategy = "XML";
            this.textEditorControl1.Font = new System.Drawing.Font("Courier New", 10F);
            this.textEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.Size = new System.Drawing.Size(642, 143);
            this.textEditorControl1.SyntaxHighlighting = "SQL";
            this.textEditorControl1.TabIndex = 9;
            // 
            // gridOutput
            // 
            this.gridOutput.AllowUserToAddRows = false;
            this.gridOutput.AllowUserToDeleteRows = false;
            this.gridOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOutput.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridOutput.Location = new System.Drawing.Point(0, 0);
            this.gridOutput.Name = "gridOutput";
            this.gridOutput.RowHeadersWidth = 56;
            this.gridOutput.Size = new System.Drawing.Size(642, 252);
            this.gridOutput.TabIndex = 7;
            // 
            // comboBoxConnection
            // 
            this.comboBoxConnection.FormattingEnabled = true;
            this.comboBoxConnection.Location = new System.Drawing.Point(6, 38);
            this.comboBoxConnection.Name = "comboBoxConnection";
            this.comboBoxConnection.Size = new System.Drawing.Size(133, 23);
            this.comboBoxConnection.TabIndex = 10;
            this.comboBoxConnection.SelectedIndexChanged += new System.EventHandler(this.ComboBoxConnection_SelectedIndexChanged);
            // 
            // comboBoxDataBase
            // 
            this.comboBoxDataBase.Font = new System.Drawing.Font("宋体", 10F);
            this.comboBoxDataBase.FormattingEnabled = true;
            this.comboBoxDataBase.Location = new System.Drawing.Point(6, 94);
            this.comboBoxDataBase.Name = "comboBoxDataBase";
            this.comboBoxDataBase.Size = new System.Drawing.Size(133, 25);
            this.comboBoxDataBase.TabIndex = 11;
            this.comboBoxDataBase.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDataBase_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "已配连接:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "架构:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(6, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "表预览:";
            // 
            // bnHelp
            // 
            this.bnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnHelp.Image = global::C2.Properties.Resources.sql_help;
            this.bnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnHelp.Location = new System.Drawing.Point(833, 9);
            this.bnHelp.Name = "bnHelp";
            this.bnHelp.Size = new System.Drawing.Size(24, 22);
            this.bnHelp.TabIndex = 7;
            this.bnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnHelp.UseVisualStyleBackColor = true;
            // 
            // bnConnect
            // 
            this.bnConnect.Image = global::C2.Properties.Resources.db_connect;
            this.bnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnConnect.Location = new System.Drawing.Point(145, 37);
            this.bnConnect.Name = "bnConnect";
            this.bnConnect.Size = new System.Drawing.Size(65, 23);
            this.bnConnect.TabIndex = 17;
            this.bnConnect.Text = "连接库";
            this.bnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnConnect.UseVisualStyleBackColor = true;
            this.bnConnect.Click += new System.EventHandler(this.BnConnect_Click);
            // 
            // bnView
            // 
            this.bnView.Image = global::C2.Properties.Resources.table_view;
            this.bnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnView.Location = new System.Drawing.Point(145, 94);
            this.bnView.Name = "bnView";
            this.bnView.Size = new System.Drawing.Size(65, 23);
            this.bnView.TabIndex = 18;
            this.bnView.Text = "预览表";
            this.bnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.bnView, "仅预览当前用户下的所有数据表");
            this.bnView.UseVisualStyleBackColor = true;
            this.bnView.Click += new System.EventHandler(this.BnView_Click);
            // 
            // tableListBox
            // 
            this.tableListBox.FormattingEnabled = true;
            this.tableListBox.ItemHeight = 15;
            this.tableListBox.Location = new System.Drawing.Point(6, 156);
            this.tableListBox.Name = "tableListBox";
            this.tableListBox.Size = new System.Drawing.Size(203, 259);
            this.tableListBox.TabIndex = 19;
            this.tableListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TableListBox_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(247, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "（尽量使用能减少结果条数的命令）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(554, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "数据提取";
            // 
            // allRadioButton
            // 
            this.allRadioButton.AutoSize = true;
            this.allRadioButton.Checked = true;
            this.allRadioButton.Location = new System.Drawing.Point(613, 12);
            this.allRadioButton.Name = "allRadioButton";
            this.allRadioButton.Size = new System.Drawing.Size(58, 19);
            this.allRadioButton.TabIndex = 22;
            this.allRadioButton.TabStop = true;
            this.allRadioButton.Text = "所有";
            this.allRadioButton.UseVisualStyleBackColor = true;
            this.allRadioButton.CheckedChanged += new System.EventHandler(this.allRadioButton_CheckedChanged);
            // 
            // partialRadioButton
            // 
            this.partialRadioButton.AutoSize = true;
            this.partialRadioButton.Location = new System.Drawing.Point(666, 12);
            this.partialRadioButton.Name = "partialRadioButton";
            this.partialRadioButton.Size = new System.Drawing.Size(58, 19);
            this.partialRadioButton.TabIndex = 23;
            this.partialRadioButton.Text = "部分";
            this.partialRadioButton.UseVisualStyleBackColor = true;
            this.partialRadioButton.Click += new System.EventHandler(this.partialRadioButton_Click);
            // 
            // maxNumTextBox
            // 
            this.maxNumTextBox.Location = new System.Drawing.Point(718, 10);
            this.maxNumTextBox.Name = "maxNumTextBox";
            this.maxNumTextBox.Size = new System.Drawing.Size(76, 25);
            this.maxNumTextBox.TabIndex = 24;
            this.maxNumTextBox.Visible = false;
            // 
            // C2SqlOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(864, 481);
            this.ControlBox = true;
            this.Controls.Add(this.maxNumTextBox);
            this.Controls.Add(this.partialRadioButton);
            this.Controls.Add(this.allRadioButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tableListBox);
            this.Controls.Add(this.bnView);
            this.Controls.Add(this.bnConnect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDataBase);
            this.Controls.Add(this.comboBoxConnection);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bnHelp);
            this.Controls.Add(this.bnExecute);
            this.Controls.Add(this.bottomPanel);
            this.Icon = global::C2.Properties.Resources.sql_icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "C2SqlOperatorView";
            this.ShowIcon = true;
            this.Text = "自定义Sql算子设置";
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.bnExecute, 0);
            this.Controls.SetChildIndex(this.bnHelp, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.comboBoxConnection, 0);
            this.Controls.SetChildIndex(this.comboBoxDataBase, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.bnConnect, 0);
            this.Controls.SetChildIndex(this.bnView, 0);
            this.Controls.SetChildIndex(this.tableListBox, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.allRadioButton, 0);
            this.Controls.SetChildIndex(this.partialRadioButton, 0);
            this.Controls.SetChildIndex(this.maxNumTextBox, 0);
            this.bottomPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bnExecute;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ICSharpCode.TextEditor.TextEditorControlEx textEditorControl1;
        private System.Windows.Forms.DataGridView gridOutput;
        private System.Windows.Forms.ComboBox comboBoxConnection;
        private System.Windows.Forms.ComboBox comboBoxDataBase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bnHelp;
        private System.Windows.Forms.Button bnConnect;
        private System.Windows.Forms.Button bnView;
        private System.Windows.Forms.ListBox tableListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton allRadioButton;
        private System.Windows.Forms.RadioButton partialRadioButton;
        private System.Windows.Forms.TextBox maxNumTextBox;
    }
}