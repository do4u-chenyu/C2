using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellManageForm : Form
    {
        List<WebShellTaskInfo> webShellTasks;
        private string webShellFilePath;

        public WebShellManageForm()
        {
            InitializeComponent();
            webShellTasks = new List<WebShellTaskInfo>();
            webShellFilePath = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig");
        }

        private void AddShellMenu_Click(object sender, EventArgs e)
        {
            string addId = webShellTasks.Count == 0 ? "1" : int.Parse(webShellTasks.Max(c => c.TaskID)) + 1 + "";
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

            webShellTasks.Add(dialog.WebShellTask);
        }



        private void SaveShellMenu_Click(object sender, EventArgs e)
        {
            SaveDB();
            MessageBox.Show("data saved in webshells.db");
        }

        private void SaveDB()
        {
            using (Stream stream = File.Open(webShellFilePath + "\\webshells.db", FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, webShellTasks);
            }
        }

        private void WebShellManageForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (Stream stream = File.Open(webShellFilePath + "\\webshells.db", FileMode.Open))
                {
                    var binaryFormatter = new BinaryFormatter();
                    webShellTasks = (List<WebShellTaskInfo>)binaryFormatter.Deserialize(stream);
                }

                RefreshListViewInfos();
            }
            catch{ }
        }

        public void RefreshListViewInfos()
        {
            foreach (WebShellTaskInfo ws in webShellTasks)
            {
                ListViewItem lvi = new ListViewItem(ws.TaskID);
                lvi.Tag = ws;
                lvi.SubItems.Add(ws.TaskName);
                lvi.SubItems.Add(ws.TaskUrl);
                lvi.SubItems.Add(ws.TaskType.ToString());
                lvi.SubItems.Add(ws.TaskRemark);
                lvi.SubItems.Add(ws.TaskAddTime);

                listView1.Items.Add(lvi);
            }
        }

        private void WebShellManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveDB();
        }

        private void EnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;

            new WebShellDetails((WebShellTaskInfo)this.listView1.SelectedItems[0].Tag).ShowDialog();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;

            WebShellTaskInfo editTask = webShellTasks.Find(c => c.TaskID == (this.listView1.SelectedItems[0].Tag as WebShellTaskInfo).TaskID);

            AddWebShell dialog = new AddWebShell(editTask);
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            this.listView1.SelectedItems[0].Tag = dialog.WebShellTask;
            editTask.TaskName = this.listView1.SelectedItems[0].SubItems[1].Text = dialog.WebShellTask.TaskName;
            editTask.TaskUrl = this.listView1.SelectedItems[0].SubItems[2].Text = dialog.WebShellTask.TaskUrl;
            editTask.TaskRemark = this.listView1.SelectedItems[0].SubItems[4].Text = dialog.WebShellTask.TaskRemark;
            editTask.TaskAddTime = this.listView1.SelectedItems[0].SubItems[5].Text = dialog.WebShellTask.TaskAddTime;

            SaveDB();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;

            webShellTasks.Remove(webShellTasks.Find(c => c.TaskID == (this.listView1.SelectedItems[0].Tag as WebShellTaskInfo).TaskID));
            this.listView1.SelectedItems[0].Remove();
            SaveDB();
        }
    }
}
