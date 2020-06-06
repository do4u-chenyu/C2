using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{

    public partial class RandomOperatorView : BaseOperatorView
    {
        private string oldRandomNum;
        private List<string> oldColumnsName = new List<string>();

        public RandomOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitOptionInfo();
            LoadOption();
            this.oldRandomNum = this.randomNumBox.Text;

            SetTextBoxName(this.dataSourceTB0);
        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            int startID = -1;
            string encoding = String.Empty;
            char separator = OpUtil.DefaultSeparator;
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.EndID == this.opControl.ID)
                {
                    startID = mr.StartID;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    this.dataSourceFFP0 = me.FullFilePath;
                    separator = me.Separator;
                    //设置数据信息选项
                    this.dataSourceTB0.Text = Path.GetFileNameWithoutExtension(this.dataSourceFFP0);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if (this.dataSourceFFP0 != String.Empty)
                SetOption(this.dataSourceFFP0, this.dataSourceTB0.Text, encoding, separator);

        }
        private void SetOption(string path, string dataName, string encoding, char separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.nowColumnsName0 = bcpInfo.ColumnArray;
            foreach (string name in nowColumnsName0)
            {
                this.outListCCBL0.AddItems(name);
            }

            this.opControl.FirstDataSourceColumns = this.nowColumnsName0;
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.nowColumnsName0));
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            this.opControl.Option.SetOption("randomnum", this.randomNumBox.Text);
            List<int> checkIndexs = this.outListCCBL0.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList0);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join("\t", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }

        private void LoadOption()
        {
            if (!string.IsNullOrEmpty( this.opControl.Option.GetOption("randomnum")))
                this.randomNumBox.Text = this.opControl.Option.GetOption("randomnum");
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "outfield"))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnsName.Add(this.outListCCBL0.Items[index].ToString());
            }

        }
        #endregion
        #region 添加取消
        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.randomNumBox.Text == String.Empty)
            {
                MessageBox.Show("随机条数字段不能为空,请输入一个整数");
                return;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择算子要输出的字段");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.dataSourceTB0.Text == String.Empty) return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldRandomNum != this.randomNumBox.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if (String.Join(",", this.oldOutList0) != this.opControl.Option.GetOption("outfield"))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            List<string> outName = new List<string>();
            foreach (string index in this.opControl.Option.GetOptionSplit("outfield"))
            { outName.Add(this.nowColumnsName0[Convert.ToInt32(index)]); }
            if (String.Join(",", this.oldOutList0) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().DoOutputCompare(this.oldColumnsName, outName, this.opControl.ID);
        }
        #endregion

        private void RandomNumBox_Leave(object sender, EventArgs e)
        {
            ConvertUtil.ControlTextTryParseInt(randomNumBox, "\"{0}\" 不是数字, 请输入一个整数.");
        }
    }
}
