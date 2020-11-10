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
        private const int srcDataImage = 3;
        private const int rsDataImage = 2;
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

            AddTopicData(topic);
            foreach (Topic subTopic in topic.Children)
            {
                BuildTree(subTopic, node.Nodes);
            }
            if (!topic.Folded)
                node.Expand();
            return node;
        }
        private bool FindNode(TreeNodeCollection nodes, string text)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == text && node.ImageIndex != 0)
                    return true;
            }
            return false;
        }
        public void AddTopicData(Topic topic)
        {
            TreeNodeCollection nodes = FindNode(topic).Nodes;
            DataSourceWidget dtw = topic.FindWidget<DataSourceWidget>();
            ResultWidget rs      = topic.FindWidget<ResultWidget>();
            if (dtw != null)
            {
                AddWidgetData(nodes, dtw.DataItems,srcDataImage);
            }
            if (rs != null)
            {
                AddWidgetData(nodes, rs.DataItems,rsDataImage);
            }
        }
        public void AddWidgetData(TreeNodeCollection nodes,
                                  List<DataItem> dataItems,
                                  int imageIndex)
        {
            foreach (DataItem dataItem in dataItems)
            {
                TreeNode node = new TreeNode(dataItem.FileName);
                node.ImageIndex = node.SelectedImageIndex = imageIndex;
                if (!FindNode(nodes, dataItem.FileName))
                    nodes.Add(node);
            }
        }
    }
}
