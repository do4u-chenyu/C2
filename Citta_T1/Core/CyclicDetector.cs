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
        private Dictionary<int, bool> recStack;
        private Dictionary<int, List<int>> graph;


        public CyclicDetector(ModelDocument doc)
        {
            InitParams(doc);
        }
        private void InitParams(ModelDocument doc)
        {
            this.vertices = new List<int>();
            this.visited = new Dictionary<int, bool>();
            this.recStack = new Dictionary<int, bool>();
            foreach (ModelElement me in doc.ModelElements)
            {
                int ID = me.ID;
                vertices.Add(ID);
                visited[ID] = false;
                recStack[ID] = false;
            }
            this.graph = doc.ModelLineDict;
        }
        public bool IsCyclic()
        {
            foreach (int vertex in this.vertices)
            {
                if (!this.visited[vertex] && _IsCyclic(vertex, this.visited, this.recStack))
                    return true;
            }
            return false;
        }
        private bool _IsCyclic(int v, Dictionary<int, bool> vst, Dictionary<int, bool> rs)
        {
            this.visited[v] = true;
            this.recStack[v] = true;
            if (this.graph.ContainsKey(v))
            {
                foreach (int neighbour in this.graph[v])
                {
                    if ((!this.visited[neighbour] && _IsCyclic(neighbour, this.visited, this.recStack)) || recStack[neighbour])
                        return true;
                }
            }
            this.recStack[v] = false;
            return false;
        }

    }       

}
//int public bool isCyclic(ModelRelation mr)
//{
//    int relationNum = this.modelRelations.Count;
//    Dictionary<int, bool> visited = new Dictionary<int, bool> { };
//    Dictionary<int, bool> recStack = new Dictionary<int, bool> { };
//    List<int> vertices = new List<int> { };
//    foreach (ModelRelation _mr in this.modelRelations)
//    {
//        int startVertex = _mr.StartID;
//        int endVertex = _mr.EndID;
//        visited[startVertex] = false;
//        recStack[startVertex] = false;
//        visited[endVertex] = false;
//        recStack[endVertex] = false;
//        if (!vertices.Contains(startVertex))
//            vertices.Add(startVertex);
//        if (!vertices.Contains(endVertex))
//            vertices.Add(endVertex);
//    }
//    foreach (int vertex in vertices)
//    {
//        if (!visited[vertex] && _isCyclic(vertex, visited, recStack))
//            return true;
//    }
//    return false;
//}
//private bool _isCyclic(int vertex, Dictionary<int, bool> visited, Dictionary<int, bool> recStack)
//{
//    visited[vertex] = true;
//    recStack[vertex] = true;
//    foreach ()
//        }
