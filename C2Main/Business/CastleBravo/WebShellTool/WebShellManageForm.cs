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

            webShellTasks.Add(dialog.WebShellTask);
        }

        private void EnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                return;
            }
            new WebShellDetails((WebShellTaskInfo)this.listView1.SelectedItems[0].Tag).ShowDialog();
        }

        private void SaveShellMenu_Click(object sender, EventArgs e)
        {
            SaveDB();
        }

        private void SaveDB()
        {
            using (Stream stream = File.Open(webShellFilePath + "\\webshells.db", FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, webShellTasks);
                MessageBox.Show("data saved in webshells.db");
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
    }
}
