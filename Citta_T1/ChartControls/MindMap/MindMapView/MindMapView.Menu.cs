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

            MenuDesign.Text = "修改";
            MenuRunning.Text = "运行";
            MenuPublic.Text = "发布";
            MenuDelete.Text = "删除";
            MenuDelete.Click += new System.EventHandler(MenuDelete_Click);

            WidgetMenuStrip.Items.Add(MenuOpenOperator);
        }

        void MenuDelete_Click(object sender, EventArgs e)
        {
            Delete(new ChartObject[] { opw });
        }



        public void CreateDataSourceMenu()
        {
            dtw = HoverObject.Widget as DataSourceWidget;

            ToolStripMenuItem MenuOpenDataSource = new ToolStripMenuItem();
            MenuOpenDataSource.Text = "Data";

            WidgetMenuStrip.Items.Add(MenuOpenDataSource);
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
