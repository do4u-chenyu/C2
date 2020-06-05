namespace Citta_T1.OperatorViews.Base
{
    partial class BaseOperatorView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
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
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // dataSource0
            // 
            this.dataSourceTB0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSourceTB0.Location = new System.Drawing.Point(4, 2);
            this.dataSourceTB0.Margin = new System.Windows.Forms.Padding(2);
            this.dataSourceTB0.Name = "dataSourceTB0";
            this.dataSourceTB0.ReadOnly = true;
            this.dataSourceTB0.Size = new System.Drawing.Size(144, 23);
            this.dataSourceTB0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataSource1
            // 
            this.dataSourceTB0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSourceTB0.Location = new System.Drawing.Point(187, 2);
            this.dataSourceTB0.Margin = new System.Windows.Forms.Padding(2);
            this.dataSourceTB0.Name = "dataSourceTB1";
            this.dataSourceTB0.ReadOnly = true;
            this.dataSourceTB0.Size = new System.Drawing.Size(144, 23);
            this.dataSourceTB0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OutList0
            // 
            this.outListCCBL0.DataSource = null;
            this.outListCCBL0.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outListCCBL0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.outListCCBL0.Name = "OutList0";
            // 
            // OutList1
            // 
            this.outListCCBL1.DataSource = null;
            this.outListCCBL1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outListCCBL1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.outListCCBL1.Name = "OutList1";
            // 
            // BaseOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(473, 266);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BaseOperatorView";
            this.ShowIcon = false;
            this.Text = "BaseOperatorView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.TextBox dataSourceTB1; // 左表框
        protected System.Windows.Forms.TextBox dataSourceTB0; // 右表框
        protected System.Windows.Forms.Button cancelButton;   // 取消键
        protected System.Windows.Forms.Button confirmButton;  // 确认键
        protected System.Windows.Forms.ToolTip toolTip1;      // 浮动提示栏,一个就够了
        protected Citta_T1.Controls.Common.ComCheckBoxList outListCCBL0;
        protected Citta_T1.Controls.Common.ComCheckBoxList outListCCBL1;
    }
}