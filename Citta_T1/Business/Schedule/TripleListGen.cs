using C2.Business.Model;
using System.Collections.Generic;
using System.Linq;

namespace C2.Business.Schedule
{
    /// <summary>
    /// 三元组生成类
    /// </summary>

    public class TripleListGen
    {
        private List<Triple> currentModelTripleList;
        private ModelDocument currentModel;
        private string state;
        private ModelElement stopElement;
        private List<int> haveSearchedNodes;//已经找过的节点，如果在里面，不需要再找了
        //双输入的op算子集合，保证存triple的时候数据源个数正确
        private List<ElementSubType> doubleSubType = new List<ElementSubType> { ElementSubType.CustomOperator2, ElementSubType.CollideOperator, ElementSubType.DifferOperator, ElementSubType.RelateOperator, ElementSubType.UnionOperator, ElementSubType.KeywordOperator };
        public List<Triple> CurrentModelTripleList { get => currentModelTripleList; set => currentModelTripleList = value; }

        public TripleListGen(ModelDocument currentModel,string state,ModelElement stopElement)
        {
            this.currentModel = currentModel;
            this.stopElement = stopElement;
            this.state = state;//“运行”构造方法status为all,stopElement为null.“运行到此”构造方法status为mid,stopElement为指定模型元素
            this.haveSearchedNodes = new List<int>();
            this.currentModelTripleList = new List<Triple>();
        }
        public void GenerateList()
        {
            SearchNewTriple(FindModelLeafNodeIds());//寻找当前模型的所有叶子节点的id,从叶子节点开始生成三元组列表
            TopDataOnlyTriple(); //将数据项type均为datasource的置顶
            TopologicalSort(); //生成拓扑序列,每个三元组能够保证所有DE都已Done，从而使整个调度顺序进行
        }

        private List<int> FindModelLeafNodeIds()
        {
            List<int> leafNodeIds = new List<int>();//叶子节点列表
            List<int> starNodes = new List<int>();
            List<int> endNodes = new List<int>();
            this.currentModel.ModelRelations.ForEach(mr => { starNodes.Add(mr.StartID);endNodes.Add(mr.EndID); });

            //从“运行”按钮进入,找到当前模型所有的叶子节点（存在多个），仅在边关系的end出现，没有在start出现，说明这些节点没有出度，即为叶子节点
            //从“运行到此”右键选项进入，stopElement固定为op算子，需要先找它的rs算子作为叶子节点
            if (this.state == "all")
                leafNodeIds = endNodes.Except(starNodes).ToList();
            else
                leafNodeIds.Add(this.currentModel.ModelRelations.Find(c => c.StartID == this.stopElement.ID).EndID);
            return leafNodeIds;
        }

        private void SearchNewTriple(List<int> needSearchNodeIds)
        {
            List<int> nextNeedSearchNodeIds = new List<int>();//下一次需要找上游的点
            if (needSearchNodeIds.Count == 0)
                return;
            List<int> endNodes = new List<int>();
            this.currentModel.ModelRelations.ForEach(mr => endNodes.Add(mr.EndID));

            foreach (int resultNodeId in needSearchNodeIds)
            {
                ModelElement resultElement = this.currentModel.ModelElements.Find(c => c.ID == resultNodeId);
                if (resultElement == null || resultElement.Type != ElementType.Result || !endNodes.Exists(c => c == resultNodeId)) //不是结果算子或者没有上游，该节点没有对应三元组
                    continue;

                int operateNodeId = FindBeforeNodeIds(resultNodeId).First();
                ModelElement operateElement = this.currentModel.ModelElements.Find(c => c.ID == operateNodeId);
                if (operateElement == null || operateElement.Type != ElementType.Operator || !endNodes.Exists(c => c == operateNodeId)) //不是op算子或者没有上游，该节点没有对应三元组
                    continue;

                List<int> dataNodeIds = FindBeforeNodeIds(operateNodeId);
                List<ModelElement> dataElements = new List<ModelElement>();
                foreach (int dataNodeId in dataNodeIds)
                {
                    ModelElement dataElement = this.currentModel.ModelElements.Find(c => c.ID == dataNodeId);
                    if (dataElement == null || dataElement.Type == ElementType.Operator) //不是dt算子,找不到关系对应id,该节点没有对应三元组
                        continue;
                    dataElements.Add(dataElement);
                    if (!this.haveSearchedNodes.Exists(c => c == dataNodeId)) //结果\数据 判断是否存在在 haveSearchedNodes, 不在则加入needSearchNodes
                    {
                        nextNeedSearchNodeIds.Add(dataNodeId);
                        this.haveSearchedNodes.Add(dataNodeId);
                    }
                }

                //数据源不为空的个数满足op算子引脚个数，才加入
                if(dataElements.Count.Equals(doubleSubType.Contains(operateElement.SubType)? 2 : 1))
                    this.currentModelTripleList.Add(new Triple(dataElements, operateElement, resultElement));
            }
            SearchNewTriple(nextNeedSearchNodeIds);
        }

        private void TopDataOnlyTriple()
        {
            List<Triple> tmpDataTriple = new List<Triple>();
            List<Triple> tmpContainResultTriple = new List<Triple>();
            foreach (Triple tmpTri in this.currentModelTripleList)
            {
                if (tmpTri.DataElements.Exists(me => me.Type == ElementType.Result))
                    tmpContainResultTriple.Add(tmpTri);
                else
                    tmpDataTriple.Add(tmpTri);
            }
            tmpContainResultTriple.Reverse();//生成triple时从叶子往根，运行时从根往叶子
            this.currentModelTripleList = tmpDataTriple.Concat(tmpContainResultTriple).ToList();
        }

        private void TopologicalSort()
        {
            for (int i = 0; i < CurrentModelTripleList.Count; i++)
                while (TopologicalOrder(i)); // 对每一个元素调整拓扑顺序,直到不存在拓扑冲突为止
        }

        private bool TopologicalOrder(int i)
        {
            foreach (var de in CurrentModelTripleList[i].DataElements)
            {
                int j = CurrentModelTripleList.FindIndex(me => me.ResultElement.ID == de.ID);
                if (j > i) // 存在拓扑冲突:后续元素的输出是当前的输入元素,调换位置
                {
                    // Switch i 和 j 2个元素位置
                    Triple tmp = CurrentModelTripleList[i];
                    CurrentModelTripleList[i] = CurrentModelTripleList[j];
                    CurrentModelTripleList[j] = tmp;
                    return true;
                }     
            }
            return false;
        }

        private List<int> FindBeforeNodeIds(int id)
        {
            List<int> beforeNodeId = new List<int>();
            Dictionary<int, int> nodeIdPinDict = new Dictionary<int, int>();

            foreach (ModelRelation beforeNode in this.currentModel.ModelRelations.FindAll(c => c.EndID == id))
            {
                if(!nodeIdPinDict.ContainsKey(beforeNode.EndPin))
                    nodeIdPinDict.Add(beforeNode.EndPin, beforeNode.StartID);
            }

            for (int i = 0; i < nodeIdPinDict.Count; i++)
            {
                if (nodeIdPinDict.ContainsKey(i))
                    beforeNodeId.Add(nodeIdPinDict[i]);
            }

            return beforeNodeId;
        }

    }
}
