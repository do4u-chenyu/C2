
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
        private List<int> starNodes;
        private List<int> endNodes;
        
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

        public void FindModelEndNodes()
        {
            //寻找只有入度没有出度的叶子节点
            modelElements = this.currentModel.ModelElements;
            modelRelations = this.currentModel.ModelRelations;
            List<int> leafNodeIds = new List<int>();           
            starNodes = new List<int>();
            endNodes = new List<int>();

            foreach (ModelRelation mr in modelRelations)
            {
                starNodes.Add(mr.Start);
                endNodes.Add(mr.End);
            }
            leafNodeIds = endNodes.Except(starNodes).ToList();
        }
        




    }
}
