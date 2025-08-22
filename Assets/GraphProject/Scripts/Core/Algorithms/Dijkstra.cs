using System.Collections.Generic;
using GraphProject.Core.Data;

namespace GraphProject.Core.Algorithms
{
    public class Dijkstra
    {
        private List<VertexData> _vertexData;

        public List<int> FindShortestPath(Graph graph, int startID, int finishID)
        {
            if(graph.Vertices.TryGetValue(startID, out var startVertex) && graph.Vertices.TryGetValue(finishID, out var endVertex))
            {
                return FindShortestPath(graph, startVertex, endVertex);
            }
            return new List<int>();
        }

        public List<int> FindShortestPath(Graph graph, Vertex startVertex, Vertex finishVertex)
        {
            InitData(graph);
            VertexData first = GetVertexData(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                VertexData current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }
                SetSumToNextVertex(current);
            }
            return GetPath(startVertex, finishVertex);
        }

        private void InitData(Graph graph)
        {
            _vertexData = new List<VertexData>(graph.Vertices.Count);

            foreach (Vertex vertex in graph.Vertices.Values)
            {
                _vertexData.Add(new VertexData(vertex));
            }
        }

        private VertexData FindUnvisitedVertexWithMinSum()
        {
            float minValue = float.MaxValue;
            VertexData minVertexInfo = null;

            foreach (VertexData data in _vertexData)
            {
                if (data.IsUnvisited && data.EdgesWeightSum < minValue)
                {
                    minVertexInfo = data;
                    minValue = data.EdgesWeightSum;
                }
            }
            return minVertexInfo;
        }

        private void SetSumToNextVertex(VertexData data)
        {
            data.IsUnvisited = false;
            foreach (Edge edge in data.Vertex.Edges)
            {
                var nextData = GetVertexData(edge.ConnectedVertex);
                var sum = data.EdgesWeightSum + edge.Weight;
                if (sum < nextData.EdgesWeightSum)
                {
                    nextData.EdgesWeightSum = sum;
                    nextData.PreviousVertex = data.Vertex;
                }
            }
        }

        private List<int> GetPath(Vertex startVertex, Vertex endVertex)
        {
            List<int> path = new() { endVertex.ID };

            while (startVertex != endVertex)
            {
                endVertex = GetVertexData(endVertex).PreviousVertex;
                if (endVertex == null)
                    break;

                path.Add(endVertex.ID);
            }
            return path;
        }

        private VertexData GetVertexData(Vertex vertex)
        {
            foreach (VertexData data in _vertexData)
            {
                if (data.Vertex.Equals(vertex))
                {
                    return data;
                }
            }
            return null;
        }
    }
}

