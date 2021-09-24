using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellManageForm : Form
    {
        public WebShellManageForm()
        {
            InitializeComponent();
        }

        private void AddShellMenu_Click(object sender, EventArgs e)
        {
            string addId = "1";//TODO 找可用的id
            AddWebShell dialog = new AddWebShell(addId);
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            ListViewItem lvi = new ListViewItem(addId);
            lvi.Tag = dialog.WebShellTask;
            lvi.SubItems.Add(dialog.WebShellTask.TaskName);
            lvi.SubItems.Add(dialog.WebShellTask.TaskUrl);
            lvi.SubItems.Add(dialog.WebShellTask.TaskType.ToString());
            lvi.SubItems.Add(dialog.WebShellTask.TaskRemark);
            lvi.SubItems.Add(dialog.WebShellTask.TaskAddTime);

            this.listView1.Items.Add(lvi);
        }

        private void EnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                return;
            }
            new WebShellDetails((WebShellTaskInfo)this.listView1.SelectedItems[0].Tag).ShowDialog();
        }
    }
}
