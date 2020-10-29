using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2.Configuration;
using C2.Controls;
using C2.Controls.DataCharts;
using C2.Core;
using C2.Globalization;

namespace C2.Forms
{
    class StartForm : BaseForm
    {
        RecentFilesView recentFilesView1;
        public StartForm()
        {
            Text = "Start";

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
            this.recentFilesView1.CellSpace = new System.Drawing.Size(16, 100);
            this.recentFilesView1.Dimension = new System.Drawing.Size(4, 3);
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
            this.Name = "StartForm";
            this.VisibleChanged += new System.EventHandler(this.StartForm_VisibleChanged);
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
                if (Program.MainForm != null && !string.IsNullOrEmpty(item.Filename))
                {
                    Program.MainForm.OpenDocument(item.Filename);
                }
            }
        }

        private void StartForm_VisibleChanged(object sender, EventArgs e)
        {
            Global.GetBottomViewPanel().Visible = !this.Visible;
        }

        /*
public override void ApplyTheme(UITheme theme)
{
base.ApplyTheme(theme);

this.Font = theme.DefaultFont;
this.BackColor = theme.Colors. MediumLight;

if (recentFilesView1 != null)
{
recentFilesView1.BackColor = theme.Colors.MediumLight;
recentFilesView1.CellBackColor = Color.Transparent;// metroBox1.BackColor;
recentFilesView1.CellForeColor = PaintHelper.FarthestColor(recentFilesView1.BackColor, theme.Colors.Dark, theme.Colors.Light);
recentFilesView1.ActiveCellBackColor = Color.FromArgb(128, theme.Colors.Sharp);
recentFilesView1.ActiveCellForeColor = theme.Colors.SharpText;
}

if (buttonListBox1 != null)
{
buttonListBox1.BackColor = PaintHelper.GetDarkColor(theme.Colors.MediumLight, 0.1f);
buttonListBox1.ButtonBackColor = Color.Transparent;// theme.Colors.MediumDark;
buttonListBox1.ButtonForeColor = PaintHelper.FarthestColor(buttonListBox1.BackColor, theme.Colors.Dark, theme.Colors.Light);
buttonListBox1.ButtonHoverBackColor = theme.Colors.Sharp;
buttonListBox1.ButtonHoverForeColor = theme.Colors.SharpText;
}
}

protected override void OnCurrentLanguageChanged()
{
base.OnCurrentLanguageChanged();

if (buttonListBox1 != null)
{
btnNew.Text = Lang._("New");
btnOpen.Text = Lang._("Open");
btnOptions.Text = Lang._("Options");
}
}

void btnOptions_Click(object sender, EventArgs e)
{
if (Program.MainForm != null)
{
Program.MainForm.ShowOptionsDialog();
}
}

void btnOpen_Click(object sender, EventArgs e)
{
if (Program.MainForm != null)
{
Program.MainForm.OpenDocument();
}
}

void btnNew_Click(object sender, EventArgs e)
{
if (Program.MainForm != null)
{
Program.MainForm.NewDocument();
}
}
*/
    }
}
