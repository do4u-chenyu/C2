using Citta_T1.Business.Model;
using Citta_T1.Controls.Move;
using Citta_T1.Core;
using Citta_T1.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class QuickformatWrapper
    {
        private List<ModelRelation> modelRelations;
        private ModelDocument currentModel;
        private List<ModelElement> modelElements;
        private static LogUtil log = LogUtil.GetInstance("QuickformatWrapper");
        private List<int> leafNodeIds;
        private List<int> starNodes;
        private List<int> endNodes;
        private List<List<int>> treeNodes;
        private List<List<List<int>>> treeGroup;
        private Hashtable ht;
        private List<List<List<int>>> recordSearch;
        private List<int> ctWidths;
        private int ctHeight;
        public QuickformatWrapper(ModelDocument currentModel)
        {
            this.currentModel = currentModel;
        }

        private List<int> FindBeforeNodeIds(int id)
        {
            List<int> beforeNodeId = new List<int>();
            foreach (ModelRelation beforeNode in modelRelations.FindAll(c => c.EndID == id))
            {
                beforeNodeId.Add(beforeNode.StartID);
            }
            return beforeNodeId;
        }

        private void FindModelEndNodes()
        {
            //寻找只有入度没有出度的叶子节点

            modelRelations = this.currentModel.ModelRelations;
            this.leafNodeIds = new List<int>();
            this.starNodes = new List<int>();
            this.endNodes = new List<int>();
            foreach (ModelRelation mr in modelRelations)
            {
                this.starNodes.Add(mr.StartID);
                this.endNodes.Add(mr.EndID);
            }
            this.leafNodeIds = endNodes.Except(starNodes).ToList();
        }

        private void SearchTree(List<int> needSearchNodeIds)
        {
            //下一次需要找上游的点
            List<int> nextNeedSearchNodeIds = new List<int>();

            if (needSearchNodeIds.Count == 0)
            {
                return;
            }

            foreach (int resultNodeId in needSearchNodeIds)
            {
                /*拿到一个待溯源的节点
                 * 1、判断这个节点是否在endnode列表里，不在，说明没有入度，即为根，不需要再找上游了。在，下一步。
                 * 2、通过relation找到它对应的start
                 * 3、每层一个列表存放
                 */
                if (this.endNodes.Exists(c => c == resultNodeId))
                {
                    List<int> beforeNodeIds = FindBeforeNodeIds(resultNodeId);
                    nextNeedSearchNodeIds = nextNeedSearchNodeIds.Union(beforeNodeIds).ToList();

                    this.treeNodes.Add(nextNeedSearchNodeIds);
                    break;
                }

            }
            SearchTree(nextNeedSearchNodeIds);
        }
        private List<List<int>> TreeDeepSort(List<List<int>> treeA, List<List<int>> treeB)
        {

            for (int i = 0; i < treeA.Count; i++)
            {
                int dx = i + treeB.Count - treeA.Count;
                treeB[dx] = treeB[dx].Union(treeA[i]).ToList();
            }
            return treeB;
        }

        private bool ExitIntersect(List<List<int>> treeA, List<List<int>> treeB)
        {
            for (int i = 0; i < treeA.Count; i++)
            {
                for (int j = 0; j < treeB.Count; j++)
                {
                    for (int m = 0; m < treeA[i].Count; m++)
                    {
                        if (treeB[j].Contains(treeA[i][m]))
                            return true;
                    }
                }
            }
            return false;
        }
        private void TreeComplete(List<List<int>> treeA, List<List<int>> treeB)
        {
            List<List<List<int>>> key = new List<List<List<int>>>();
            if (!this.recordSearch.Contains(treeA))
            {
                this.recordSearch.Add(treeA);
                key.Add(treeA);
                ht.Add(key, treeA);

            }
            key = new List<List<List<int>>>();
            if (!ExitIntersect(treeA, treeB))
            {
                if (!this.recordSearch.Contains(treeB))
                {
                    this.recordSearch.Add(treeB);
                    key.Add(treeB);
                    ht.Add(key, treeB);
                }
                return;
            }
            key = new List<List<List<int>>>();
            key.Add(treeB);
            foreach (List<List<List<int>>> tmp in ht.Keys)
            {
                if (tmp.Contains(treeB) & tmp.Count == 1)
                {
                    ht.Remove(tmp);
                    break;
                }
            }
            foreach (List<List<List<int>>> tmp in ht.Keys)
            {

                if (tmp.Contains(treeA) & !tmp.Contains(treeB))
                {

                    treeA = (List<List<int>>)ht[tmp];
                    tmp.Add(treeB);
                    this.recordSearch.Add(treeB);
                    if (treeA.Count < treeB.Count)
                        ht[tmp] = TreeDeepSort(treeA.ToList(), treeB.ToList());
                    else
                        ht[tmp] = TreeDeepSort(treeB.ToList(), treeA.ToList());
                    return;
                }
            }

            

        }
        private Point WorldBoundControl(Point Ps, int width, int height)
        {

            Point dragOffset = new Point(0, 0);
            float screenFactor = Global.GetCurrentDocument().ScreenFactor;


            if (Ps.X > 2000 * screenFactor)
            {
                dragOffset.X = Ps.X - 2000;
            }
            if (Ps.Y > 900 * screenFactor)
            {
                dragOffset.Y = Ps.Y - 900;
            }
            return dragOffset;
        }
        private void FormatLoc(int id, int dx, int dy, List<ModelElement> modelElements)
        {

            foreach (ModelElement me in modelElements)
            {
                if (me.ID == id)
                {
                    Control ct = me.GetControl;
                    int left = dx + 40;

                    int top  = dy + 100;
                    Point moveOffset = WorldBoundControl(new Point(left,top), ct.Width, ct.Height);
                    log.Info(moveOffset.ToString());
                    ct.Left = left - moveOffset.X;
                    ct.Top = top - moveOffset.Y;
                    ctWidths.Add(ct.Width);
                    ctHeight = ctHeight + ct.Height + 10;
                }
            }
        }
        private void ForamtSingleNode(List<int> nodes, int dx, int dy, List<ModelElement> modelElements)
        {
            int count = 0;
            this.ctWidths = new List<int>();
            foreach (ModelElement me in modelElements)
            {
                Control ct = me.GetControl;
                if (!nodes.Contains(me.ID))
                {
                    int left = dx + 60;
                    int top =  dy + 100;
                    Point moveOffset = WorldBoundControl(new Point(left, top), ct.Width, ct.Height);

                    ct.Left = left - moveOffset.X;
                    ct.Top = top - moveOffset.Y;

                    dx = dx + ct.Width;
                    count += 1;
                }

                if (count == 6)
                {
                    dy = dy + ct.Height;
                    count = 0;
                    dx = 0;
                }
            }
        }

        public void TreeGroup()
        {
            /*
             * 分组原则
             * 1.同一堆必有n个连接点，N》=1 以从右往左第一个为根
             * 2.1与2 有共同值 1与3 有共同值 如果1 和所有没有共同值 只存1; 
             */
            FindModelEndNodes();
            this.treeGroup = new List<List<List<int>>>();
            ht = new Hashtable();
            log.Info("叶子数" + this.leafNodeIds.Count.ToString());
            //遍历所有叶子
            foreach (int leafNode in this.leafNodeIds)
            {
                log.Info("leafNode：" + leafNode.ToString());
                this.treeNodes = new List<List<int>>();
                List<int> tmp = new List<int>();
                tmp.Add(leafNode);
                this.treeNodes.Add(tmp);
                SearchTree(tmp);
                this.treeGroup.Add(this.treeNodes);
            }
            log.Info("待合并数" + this.treeGroup.Count.ToString());
            //对堆取交集
            this.recordSearch = new List<List<List<int>>>();
            List<List<List<int>>> key = new List<List<List<int>>>();
            if (this.treeGroup.Count > 0)
            {
                this.recordSearch.Add(this.treeGroup[0]);
                key.Add(this.treeGroup[0]);
                ht.Add(key, this.treeGroup[0]);
            }


            for (int i = 0; i < this.treeGroup.Count; i++)
            {
                for (int j = i + 1; j < this.treeGroup.Count; j++)
                {
                    TreeComplete(this.treeGroup[i], this.treeGroup[j]);
                }
            }
            modelElements = Global.GetCurrentDocument().ModelElements;

            Global.GetCurrentDocument().MapOrigin = new System.Drawing.Point(0,0);
            int countDeep = 0;
            ctHeight = 0;
            List<int> countWidthList = new List<int>();
            List<int> leavelList = new List<int>();
            int count = 0;
            this.ctWidths = new List<int>();
            foreach (List<List<int>> tree in ht.Values)
            {

                tree.Reverse();
                foreach (List<int> leavel in tree)
                {
                    ctHeight = count;
                    countDeep = countDeep + 20;
                    if (this.ctWidths.Count != 0)
                    {
                        countDeep = countDeep + this.ctWidths.Max();
                        this.ctWidths = new List<int>();
                    }
                    foreach (int id in leavel)
                    {
                        FormatLoc(id, countDeep, ctHeight, modelElements);
                        leavelList.Add(id);
                    }
                    countWidthList.Add(ctHeight);       
                }
                count = countWidthList.Max();
                countDeep = 0;
                this.ctWidths = new List<int>(); 
            }
            //散元素沉底
            ForamtSingleNode(leavelList, 0, count, modelElements);
            this.currentModel.UpdateAllLines();
            Global.GetCanvasPanel().Invalidate();
            Global.GetNaviViewControl().UpdateNaviView();
        }
    }
}