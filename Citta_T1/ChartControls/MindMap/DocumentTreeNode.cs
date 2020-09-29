using System;
using System.ComponentModel;
using System.Windows.Forms;
using Citta_T1.Core;
using Citta_T1.Model.Documents;

namespace Citta_T1.Controls.MapViews
{
    class DocumentTreeNode : TreeNode
    {
        ChartPage _ChartPage;

        public DocumentTreeNode()
        {
        }

        public DocumentTreeNode(ChartPage chartPage)
        {
            ChartPage = chartPage;
        }

        [DefaultValue(true)]
        public ChartPage ChartPage
        {
            get { return _ChartPage; }
            set
            {
                if (_ChartPage != value)
                {
                    ChartPage old = _ChartPage;
                    _ChartPage = value;
                    OnChartPageChanged(old);
                }
            }
        }

        protected virtual void OnChartPageChanged(ChartPage old)
        {
            if (old != null)
            {
                old.NameChanged -= new EventHandler(MindMap_NameChanged);
            }

            if (ChartPage != null)
            {
                Text = ChartPage.ToString();
                Tag = ChartPage;
                ChartPage.NameChanged += new EventHandler(MindMap_NameChanged);
            }
        }

        private void MindMap_NameChanged(object sender, EventArgs e)
        {
            if (ChartPage != null)
            {
                Text = ChartPage.Name;
            }
        }
    }
}
