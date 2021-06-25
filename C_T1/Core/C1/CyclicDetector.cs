using C2.Business.Model;
using System.Collections.Generic;

namespace C2.Core
{
    class CyclicDetector
    {
        private List<int> vertices;
        private Dictionary<int, bool> visited;
        private Dictionary<int, List<int>> graph;

        public CyclicDetector(ModelDocument doc, ModelRelation mr)
        {
            InitParams(doc, mr);
        }
        private void InitParams(ModelDocument doc, ModelRelation mr)
        {
            this.vertices = new List<int>();
            this.visited = new Dictionary<int, bool>();
            doc.ModelElements.ForEach(me => vertices.Add(me.ID));
            InitGraph(doc, mr);
        }

        private void InitGraph(ModelDocument doc, ModelRelation mr)
        {   // 深度复制文档的图关系,同时添加最新的mr关系
            this.graph = new Dictionary<int, List<int>>();
            foreach (int k in doc.ModelGraphDict.Keys)
                this.graph[k] = new List<int>(doc.ModelGraphDict[k]);

            if (!this.graph.ContainsKey(mr.StartID))
                this.graph[mr.StartID] = new List<int>() { mr.EndID };
            else
                this.graph[mr.StartID].Add(mr.EndID);
        }

        public bool IsCyclic()
        {   // 针对图中每一个定点做一次环检测
            foreach (int vertex in vertices)
                if (IsCyclic(vertex))
                    return true;
            return false;
        }

        private bool IsCyclic(int vertex)
        {
            // 访问过该节点， 成环
            if (visited.ContainsKey(vertex) && visited[vertex])
                return true;
            // 标记访问
            visited[vertex] = true;
            // 访问到一个叶子节点，没有子节点，直接返回
            if (!this.graph.ContainsKey(vertex))
                return visited[vertex] = false;  // 每个DSF过程到叶子节点后，vistited对应退栈置false

            // 访问每一个子节点
            foreach (int child in this.graph[vertex])
                if (IsCyclic(child))
                    return true;
            //所有节点访问完毕，退栈
            return visited[vertex] = false;
        }
    }
}

