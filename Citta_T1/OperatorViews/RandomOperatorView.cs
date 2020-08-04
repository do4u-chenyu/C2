﻿using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{

    public partial class RandomOperatorView : BaseOperatorView
    {
        public RandomOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();
        }
        #region 初始化配置
        private void InitByDataSource()
        {   // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
           
        }
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            this.opControl.Option.SetOption("randomnum", this.randomNumBox.Text);
            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

            this.randomNumBox.Text = this.opControl.Option.GetOption("randomnum");
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());
            }
           

        }
        #endregion
        #region 判断是否配置完毕
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;

            if (this.dataSourceTB0.Text == String.Empty)
                return notReady;

            if (this.randomNumBox.Text == String.Empty)
            {
                MessageBox.Show("随机条数字段不能为空,请输入一个整数");
                return notReady;
            }
            if (ConvertUtil.ControlTextTryParseInt(randomNumBox))
            {
                MessageBox.Show("请输入小于" + int.MaxValue + "的正整数.");
                return notReady;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择算子要输出的字段");
                return notReady;
            }
            return !notReady;
        }       
        #endregion

    }
}
