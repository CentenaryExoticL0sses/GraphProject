using GraphProject.Visualization;
using System.Collections.Generic;
using UnityEngine;

namespace GraphProject.Tools
{
    /// <summary>
    /// �����, ���������� �� ��������� ����� �����
    /// </summary>
    public class GraphPartSelector
    {
        public IReadOnlyList<int> SelectedVertices => _selectedVertices;
        public IReadOnlyList<(int, int)> SelectedEdges => _selectedEdges;

        private readonly GraphContainer _graphContainer;

        private readonly List<int> _selectedVertices;
        private readonly List<(int, int)> _selectedEdges;

        public GraphPartSelector(GraphContainer container)
        {
            _graphContainer = container;
            _selectedVertices = new List<int>();
            _selectedEdges = new List<(int, int)>();
        }

        //��������� ������� �� ������� �� ��
        private VertexDisplayObject GetVertexAtPosition(Vector2 position)
        {
            var hit = Physics2D.Raycast(position, Vector2.zero);
            if (hit.collider != null)
            {
                VertexDisplayObject vertex;
                if (hit.collider.TryGetComponent(out vertex))
                {
                    return vertex;
                }
            }
            return null;
        }

        /// <summary>
        /// ��������� ������� � �������.
        /// </summary>
        /// <param name="position">������� �� ������.</param>
        public void SelectVertex(Vector2 position)
        {
            var vertex = GetVertexAtPosition(position);
            SelectVertex(vertex);
        }

        /// <summary>
        /// ��������� �������.
        /// </summary>
        /// <param name="vertex">���������� �������.</param>
        public void SelectVertex(VertexDisplayObject vertex)
        {
            if (vertex != null)
            {
                vertex.Select();
                _selectedVertices.Add(vertex.Data.ID);
            }
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        /// <param name="edge">���������� �����.</param>
        public void SelectEdge(EdgeDisplayObject edge)
        {
            if (edge != null)
            {
                edge.Select();
                _selectedEdges.Add((edge.Data.FirstVertexID, edge.Data.SecondVertexID));
            }
        }

        /// <summary>
        /// ����� ���� ���������� ������.
        /// </summary>
        public void DeselectVertices()
        {
            foreach (var vertexId in _selectedVertices)
            {
                VertexDisplayObject vertex = _graphContainer.GetVertexObject(vertexId);
                if (vertex) vertex.Deselect();
            }
            _selectedVertices.Clear();
        }

        /// <summary>
        /// ����� ���� ���������� ����.
        /// </summary>
        public void DeselectEdges()
        {
            foreach ((int first, int second) in _selectedEdges)
            {
                EdgeDisplayObject edge = _graphContainer.GetEdgeObject(first, second);
                if(edge) edge.Deselect();
            }
            _selectedEdges.Clear();
        }
    }
}