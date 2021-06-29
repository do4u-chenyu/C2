using C2.Controls;
using C2.Core;
using C2.Globalization;
using System;

namespace C2.Forms
{
    class StartForm : BaseForm
    {
        RecentFilesView recentFilesView1;
        public StartForm()
        {
            Text = Lang._("StartPage");

            InitializeComponent();

            AfterInitialize();
        }

        void InitializeComponent()
        {
            this.recentFilesView1 = new C2.Controls.RecentFilesView();
            this.SuspendLayout();
            // 
            // recentFilesView1
            // 
            this.recentFilesView1.ActiveCellBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.recentFilesView1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.recentFilesView1.CellBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.recentFilesView1.CellSpace = new System.Drawing.Size(20, 60);
            this.recentFilesView1.Dimension = new System.Drawing.Size(4, 2);
            this.recentFilesView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentFilesView1.Location = new System.Drawing.Point(0, 0);
            this.recentFilesView1.Name = "recentFilesView1";
            this.recentFilesView1.Size = new System.Drawing.Size(784, 462);
            this.recentFilesView1.TabIndex = 0;
            // 
            // StartForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.recentFilesView1);
            this.IconImage = global::C2.Properties.Resources.首页;
            this.Name = "StartForm";
            this.ResumeLayout(false);

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        void metroBox1_ItemClick(object sender, ThumbViewItemEventArgs e)
        {
            if (e.Item is FileThumbItem)
            {
                var item = (FileThumbItem)e.Item;
                if (Global.GetMainForm() != null && !string.IsNullOrEmpty(item.Filename))
                {
                    Global.GetMainForm().OpenDocument(item.Filename);
                }
            }
        }
    }
}
