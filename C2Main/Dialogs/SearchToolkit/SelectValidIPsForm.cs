using C2.Controls;
using C2.SearchToolkit;
using C2.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Dialogs.SearchToolkit
{
    partial class SelectValidIPsForm : StandardDialog
    {
        //public List<string> SelectDaemonList;
        private SearchTaskInfo task;

        public SelectValidIPsForm(SearchTaskInfo task)
        {
            InitializeComponent();

            this.task = task;
            RefreshIPListView();
        }

        private void RefreshIPListView()
        {
            int ipCount = 0;
            this.dictListView.Items.Clear();
            string defualtPort = ConvertUtil.GetPort(task.SearchAgentIP);

            foreach (string ip in task.DaemonIP)
            {
                ipCount++;

                ListViewItem lvi = new ListViewItem(ipCount + "");
                lvi.Tag = ip;
                lvi.SubItems.Add(ip);
                lvi.SubItems.Add(defualtPort);

                this.dictListView.Items.Add(lvi);
            }
        }
    }
}
