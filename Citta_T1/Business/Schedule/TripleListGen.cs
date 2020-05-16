using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule
{
    /// <summary>
    /// 根据前台保存模型生成一个三元组列表
    /// </summary>

    class TripleListGen
    {
        private List<Triple> currentModelTripleList;
        private ModelDocument currentModel;
        private string state;
        private ModelElement stopElement;

        private List<ModelElement> modelElements;
        private List<ModelRelation> modelRelations;
        private List<int> starNodes;
        private List<int> endNodes;
        private List<int> leafNodeIds;

        //已经找过的节点，如果在里面，不需要再找了
        private List<int> haveSearchedNodes;

        public TripleListGen(ModelDocument currentModel)
        {
            //“运行”构造方法
            this.currentModel = currentModel;
            this.state = "all";
            this.haveSearchedNodes = new List<int>();
            this.currentModelTripleList = new List<Triple>();
        }

        public TripleListGen(ModelDocument currentModel, ModelElement stopElement)
        {
            //“运行到此”构造方法,需要传入运行到此的算子（id或者模型元素类实例均可）
            this.currentModel = currentModel;
            this.stopElement = stopElement;
            this.state = "mid";
            this.haveSearchedNodes = new List<int>();
            this.currentModelTripleList = new List<Triple>();
        }

        public int AllOperatorNotReadyNum()
        {
            int count = 0;
            foreach (ModelElement op in this.currentModel.ModelElements.FindAll(c => c.Type == ElementType.Operator))
            {
                if (op.Status == ElementStatus.Null)
                {
                    count++;
                }
            }
            return count;

        }


        public void GenerateList()
        {
            //寻找当前模型的所有叶子节点
            FindModelEndNodes();

            //从叶子节点开始找
            SearchNewTriple(leafNodeIds);

            //将数据项type均为datasource的置顶
            TopDataOnlyTriple();

        }




        public void FindModelEndNodes()
        {
            modelElements = this.currentModel.ModelElements;
            modelRelations = this.currentModel.ModelRelations;

            //叶子节点列表
            leafNodeIds = new List<int>();

            starNodes = new List<int>();
            endNodes = new List<int>();

            foreach (ModelRelation mr in modelRelations)
            {
                starNodes.Add(mr.StartID);
                endNodes.Add(mr.EndID);
            }

            if (this.state == "all")
            {
                //从“运行”按钮进入,找到模型的最后一个元素
                //DONE
                //结束元素可能有多个,需要判断每个元素的出度 
                leafNodeIds = endNodes.Except(starNodes).ToList();
            }
            else if (this.state == "mid")
            {
                //从“运行到此”右键选项进入
                leafNodeIds.Add(FindNextNodeId(this.stopElement.ID));
            }
        }

        public List<int> FindBeforeNodeIds(int id)
        {
            List<int> beforeNodeId = new List<int>();
            Dictionary<int, int> nodeIdPinDict = new Dictionary<int, int>();

            foreach (ModelRelation beforeNode in modelRelations.FindAll(c => c.EndID == id))
            {
                nodeIdPinDict.Add(beforeNode.EndPin, beforeNode.StartID);
            }

            for(int i = 0;i<nodeIdPinDict.Count;i++)
                beforeNodeId.Add(nodeIdPinDict[i]);

            return beforeNodeId;
        }

        public int FindNextNodeId(int id)
        {
            return modelRelations.Find(c => c.StartID == id).EndID;
        }



        public void SearchNewTriple(List<int> needSearchNodeIds)
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
                 * 2、通过relation找到它对应的start，可能有1个，可能有2个【我一次直接溯2层吧，算子不可能出现在待溯源的列表里
                 * 3、找到算子（必定1个），结果\数据（1或多），new 一个triple
                 * 4、结果\数据 判断是否存在在 haveSearchedNodes, 不在则加入needSearchNodes
                 */

                if (!endNodes.Exists(c => c == resultNodeId))
                {
                    continue;
                }

                int operateNodeId = FindBeforeNodeIds(resultNodeId).First();
                List<int> dataNodeIds = FindBeforeNodeIds(operateNodeId);

                ModelElement resultElement = modelElements.Find(c => c.ID == resultNodeId);
                ModelElement operateElement = modelElements.Find(c => c.ID == operateNodeId);
                List<ModelElement> dataElements = new List<ModelElement>();
                foreach (int dataNodeId in dataNodeIds)
                {
                    dataElements.Add(modelElements.Find(c => c.ID == dataNodeId));

                    if (!this.haveSearchedNodes.Exists(c => c == dataNodeId))
                    {
                        if (!nextNeedSearchNodeIds.Exists(c => c == dataNodeId))
                        {
                            nextNeedSearchNodeIds.Add(dataNodeId);
                        }
                        this.haveSearchedNodes.Add(dataNodeId);

                    }
                }

                this.currentModelTripleList.Add(new Triple(dataElements, operateElement, resultElement));

            }


            SearchNewTriple(nextNeedSearchNodeIds);
        }



        public void TopDataOnlyTriple()
        {


            List<Triple> tmpDataTriple = new List<Triple>();
            List<Triple> tmpContainResultTriple = new List<Triple>();

            foreach (Triple tmpTri in this.currentModelTripleList)
            {
                bool hasResultData = false;
                foreach (ModelElement dataElement in tmpTri.DataElements)
                {
                    if (dataElement.Type == ElementType.Result)
                    {
                        hasResultData = true;
                        break;
                    }
                }
                if (hasResultData)
                {
                    tmpContainResultTriple.Add(tmpTri);
                }
                else
                {
                    tmpDataTriple.Add(tmpTri);
                }
            }

            tmpContainResultTriple.Reverse();
            this.currentModelTripleList = tmpDataTriple.Concat(tmpContainResultTriple).ToList();
        }



        public List<Triple> CurrentModelTripleList { get => currentModelTripleList; set => currentModelTripleList = value; }
    }
}
