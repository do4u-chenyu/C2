using C2.Utils;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C2.Business.GlueWater
{
    public partial class SqDetailInfoDialog : Form
    {
        public DataTable DetailTable;
        public DataTable dt;
        public string replyInfo;
        public int replyCount;
        public string topic;
        public string postInfo;


        public SqDetailInfoDialog()
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
            dt.Columns.Add(new DataColumn("回帖信息", typeof(string)));
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            
            dataGridView1.Columns[0].FillWeight = 10; 
            dataGridView1.Columns[1].FillWeight = 15; 
            dataGridView1.Columns[2].FillWeight = 70;
            
        }

        public void RefreshDGV()
        {
            dataGridView1.Refresh();
            dataGridView.DataSource = DetailTable;

            dataGridView.Columns[2].Width = 150;
            dataGridView.Columns[3].Width = 100;


            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].Visible = false;             
            dataGridView.Columns[4].Visible = false;//关键词
            dataGridView.Columns[5].Visible = false;
            dataGridView.Columns[6].Visible = false;
            
            int width = 0;
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
                this.dataGridView.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.DisplayedCells);
                width += this.dataGridView.Columns[i].Width;

                this.dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            
            //默认展示第一行详情信息
            topic = dataGridView.Rows[0].Cells["主题"].Value.ToString().Trim();
            postInfo = dataGridView.Rows[0].Cells["发帖信息"].Value.ToString().Trim();
            replyInfo = dataGridView.Rows[0].Cells["回帖信息"].Value.ToString().Trim();
            CountReplyNums();
            deatilGridView();
            

            dataGridView.Refresh();
        }

        public void CountReplyNums()
        {
            string subString = "发表于";
            if (replyInfo.Contains(subString))
            {
                string strReplaced = replyInfo.Replace(subString, "");
                replyCount = (replyInfo.Length - strReplaced.Length) / subString.Length;
                label3.Text = replyCount.ToString();
            }
            else
            {
                label3.Text = 0.ToString();
            }

            textBox1.Text = topic;
            textBox2.Text = OpUtil.StringBlank + OpUtil.StringBlank + postInfo;
        }

        public void deatilGridView()
        {
            string time = string.Empty;
            string[] sArray = replyInfo.Split(']');
            sArray = sArray.Take(sArray.Count() - 1).ToArray();
            dt.Rows.Clear();
            if (label3.Text == 0.ToString())
                dt.Rows.Clear();

            foreach (string i in sArray)
            {
                string nickname = i.Split(' ')[0].Replace("[", "");
                try {time = i.Split(' ')[2] + OpUtil.StringBlank + i.Split(' ')[3].Split(',')[0]; } catch {time = i.Split(' ')[2].Split(',')[0];};
                
                //string replyContent = i.Split(' ')[3].Substring(9).Trim();
                string replyContent = i.Substring(i.IndexOf(',') + 1).Trim();

                DataRow dr;
                dr = dt.NewRow();
                dr["昵称"] = nickname;
                dr["回帖时间"] = time;
                dr["回帖信息"] = replyContent;
                dt.Rows.Add(dr);
            }
        }

        
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //object objCellValue = this.dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            try
            {
                topic = dataGridView.Rows[e.RowIndex].Cells["主题"].Value.ToString().Trim();
                postInfo = dataGridView.Rows[e.RowIndex].Cells["发帖信息"].Value.ToString().Trim();
                replyInfo = dataGridView.Rows[e.RowIndex].Cells["回帖信息"].Value.ToString().Trim();
                
                CountReplyNums();
                deatilGridView();
            }
            catch { }
        }
        
    }
}
