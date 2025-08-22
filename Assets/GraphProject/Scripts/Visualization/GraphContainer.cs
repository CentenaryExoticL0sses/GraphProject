using UnityEngine;
using System.Collections.Generic;
using GraphProject.Core.Data;
using GraphProject.Core.Algorithms;

namespace GraphProject.Visualization
{
    public class GraphContainer : MonoBehaviour
    {
        [Header("������� ��������")]
        [SerializeField] private VertexDisplayObject _vertexPrefab;
        [SerializeField] private EdgeDisplayObject _edgePrefab;

        public IReadOnlyList<VertexDisplayObject> Vertices => _vertices;
        public IReadOnlyList<EdgeDisplayObject> Edges => _edges;

        private Graph _graph;
        private List<VertexDisplayObject> _vertices;
        private List<EdgeDisplayObject> _edges;

        public void Initialize(Graph graph)
        {
            _graph = graph;
            _vertices = new List<VertexDisplayObject>();
            _edges = new List<EdgeDisplayObject>();
        }

        public void LoadGraph(List<VertexObjectData> vertices, List<EdgeObjectData> edges)
        {
            foreach (var vertex in vertices)
            {
                CreateVertex(vertex);
            }
            foreach (var edge in edges)
            {
                CreateEdge(edge.FirstVertexID, edge.SecondVertexID);
            }
        }

        /// <summary>
        /// �������� ����� ������� �����
        /// </summary>
        /// <param name="position">���������� ����� �������.</param>
        /// <returns>������������� ������� �����.</returns>
        public VertexDisplayObject CreateVertex(Vector2 position)
        {
            int id = _graph.Vertices.Count;
            var data = new VertexObjectData(id, position);
            return CreateVertex(data);
        }

        public VertexDisplayObject CreateVertex(VertexObjectData data)
        {
            _graph.AddVertex(data.ID);

            var newVertex = Instantiate(_vertexPrefab, data.Position, Quaternion.identity, transform);
            newVertex.Initialize(data);
            _vertices.Add(newVertex);
            return newVertex;
        }

        /// <summary>
        /// �������� ������ ����� �����
        /// </summary>
        /// <param name="firstID">���� ������ �������.</param>
        /// <param name="secondID">���� ������ �������.</param>
        /// <returns>������������� ����� �����.</returns>
        public EdgeDisplayObject CreateEdge(int firstID, int secondID)
        {
            var firstVertex = GetVertexObject(firstID);
            var secondVertex = GetVertexObject(secondID);
            return CreateEdge(firstVertex, secondVertex);
        }

        public EdgeDisplayObject CreateEdge(VertexDisplayObject firstVertex, VertexDisplayObject secondVertex)
        {
            if (firstVertex != null && secondVertex != null)
            {
                var existingEdge = GetEdgeObject(firstVertex.Data.ID, secondVertex.Data.ID);
                if (existingEdge != null)
                {
                    return existingEdge;
                }

                float weight = Vector2.Distance(firstVertex.Data.Position, secondVertex.Data.Position);
                var data = new EdgeObjectData(firstVertex.Data.ID, secondVertex.Data.ID);
                _graph.AddEdge(firstVertex.Data.ID, secondVertex.Data.ID, weight);

                var newEdge = Instantiate(_edgePrefab, transform);
                newEdge.Initialize(data, weight, firstVertex.Data.Position, secondVertex.Data.Position);
                _edges.Add(newEdge);
                return newEdge;
            }
            return null;
        }

        /// <summary>
        /// ����� ����������� ����
        /// </summary>
        /// <param name="firstID">������ ����.</param>
        /// <param name="secondID">����� ����.</param>
        /// <returns></returns>
        public List<int> FindShortestPath(int firstID, int secondID)
        {
            Dijkstra dijkstra = new();
            return dijkstra.FindShortestPath(_graph, firstID, secondID);
        }

        /// <summary>
        /// ������� �����, �������� ���� ������ � ����
        /// </summary>
        public void ClearGraph()
        {
            foreach (var edge in _edges)
            {
                Destroy(edge.gameObject);
            }
            foreach (var vertex in _vertices)
            {
                Destroy(vertex.gameObject);
            }
            _vertices.Clear();
            _edges.Clear();
            _graph = new Graph();
        }

        /// <summary>
        /// ����� ������� �� � ID
        /// </summary>
        /// <param name="id">ID �������.</param>
        /// <returns>���������� ������������� �������.</returns>
        public VertexDisplayObject GetVertexObject(int id)
        {
            return _vertices.Find(vertex => vertex.Data.ID == id);
        }

        /// <summary>
        /// ����� ����� �� ID ��������� ������
        /// </summary>
        /// <param name="firstID">ID ��������� �������.</param>
        /// <param name="secondID">ID �������� �������.</param>
        /// <returns></returns>
        public EdgeDisplayObject GetEdgeObject(int firstID, int secondID)
        {
            var edge = _edges.Find(edge => edge.Data.FirstVertexID == firstID && edge.Data.SecondVertexID == secondID);
            if (edge == null)
            {
                edge = _edges.Find(edge => edge.Data.FirstVertexID == secondID && edge.Data.SecondVertexID == firstID);
            }
            return edge;
        }
    }
}