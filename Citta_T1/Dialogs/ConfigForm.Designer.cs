namespace Citta_T1.Dialogs
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.userModelConfigPage = new System.Windows.Forms.TabPage();
            this.pythonConfigPage = new System.Windows.Forms.TabPage();
            this.socialNetworkConfigPage = new System.Windows.Forms.TabPage();
            this.aboutPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.userModelConfigPage);
            this.tabControl.Controls.Add(this.pythonConfigPage);
            this.tabControl.Controls.Add(this.socialNetworkConfigPage);
            this.tabControl.Controls.Add(this.aboutPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(642, 410);
            this.tabControl.TabIndex = 0;
            // 
            // userModelConfigPage
            // 
            this.userModelConfigPage.Location = new System.Drawing.Point(4, 26);
            this.userModelConfigPage.Name = "userModelConfigPage";
            this.userModelConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.userModelConfigPage.Size = new System.Drawing.Size(634, 380);
            this.userModelConfigPage.TabIndex = 0;
            this.userModelConfigPage.Text = "用户空间配置";
            this.userModelConfigPage.UseVisualStyleBackColor = true;
            // 
            // pythonConfigPage
            // 
            this.pythonConfigPage.Location = new System.Drawing.Point(4, 26);
            this.pythonConfigPage.Name = "pythonConfigPage";
            this.pythonConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.pythonConfigPage.Size = new System.Drawing.Size(634, 380);
            this.pythonConfigPage.TabIndex = 1;
            this.pythonConfigPage.Text = "Python引擎";
            this.pythonConfigPage.UseVisualStyleBackColor = true;
            // 
            // socialNetworkConfigPage
            // 
            this.socialNetworkConfigPage.Location = new System.Drawing.Point(4, 26);
            this.socialNetworkConfigPage.Name = "socialNetworkConfigPage";
            this.socialNetworkConfigPage.Size = new System.Drawing.Size(634, 380);
            this.socialNetworkConfigPage.TabIndex = 2;
            this.socialNetworkConfigPage.Text = "社交关系分析";
            this.socialNetworkConfigPage.UseVisualStyleBackColor = true;
            // 
            // aboutPage
            // 
            this.aboutPage.Location = new System.Drawing.Point(4, 26);
            this.aboutPage.Name = "aboutPage";
            this.aboutPage.Padding = new System.Windows.Forms.Padding(3);
            this.aboutPage.Size = new System.Drawing.Size(634, 380);
            this.aboutPage.TabIndex = 3;
            this.aboutPage.Text = "关于/注册";
            this.aboutPage.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 410);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "首选项";
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage userModelConfigPage;
        private System.Windows.Forms.TabPage pythonConfigPage;
        private System.Windows.Forms.TabPage socialNetworkConfigPage;
        private System.Windows.Forms.TabPage aboutPage;
    }
}