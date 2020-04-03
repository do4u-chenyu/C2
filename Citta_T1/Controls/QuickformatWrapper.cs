
using Citta_T1.Business.Model;
using System;
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
            modelElements = this.currentModel.ModelElements;
            modelRelations = this.currentModel.ModelRelations;
            this.leafNodeIds = new List<int>();
            this.starNodes = new List<int>();
            this.endNodes  = new List<int>();
            foreach (ModelRelation mr in modelRelations)
            {
                this.starNodes.Add(mr.StartID);
                this.endNodes.Add(mr.EndID);
            }
            this.leafNodeIds = endNodes.Except(starNodes).ToList();
        }
        private void SearchNode(List<int> needSearchNodeIds)
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
                List<int> dataNodeIds = FindBeforeNodeIds(resultNodeId);
            }
            SearchNode(nextNeedSearchNodeIds);
        }




        private void TreeGroup()
        {

        }



    }
}
