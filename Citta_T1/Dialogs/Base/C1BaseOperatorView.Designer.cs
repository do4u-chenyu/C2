namespace C2.Dialogs.Base
{
    partial class C1BaseOperatorView
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "C1BaseOperatorView";

            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            this.dataSourceTB0.MouseHover += new System.EventHandler(this.DataSourceTB0_MouseHover);
            this.dataSourceTB1.MouseHover += new System.EventHandler(this.DataSourceTB1_MouseHover);
            this.comboBox0.SelectionChangeCommitted += new System.EventHandler(this.GetLeftSelectedItemIndex);
            this.comboBox0.TextUpdate += new System.EventHandler(LeftComboBox_TextUpdate);
            this.comboBox0.DropDownClosed += new System.EventHandler(LeftComboBox_ClosedEvent);
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.GetRightSelectedItemIndex);
            this.comboBox1.TextUpdate += new System.EventHandler(RightComboBox_TextUpdate);
            this.comboBox1.DropDownClosed += new System.EventHandler(RightComboBox_ClosedEvent);
        }

        #endregion
    }
}