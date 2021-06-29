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
        public void AddTopicData(Topic topic)
        {
            if (FindNode(topic) == null)
                return;
            TreeNodeCollection nodes = FindNode(topic).Nodes;
            for(int i = nodes.Count-1;i >= 0;i--)
            {
                if ((nodes[i].ImageIndex == rsDataImage) || (nodes[i].ImageIndex == srcDataImage))
                    nodes[i].Remove();
            }

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
                InsertNode(nodes, dataItem.FileName, imageIndex);
            }
        }
        public void InsertNode(TreeNodeCollection nodes, 
                               string text, 
                               int imageIndex)
        {
            TreeNode node = new TreeNode(text);
            node.ImageIndex = node.SelectedImageIndex = imageIndex;
            int count = 0;
            foreach (TreeNode nodeTmp in nodes)
            {
                if (nodeTmp.ImageIndex == srcDataImage)
                    count += 1;
                if (nodeTmp.ImageIndex == 0)
                    break;
            }
            nodes.Insert(imageIndex == srcDataImage? 0:count, node);
        }
        public void DelTopicData(Topic topic, DataItem dataItem)
        {
            TreeNodeCollection nodes = FindNode(topic).Nodes;
            TreeNode node = new TreeNode();
            foreach (TreeNode nodeTmp in nodes)
            {
                if (nodeTmp.ImageIndex == 0)
                    return;
                if (nodeTmp.Text == dataItem.FileName)
                {
                    node = nodeTmp;
                    break;
                }          
            }
            nodes.Remove(node);
        }
    }
}
