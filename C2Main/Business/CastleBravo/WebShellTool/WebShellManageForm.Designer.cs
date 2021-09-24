namespace C2.Business.CastleBravo.WebShellTool
{
    partial class WebShellManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebShellManageForm));
            this.listView1 = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ManagerTC = new System.Windows.Forms.TabControl();
            this.ShellManagerTab = new System.Windows.Forms.TabPage();
            this.ManagerTC.SuspendLayout();
            this.ShellManagerTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(786, 418);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ManagerTC
            // 
            this.ManagerTC.Controls.Add(this.ShellManagerTab);
            this.ManagerTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ManagerTC.Location = new System.Drawing.Point(0, 0);
            this.ManagerTC.Name = "ManagerTC";
            this.ManagerTC.SelectedIndex = 0;
            this.ManagerTC.Size = new System.Drawing.Size(800, 450);
            this.ManagerTC.TabIndex = 1;
            // 
            // ShellManagerTab
            // 
            this.ShellManagerTab.Controls.Add(this.listView1);
            this.ShellManagerTab.Location = new System.Drawing.Point(4, 22);
            this.ShellManagerTab.Name = "ShellManagerTab";
            this.ShellManagerTab.Padding = new System.Windows.Forms.Padding(3);
            this.ShellManagerTab.Size = new System.Drawing.Size(792, 424);
            this.ShellManagerTab.TabIndex = 0;
            this.ShellManagerTab.Text = "ShellManager";
            this.ShellManagerTab.UseVisualStyleBackColor = true;
            // 
            // WebShellManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ManagerTC);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebShellManageForm";
            this.Text = "WebShell管理";
            this.ManagerTC.ResumeLayout(false);
            this.ShellManagerTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.TabControl ManagerTC;
        private System.Windows.Forms.TabPage ShellManagerTab;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}