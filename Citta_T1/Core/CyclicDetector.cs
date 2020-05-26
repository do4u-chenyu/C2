using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core
{
    class CyclicDetector
    {
        private List<int> vertices;
        private Dictionary<int, bool> visited;
        //private Dictionary<int, bool> recStack;
        private Dictionary<int, List<int>> graph;


        public CyclicDetector(ModelDocument doc, ModelRelation mr)
        {
            InitParams(doc, mr);
        }
        private void InitParams(ModelDocument doc, ModelRelation mr)
        {
            this.vertices = new List<int>();
            this.visited = new Dictionary<int, bool>();
            //this.recStack = new Dictionary<int, bool>();
            foreach (ModelElement me in doc.ModelElements)
            {
                int ID = me.ID;
                vertices.Add(ID);
                //visited[ID] = false;
                //recStack[ID] = false;
            }
            //this.graph = new Dictionary<int, List<int>>(doc.ModelLineDict);
            this.graph = InitGraph(doc.ModelLineDict);
            if (!this.graph.ContainsKey(mr.StartID))
                this.graph[mr.StartID] = new List<int>() { mr.EndID };
            else
                this.graph[mr.StartID].Add(mr.EndID);
        }

        private Dictionary<int, List<int>> InitGraph(Dictionary<int, List<int>> dict)
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();
            foreach (int k in dict.Keys)
            {
                result[k] = new List<int>(dict[k]);
            }
            return result;
        }
        //public bool IsCyclic()
        //{
        //    foreach (int vertex in this.vertices)
        //    {
        //        if (!this.visited[vertex] && IsCyclic(vertex, this.visited, this.recStack))
        //            return true;
        //    }
        //    return false;
        //}
        //private bool IsCyclic(int v, Dictionary<int, bool> vst, Dictionary<int, bool> rs)
        //{
        //    vst[v] = true;
        //    rs[v] = true;
        //    if (this.graph.ContainsKey(v))
        //    {
        //        foreach (int neighbour in this.graph[v])
        //        {
        //            if ((!vst[neighbour] && IsCyclic(neighbour, vst, rs)) || rs[neighbour])
        //                return true;
        //        }
        //    }
        //    rs[v] = false;
        //    return false;
        //}

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

