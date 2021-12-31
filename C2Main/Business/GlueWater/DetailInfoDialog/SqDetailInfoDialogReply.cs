using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Business.GlueWater
{
    public partial class SqDetailInfoDialogReply : Form
    {
        public DataTable DetailTable;
        public DataTable dt;
        public string replyInfo;
        public int replyCount;
        public string topic;
        public string postInfo;
        public string nickname;
        string time;
        public SqDetailInfoDialogReply()
        {
            InitializeComponent();
            DetailTable = new DataTable();
            dataGridView.DataSource = DetailTable;
            // 列头高度大小
            dataGridView.ColumnHeadersHeight = 30;
            // 列头居中
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);

            
            dt = new DataTable();
            dataGridView1.DataSource = dt;
            dt.Columns.Add(new DataColumn("昵称", typeof(string)));
            dt.Columns.Add(new DataColumn("回帖时间", typeof(string)));
            dt.Columns.Add(new DataColumn("回帖内容", typeof(string)));
           
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);

            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 20;
            dataGridView1.Columns[2].FillWeight = 70;
            
        }
        public void RefreshDGV()
        {
            dataGridView1.Refresh();
            dataGridView.DataSource = DetailTable;

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].Width = 300;
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[3].Visible = false;
            
            int width = 0;
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
                this.dataGridView.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.DisplayedCells);
                width += this.dataGridView.Columns[i].Width;

                this.dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //默认展示第一行详情信息
            if (dataGridView.Rows.Count != 0)
            {
                topic = dataGridView.Rows[0].Cells["主题"].Value.ToString().Trim();
                nickname = dataGridView.Rows[0].Cells["用户名称"].Value.ToString().Trim();
                replyInfo = dataGridView.Rows[0].Cells["回帖内容"].Value.ToString().Trim();
                CountReplyNums();
                deatilGridView();
            }
            else 
            {
                textBox1.Text = string.Empty;
                dt.Rows.Clear();
            }
            
            

            dataGridView.Refresh();
        }

        public void CountReplyNums()
        {
            textBox1.Text = topic;
        }

        public void deatilGridView()
        {
            //回帖时间
            time = replyInfo.Substring(replyInfo.IndexOf(']') + 1).Trim();
            string subString = "]";
            while (time.Contains(subString))
                time = time.Substring(replyInfo.IndexOf(']') + 1).Trim();
            if (!time.Contains("-"))
                time = Regex.Replace(time, @"\s", "");
            dt.Rows.Clear();
            //回帖内容
            Regex rgx = new Regex(@"(?i)(?<=\[)(.*)(?=\])");
            string replyContent = rgx.Match(replyInfo).Value.Replace("「","").Replace("」", "").Replace(".","");

            DataRow dr;
            dr = dt.NewRow();
            dr["昵称"] = nickname;
            dr["回帖时间"] = time;
            dr["回帖内容"] = replyContent;
            dt.Rows.Add(dr);
        }

        
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                topic = dataGridView.Rows[e.RowIndex].Cells["主题"].Value.ToString().Trim();
                nickname = dataGridView.Rows[e.RowIndex].Cells["用户名称"].Value.ToString().Trim();
                replyInfo = dataGridView.Rows[e.RowIndex].Cells["回帖内容"].Value.ToString().Trim();
                

                CountReplyNums();
                deatilGridView();
            }
            catch { }
        }
    }
}
