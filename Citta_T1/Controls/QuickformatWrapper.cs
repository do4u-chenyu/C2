
using Citta_T1.Business.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Controls
{
    class QuickformatWrapper
    {
        private List<ModelRelation> modelRelations;
        private ModelDocument currentModel;
        private List<ModelElement> modelElements;

        private List<int> leafNodeIds;
        private List<int> starNodes;
        private List<int> endNodes ;
        private List<List<int>> treeNodes;
        private List<List<List<int>>> treeGroup;
        private Hashtable ht ;
        private List<List<List<int>>> recordSearch; 
        public QuickformatWrapper(ModelDocument currentModel)
        {
            this.currentModel = currentModel;
        }

        private List<int> FindBeforeNodeIds(int id)
        {
            List<int> beforeNodeId = new List<int>();
            foreach (ModelRelation beforeNode in modelRelations.FindAll(c => c.End == id))
            {
                beforeNodeId.Add(beforeNode.Start);
            }
            return beforeNodeId;
        }

        private void FindModelEndNodes()
        {
            //寻找只有入度没有出度的叶子节点
            modelElements = this.currentModel.ModelElements;
            modelRelations = this.currentModel.ModelRelations;
            this.leafNodeIds = new List<int>();
            this.starNodes = new List<int>();
            this.endNodes  = new List<int>();
            foreach (ModelRelation mr in modelRelations)
            {
                this.starNodes.Add(mr.Start);
                this.endNodes.Add(mr.End);
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
                if (!this.endNodes.Exists(c => c == resultNodeId))
                {
                    continue;
                }
                nextNeedSearchNodeIds.Union(FindBeforeNodeIds(resultNodeId));
            }
            this.treeNodes.Add(nextNeedSearchNodeIds);
            SearchTree(nextNeedSearchNodeIds);
        }
        private List<List<int>> TreeDeepSort(List<List<int>> treeA, List<List<int>> treeB)
        {
            List<List<int>> results = new List<List<int>>();
            for (int i=0;i < treeA.Count;i++)
            {
                List<int> result = treeB[treeB.Count - i - 1].Union(treeA[treeA.Count - i - 1]).ToList();
                results.Add(result);
            }
            return results;
        }
        

        private void TreeComplete(List<List<int>> treeA, List<List<int>> treeB)
        {
            
            
            List<List<int>> commonList = treeA.Intersect(treeB).ToList();
            List<List<List<int>>> key = new List<List<List<int>>>();
            
            if (!this.recordSearch.Contains(treeA))
            {
                this.recordSearch.Add(treeA);
                key.Add(treeA);
                ht.Add(key, treeA);
                
            }
            
            if (commonList.Count == 0)
                return;
            foreach(List<List<List<int>>> tmp in ht.Keys)
            {
                
            }
            key.Add(treeA);
            key.Add(treeB);
            if (treeA.Count < treeB.Count)
                ht.Add(key, TreeDeepSort(treeA, treeB));
            if (treeA.Count < treeB.Count)
                ht.Add(key, TreeDeepSort(treeB, treeA));
        }


        private void TreeGroup()
        {
            /*
             * 分组原则
             * 1.同一堆必有n个连接点，N》=1 以从右往左第一个为根
             * 2.1与2 有共同值 1与3 有共同值 如果1 和所有没有共同值 只存1; 
             */
            FindModelEndNodes();
            ht = new Hashtable();
            //遍历所有叶子
            foreach (int leafNode in this.leafNodeIds)
            {
                this.treeNodes = new List<List<int>>();
                List<int> tmp = new List<int>();
                tmp.Add(leafNode);
                this.treeNodes.Add(tmp);
                SearchTree(tmp);
                this.treeGroup.Add(this.treeNodes);
            }
            //对堆取交集
            this.recordSearch = new List<List<List<int>>>();
            for (int i=0;i< this.treeGroup.Count;i++)
            {
                for (int j= i+1;j< this.treeGroup.Count;j++)
                {
                    TreeComplete(this.treeGroup[i], this.treeGroup[j]);
                }
            }
        }



    }
}
