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
                nodeDoc.ImageIndex = nodeDoc.SelectedImageIndex = 0;
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
            DataTreeNode node = new DataTreeNode(topic);
            node.ImageIndex = node.SelectedImageIndex = 3;
            nodes.Add(node);

            foreach (Topic subTopic in topic.Children)
            {
                BuildTree(subTopic, node.Nodes);
            }

            if (!topic.Folded)
                node.Expand();
            return node;
        }
    }
}
