namespace Citta_T1.Dialogs
{
    partial class CreateNewModel
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
            this.formCreateModel1 = new Citta_T1.FormCreateModel();
            this.SuspendLayout();
            // 
            // formCreateModel1
            // 
            this.formCreateModel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.formCreateModel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formCreateModel1.Location = new System.Drawing.Point(0, 0);
            this.formCreateModel1.Name = "formCreateModel1";
            this.formCreateModel1.Size = new System.Drawing.Size(558, 252);
            this.formCreateModel1.TabIndex = 0;
            // 
            // CreateNewModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 252);
            this.Controls.Add(this.formCreateModel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(250, 250);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateNewModel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "新建模型";
            this.ResumeLayout(false);

        }

        #endregion

        private FormCreateModel formCreateModel1;
    }
}