
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
            this.treeConnections = new System.Windows.Forms.TreeView();
            this.bnHelp = new System.Windows.Forms.Button();
            this.bnTest = new System.Windows.Forms.Button();
            this.bnDelete = new System.Windows.Forms.Button();
            this.bnNew = new System.Windows.Forms.Button();
            this.bnExecute = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControlEx();
            this.gridOutput = new System.Windows.Forms.DataGridView();
            this.comboBoxConnection = new System.Windows.Forms.ComboBox();
            this.comboBoxDataBase = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.cancelButton.Location = new System.Drawing.Point(689, 7);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(590, 7);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(763, 0);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 441);
            this.bottomPanel.Size = new System.Drawing.Size(763, 40);
            // 
            // treeConnections
            // 
            this.treeConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeConnections.Location = new System.Drawing.Point(8, 164);
            this.treeConnections.Name = "treeConnections";
            this.treeConnections.Size = new System.Drawing.Size(232, 272);
            this.treeConnections.TabIndex = 3;
            // 
            // bnHelp
            // 
            this.bnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnHelp.Location = new System.Drawing.Point(714, 10);
            this.bnHelp.Name = "bnHelp";
            this.bnHelp.Size = new System.Drawing.Size(42, 22);
            this.bnHelp.TabIndex = 7;
            this.bnHelp.Text = "帮助";
            this.bnHelp.UseVisualStyleBackColor = true;
            // 
            // bnTest
            // 
            this.bnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnTest.Location = new System.Drawing.Point(144, 124);
            this.bnTest.Name = "bnTest";
            this.bnTest.Size = new System.Drawing.Size(48, 22);
            this.bnTest.TabIndex = 2;
            this.bnTest.Text = "测试";
            this.bnTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnTest.UseVisualStyleBackColor = true;
            // 
            // bnDelete
            // 
            this.bnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnDelete.Location = new System.Drawing.Point(72, 124);
            this.bnDelete.Name = "bnDelete";
            this.bnDelete.Size = new System.Drawing.Size(64, 22);
            this.bnDelete.TabIndex = 1;
            this.bnDelete.Text = "Delete";
            this.bnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnDelete.UseVisualStyleBackColor = true;
            // 
            // bnNew
            // 
            this.bnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnNew.Location = new System.Drawing.Point(8, 124);
            this.bnNew.Name = "bnNew";
            this.bnNew.Size = new System.Drawing.Size(56, 22);
            this.bnNew.TabIndex = 0;
            this.bnNew.Text = "New";
            this.bnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnNew.UseVisualStyleBackColor = true;
            // 
            // bnExecute
            // 
            this.bnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnExecute.Location = new System.Drawing.Point(248, 10);
            this.bnExecute.Name = "bnExecute";
            this.bnExecute.Size = new System.Drawing.Size(96, 22);
            this.bnExecute.TabIndex = 4;
            this.bnExecute.Text = "执行SQL";
            this.bnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnExecute.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(248, 37);
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
            this.splitContainer1.Size = new System.Drawing.Size(508, 399);
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
            this.textEditorControl1.Size = new System.Drawing.Size(508, 143);
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
            this.gridOutput.Size = new System.Drawing.Size(508, 252);
            this.gridOutput.TabIndex = 7;
            // 
            // comboBoxConnection
            // 
            this.comboBoxConnection.FormattingEnabled = true;
            this.comboBoxConnection.Location = new System.Drawing.Point(6, 31);
            this.comboBoxConnection.Name = "comboBoxConnection";
            this.comboBoxConnection.Size = new System.Drawing.Size(184, 20);
            this.comboBoxConnection.TabIndex = 10;
            // 
            // comboBoxDataBase
            // 
            this.comboBoxDataBase.FormattingEnabled = true;
            this.comboBoxDataBase.Location = new System.Drawing.Point(6, 83);
            this.comboBoxDataBase.Name = "comboBoxDataBase";
            this.comboBoxDataBase.Size = new System.Drawing.Size(184, 20);
            this.comboBoxDataBase.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "当前连接:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "架构:";
            // 
            // C2SqlOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 481);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDataBase);
            this.Controls.Add(this.comboBoxConnection);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bnHelp);
            this.Controls.Add(this.bnTest);
            this.Controls.Add(this.bnDelete);
            this.Controls.Add(this.bnNew);
            this.Controls.Add(this.bnExecute);
            this.Controls.Add(this.treeConnections);
            this.Controls.Add(this.bottomPanel);
            this.Name = "C2SqlOperatorView";
            this.Text = "Sql算子设置";
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.treeConnections, 0);
            this.Controls.SetChildIndex(this.bnExecute, 0);
            this.Controls.SetChildIndex(this.bnNew, 0);
            this.Controls.SetChildIndex(this.bnDelete, 0);
            this.Controls.SetChildIndex(this.bnTest, 0);
            this.Controls.SetChildIndex(this.bnHelp, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.comboBoxConnection, 0);
            this.Controls.SetChildIndex(this.comboBoxDataBase, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
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

        private System.Windows.Forms.TreeView treeConnections;
        private System.Windows.Forms.Button bnExecute;
        private System.Windows.Forms.Button bnNew;
        private System.Windows.Forms.Button bnDelete;
        private System.Windows.Forms.Button bnTest;
        private System.Windows.Forms.Button bnHelp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ICSharpCode.TextEditor.TextEditorControlEx textEditorControl1;
        private System.Windows.Forms.DataGridView gridOutput;
        private System.Windows.Forms.ComboBox comboBoxConnection;
        private System.Windows.Forms.ComboBox comboBoxDataBase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}