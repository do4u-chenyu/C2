namespace C2.Dialogs.Base
{
    partial class C1BaseOperatorView
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
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB1
            // 
            this.dataSourceTB1.MouseHover += new System.EventHandler(this.DataSourceTB1_MouseHover);
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.MouseHover += new System.EventHandler(this.DataSourceTB0_MouseHover);
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // comboBox0
            // 
            this.comboBox0.SelectionChangeCommitted += new System.EventHandler(this.GetLeftSelectedItemIndex);
            this.comboBox0.TextUpdate += new System.EventHandler(this.LeftComboBox_TextUpdate);
            this.comboBox0.DropDownClosed += new System.EventHandler(this.LeftComboBox_ClosedEvent);
            // 
            // comboBox1
            // 
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.GetRightSelectedItemIndex);
            this.comboBox1.TextUpdate += new System.EventHandler(this.RightComboBox_TextUpdate);
            this.comboBox1.DropDownClosed += new System.EventHandler(this.RightComboBox_ClosedEvent);
            // 
            // C1BaseOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "C1BaseOperatorView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.Text = "C1BaseOperatorView";
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}