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
            this.lblConn = new System.Windows.Forms.Label();
            this.bnHelp = new System.Windows.Forms.Button();
            this.bnEdit = new System.Windows.Forms.Button();
            this.bnDelete = new System.Windows.Forms.Button();
            this.bnNew = new System.Windows.Forms.Button();
            this.bnExecute = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textEditorControl1 = new System.Windows.Forms.TextBox();
            this.gridOutput = new System.Windows.Forms.DataGridView();
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
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 385);
            this.bottomPanel.Size = new System.Drawing.Size(763, 40);
            // 
            // treeConnections
            // 
            this.treeConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeConnections.Location = new System.Drawing.Point(8, 37);
            this.treeConnections.Name = "treeConnections";
            this.treeConnections.Size = new System.Drawing.Size(232, 343);
            this.treeConnections.TabIndex = 3;
            // 
            // lblConn
            // 
            this.lblConn.AutoSize = true;
            this.lblConn.Location = new System.Drawing.Point(352, 15);
            this.lblConn.Name = "lblConn";
            this.lblConn.Size = new System.Drawing.Size(71, 12);
            this.lblConn.TabIndex = 2;
            this.lblConn.Text = "Connection:";
            // 
            // bnHelp
            // 
            this.bnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnHelp.Location = new System.Drawing.Point(700, 7);
            this.bnHelp.Name = "bnHelp";
            this.bnHelp.Size = new System.Drawing.Size(56, 22);
            this.bnHelp.TabIndex = 7;
            this.bnHelp.Text = "Help";
            this.bnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnHelp.UseVisualStyleBackColor = true;
            // 
            // bnEdit
            // 
            this.bnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnEdit.Location = new System.Drawing.Point(144, 7);
            this.bnEdit.Name = "bnEdit";
            this.bnEdit.Size = new System.Drawing.Size(48, 22);
            this.bnEdit.TabIndex = 2;
            this.bnEdit.Text = "Edit";
            this.bnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bnEdit.UseVisualStyleBackColor = true;
            // 
            // bnDelete
            // 
            this.bnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bnDelete.Location = new System.Drawing.Point(72, 7);
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
            this.bnNew.Location = new System.Drawing.Point(8, 7);
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
            this.bnExecute.Location = new System.Drawing.Point(248, 7);
            this.bnExecute.Name = "bnExecute";
            this.bnExecute.Size = new System.Drawing.Size(96, 22);
            this.bnExecute.TabIndex = 4;
            this.bnExecute.Text = "Execute SQL";
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
            this.splitContainer1.Size = new System.Drawing.Size(508, 343);
            this.splitContainer1.SplitterDistance = 119;
            this.splitContainer1.TabIndex = 9;
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl1.Font = new System.Drawing.Font("Courier New", 10F);
            this.textEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.Size = new System.Drawing.Size(508, 23);
            this.textEditorControl1.TabIndex = 9;
            // 
            // gridOutput
            // 
            this.gridOutput.AllowUserToAddRows = false;
            this.gridOutput.AllowUserToDeleteRows = false;
            this.gridOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOutput.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridOutput.Location = new System.Drawing.Point(0, 0);
            this.gridOutput.Name = "gridOutput";
            this.gridOutput.Size = new System.Drawing.Size(504, 217);
            this.gridOutput.TabIndex = 7;
            // 
            // C2SqlOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 425);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bnHelp);
            this.Controls.Add(this.bnEdit);
            this.Controls.Add(this.bnDelete);
            this.Controls.Add(this.bnNew);
            this.Controls.Add(this.bnExecute);
            this.Controls.Add(this.lblConn);
            this.Controls.Add(this.treeConnections);
            this.Controls.Add(this.bottomPanel);
            this.Name = "C2SqlOperatorView";
            this.Text = "Sql算子设置";
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.treeConnections, 0);
            this.Controls.SetChildIndex(this.lblConn, 0);
            this.Controls.SetChildIndex(this.bnExecute, 0);
            this.Controls.SetChildIndex(this.bnNew, 0);
            this.Controls.SetChildIndex(this.bnDelete, 0);
            this.Controls.SetChildIndex(this.bnEdit, 0);
            this.Controls.SetChildIndex(this.bnHelp, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.bottomPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeConnections;
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.Button bnExecute;
        private System.Windows.Forms.Button bnNew;
        private System.Windows.Forms.Button bnDelete;
        private System.Windows.Forms.Button bnEdit;
        private System.Windows.Forms.Button bnHelp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textEditorControl1;
        private System.Windows.Forms.DataGridView gridOutput;
    }
}