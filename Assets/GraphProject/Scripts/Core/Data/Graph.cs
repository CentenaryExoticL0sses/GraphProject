using System.Collections.Generic;

namespace GraphProject.Core.Data
{
    public class Graph
    {
        public IReadOnlyDictionary<int, Vertex> Vertices => _vertices;

        private readonly Dictionary<int, Vertex> _vertices;

        public Graph()
        {
            _vertices = new Dictionary<int, Vertex>();
        }

        public bool AddVertex(int id)
        {
            Vertex newVertex = new(id);
            return _vertices.TryAdd(id, newVertex);
        }

        public bool RemoveVertex(int id)
        {
            return _vertices.Remove(id);
        }

        public void AddEdge(int firstID, int secondID, float weight)
        {
            if(_vertices.TryGetValue(firstID, out Vertex firstVertex) && 
                _vertices.TryGetValue(secondID, out Vertex secondVertex))
            {
                firstVertex.AddEdge(secondVertex, weight);
                secondVertex.AddEdge(firstVertex, weight);
            }
        }
    }
}
