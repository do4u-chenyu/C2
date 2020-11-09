using C2.Controls.MapViews;
using C2.Model.MindMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using C2.Core;
using C2.Model.Documents;
using C2.Model;
using C2.Model.Widgets;

namespace C2.Controls.MapViews
{
    class DataTreeView: ObjectTreeView
    {
        public override void BuildTree()
        {
            Nodes.Clear();

            if (ChartPage != null)
            {
                DocumentTreeNode nodeDoc = new DocumentTreeNode(ChartPage);
                nodeDoc.ImageIndex = nodeDoc.SelectedImageIndex = 1;
                Nodes.Add(nodeDoc);

                //========
                if (ChartPage is MindMap)
                {
                    var mindMap = (MindMap)ChartPage;
                    if (mindMap.Root != null)
                    {
                        TreeNode root = BuildTree(mindMap.Root, nodeDoc.Nodes);

                        root.Expand();
                    }
                }

                nodeDoc.Expand();
            }
        }
        public override TreeNode BuildTree(Topic topic, TreeNodeCollection nodes)
        {
            TopicTreeNode node = new TopicTreeNode(topic);
            node.ImageIndex = node.SelectedImageIndex = 0;
            nodes.Add(node);
            AddWidgetData(topic, node.Nodes);

            foreach (Topic subTopic in topic.Children)
            {
                BuildTree(subTopic, node.Nodes);
            }

            if (!topic.Folded)
                node.Expand();
            return node;
        }
        private void AddWidgetData(Topic topic, TreeNodeCollection nodes)
        {
            DataSourceWidget dtw = topic.FindWidget<DataSourceWidget>();
            ResultWidget rs      = topic.FindWidget<ResultWidget>();
            if (dtw != null)
            {
                foreach (DataItem dataItem in dtw.DataItems)
                {
                    TreeNode node = new TreeNode(dataItem.FileName);
                    node.ImageIndex = node.SelectedImageIndex = 3;
                    nodes.Add(node);
                }
            }
            if (rs != null)
            {
                foreach (DataItem dataItem in rs.DataItems)
                {
                    TreeNode node = new TreeNode(dataItem.FileName);
                    node.ImageIndex = node.SelectedImageIndex = 2;
                    nodes.Add(node);
                }
            }
        }
    }
}
