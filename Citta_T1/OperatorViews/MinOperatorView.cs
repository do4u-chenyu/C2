using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class MinOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldMinfield;
        private List<int> oldOutList;
        private string[] columnName;
        private string oldOptionDict;
        private List<string> oldColumnName;
        private bool hasNewDataSource;
        private LogUtil log = LogUtil.GetInstance("MinOperatorView");
        public MinOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.hasNewDataSource = false;
            dataPath = "";
            columnName = new string[] { };
            oldColumnName = new List<string>();
            oldOutList = new List<int>();
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();

            this.oldMinfield = this.MinValueBox.Text;
            this.oldOutList = this.OutList.GetItemCheckIndex();
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
        }
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.MinValueBox.Text == "")
            {
                MessageBox.Show("请选择最小值字段!");
                return;
            }
            if (this.OutList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.DataInfoBox.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldMinfield != this.MinValueBox.Text|| !this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            {
                Global.GetOptionDao().CreateResultControl(this.opControl, this.OutList.GetItemCheckText());
                return;
            }
            //输入数据源变化，并且输出重写
            //if (hasResutl != null && this.hasNewDataSource)
            //{
            //    Global.GetOptionDao().ModifyOut(this.OutList.GetItemCheckText(), this.opControl.ID);
            //    return;
            //}

            //输出变化，重写BCP文件
            if (hasResutl != null && !this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                Global.GetOptionDao().IsModifyOut(this.oldColumnName, this.OutList.GetItemCheckText(), this.opControl.ID);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList);
            List<int> removeIndex = new List<int>();
            foreach (int index in checkIndexs)
            {
                if (!outIndexs.Contains(index))
                    outIndexs.Add(index);
            }
            foreach (int index in outIndexs)
            {
                if (!checkIndexs.Contains(index))
                {
                    outIndexs = new List<int>(checkIndexs);
                    break;
                }
            }
            string outField = string.Join(",", checkIndexs);
            if (this.MinValueBox.Text == "")
                this.opControl.Option.SetOption("minfield", "");
            else
                this.opControl.Option.SetOption("minfield", this.MinValueBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("outfield", outField);
            if (this.MinValueBox.Text != "" && outField != "")
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("minfield") != "")
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("minfield"));
                this.MinValueBox.Text = this.MinValueBox.Items[index].ToString();
            }
            if (this.opControl.Option.GetOption("outfield") != "")
            {

                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.OutList.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName.Add(this.OutList.Items[index].ToString());
            }
            
        }
        #endregion
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                SetOption(this.dataPath, this.DataInfoBox.Text, dataInfo["encoding0"]);
            }
        }
        private void SetOption(string path, string dataName, string encoding)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split('\t');
            foreach (string name in columnName)
            {
                this.OutList.AddItems(name);
                this.MinValueBox.Items.Add(name);
            }
            CompareDataSource();
            this.opControl.SingleDataSourceColumns = column;
            this.opControl.Option.SetOption("columnname", this.opControl.SingleDataSourceColumns);
        }
        private void CompareDataSource()
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断
            if (this.opControl.Option.GetOption("columnname") == "") return;
            string[] oldColumnList = this.opControl.Option.GetOption("columnname").Split('\t');
            try
            {
                if (this.opControl.Option.GetOption("minfield") != "")
                {
                    int index = Convert.ToInt32(this.opControl.Option.GetOption("minfield"));
                    if (index > this.columnName.Length - 1 || oldColumnList[index] != this.columnName[index])
                        this.opControl.Option.OptionDict.Remove("minfield");
                }
                if (this.opControl.Option.GetOption("outfield") != "")
                {

                    string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (Global.GetOptionDao().IsDataSourceEqual(oldColumnList, this.columnName, outIndex))
                    {
                        this.opControl.Option.OptionDict.Remove("outfield");
                        this.hasNewDataSource = true;
                    }
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
           
        }
        #endregion
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); } 
    }
}
