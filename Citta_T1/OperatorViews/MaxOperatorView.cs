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
    public partial class MaxOperatorView : BaseOperatorView
    {
        public MaxOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitOptionInfo();
            LoadOption();

            SetTextBoxName(this.dataSourceTB0);
            this.maxValueBox.Leave += new EventHandler(optionInfoCheck.Control_Leave);
            this.maxValueBox.KeyUp += new KeyEventHandler(optionInfoCheck.Control_KeyUp);
            this.maxValueBox.SelectionChangeCommitted += new EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
        }
        #region 添加取消
        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.dataSourceTB0.Text == String.Empty) return;
            if (this.maxValueBox.Text == String.Empty)
            {
                MessageBox.Show("请选择最大值字段");
                return;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;

            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.outListCCBL0.GetItemCheckText());
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            //输出变化，重写BCP文件
            List<string> outName = new List<string>();
            foreach (string index in this.opControl.Option.GetOptionSplit("outfield"))
            { outName.Add(this.nowColumnsName0[Convert.ToInt32(index)]); }
            if (String.Join(",", this.oldOutList0) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().DoOutputCompare(this.oldColumnsName0, outName, this.opControl.ID);

           

        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.outListCCBL0.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList0);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join("\t", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);
            if (this.maxValueBox.Text == String.Empty)
                this.opControl.Option.SetOption("maxfield", String.Empty);
            else
                this.opControl.Option.SetOption("maxfield", this.maxValueBox.Tag == null ? this.maxValueBox.SelectedIndex.ToString() : this.maxValueBox.Tag.ToString());
            
            
            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }

        private void LoadOption()
        {
            int maxIndex = -1;
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "maxfield"))
            {
                maxIndex = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                this.maxValueBox.Text = this.maxValueBox.Items[maxIndex].ToString();
                this.maxValueBox.Tag = maxIndex.ToString();
            }
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "outfield"))
            {

                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] outIndexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = outIndexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(outIndexs);
                foreach (int i in outIndexs)
                    this.oldColumnsName0.Add(this.outListCCBL0.Items[i].ToString());
            }

            this.opControl.Option.SetOption("columnname0", string.Join("\t", this.opControl.FirstDataSourceColumns));
        }
        #endregion
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataSourceFFP0 = dataInfo["dataPath0"];
                this.dataSourceTB0.Text = Path.GetFileNameWithoutExtension(this.dataSourceFFP0);
                this.toolTip1.SetToolTip(this.dataSourceTB0, this.dataSourceFFP0);
                SetOption(this.dataSourceFFP0, this.dataSourceTB0.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
        }

        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.nowColumnsName0 = bcpInfo.ColumnArray;
            foreach (string name in this.nowColumnsName0)
            {
                this.outListCCBL0.AddItems(name);
                this.maxValueBox.Items.Add(name);
            }
            this.opControl.FirstDataSourceColumns = this.nowColumnsName0; 
        }
        #endregion
    }

}
