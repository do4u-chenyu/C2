using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Linq;
using C2.Configuration;
using C2.Core;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Styles;
using C2.Model.Widgets;
using System.Collections.Generic;
using C2.Globalization;

namespace C2.Controls.MapViews
{
    public partial class MindMapView 
    {
        public ContextMenuStrip WidgetMenuStrip = new ContextMenuStrip();
        
        private DataSourceWidget dtw;
        private OperatorWidget opw;
        private ResultWidget rsw;


        public void CreateWidgetMenu()
        {
            WidgetMenuStrip.Items.Clear();

            switch (HoverObject.Widget.GetTypeID())
            {
                case OperatorWidget.TypeID:
                    CreateOperatorMenu();
                    break;
                case DataSourceWidget.TypeID:
                    CreateDataSourceMenu();
                    break;
                case ResultWidget.TypeID:
                    CreateResultMenu();
                    break;
                default:
                    break;
            }
           
        }

        public void CreateOperatorMenu()
        {
            opw = HoverObject.Widget as OperatorWidget;

            ToolStripMenuItem MenuOpenOperator = new ToolStripMenuItem();
            ToolStripMenuItem MenuDesign = new ToolStripMenuItem();
            ToolStripMenuItem MenuRunning = new ToolStripMenuItem();
            ToolStripMenuItem MenuPublic = new ToolStripMenuItem();
            ToolStripMenuItem MenuDelete = new ToolStripMenuItem();

            MenuOpenOperator.Text = opw.OpType;
            MenuOpenOperator.DropDownItems.AddRange(new ToolStripItem[] {
                MenuDesign,
                MenuRunning,
                MenuPublic,
                MenuDelete});

            MenuDesign.Text = Lang._("Design");
            MenuRunning.Text = Lang._("Running");
            MenuPublic.Text = Lang._("Public");
            MenuDelete.Text = Lang._("Delete");
            MenuDelete.Click += new System.EventHandler(MenuDeleteOp_Click);

            WidgetMenuStrip.Items.Add(MenuOpenOperator);
        }

        void MenuDeleteOp_Click(object sender, EventArgs e)
        {
            Delete(new ChartObject[] { opw });
        }

        void MenuDelete_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            // 剩余最后一个菜单项，删除数据源挂件
            if (dtw.DataItems.Count == 1)
                Delete(new ChartObject[] { dtw });
            else
                dtw.DataItems.Remove(hitItem);
        }
        void DSWidgetMenuDelete_Click(object sender, EventArgs e)
        {
            Delete(new ChartObject[] { opw });
        }
        void MenuViewData_Click(object sender, EventArgs e)
        {
    
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            if (hitItem != null)
                Global.GetMainForm().PreViewDataByFullFilePath(hitItem);
        }


        public void CreateDataSourceMenu()
        {
            dtw = HoverObject.Widget as DataSourceWidget;
            WidgetMenuStrip.SuspendLayout();
            foreach (DataItem dataItem in dtw.DataItems)
            {
                ToolStripMenuItem MenuViewData = new ToolStripMenuItem();
                ToolStripMenuItem MenuGetChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuDelete = new ToolStripMenuItem();
                ToolStripMenuItem MenuOpenDataSource = new ToolStripMenuItem();
                MenuOpenDataSource.Image = Properties.Resources.data_w_icon;

                MenuOpenDataSource.Text = dataItem.FileName;
                MenuOpenDataSource.DropDownItems.AddRange(new ToolStripItem[] {
                MenuViewData,
                MenuGetChart,
                MenuDelete});
            
                MenuViewData.Image = Properties.Resources.viewdata;
                MenuViewData.Tag = dataItem;
                MenuViewData.Text = Lang._("ViewData");
                MenuViewData.Click += MenuViewData_Click;

                MenuGetChart.Image = Properties.Resources.getchart;              
                MenuGetChart.Text = Lang._("GetChart");

                MenuDelete.Image = Properties.Resources.deletewidget;
                MenuDelete.Text = Lang._("Delete");
                MenuDelete.Tag = dataItem;
                MenuDelete.Click += MenuDelete_Click;

                WidgetMenuStrip.Items.Add(MenuOpenDataSource);           
            }
            WidgetMenuStrip.ResumeLayout();
            if (UITheme.Default != null)
            {
                WidgetMenuStrip.Renderer = UITheme.Default.ToolStripRenderer;
            }
        }

        public void CreateResultMenu()
        {
            dtw = HoverObject.Widget as DataSourceWidget;

            ToolStripMenuItem MenuOpenResult = new ToolStripMenuItem();
            MenuOpenResult.Text = "Result";

            WidgetMenuStrip.Items.Add(MenuOpenResult);
        }


    }
}
