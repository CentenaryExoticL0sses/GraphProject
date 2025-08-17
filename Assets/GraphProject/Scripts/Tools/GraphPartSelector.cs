using System.Collections.Generic;
using UnityEngine;

namespace GraphProject.Tools
{

    //�����, ���������� �� ��������� ����� �����
    public class GraphPartSelector
    {
        public IReadOnlyList<VertexDisplayObject> SelectedVertices => _selectedVertices;
        public IReadOnlyList<EdgeDisplayObject> SelectedEdges => _selectedEdges;

        private List<VertexDisplayObject> _selectedVertices;
        private List<EdgeDisplayObject> _selectedEdges;

        public GraphPartSelector()
        {
            _selectedVertices = new List<VertexDisplayObject>();
            _selectedEdges = new List<EdgeDisplayObject>();
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

        //��������� �������
        public void SelectVertex(Vector2 position)
        {
            var vertex = GetVertexAtPosition(position);
            SelectVertex(vertex);
        }

        public void SelectVertex(VertexDisplayObject vertex)
        {
            if (vertex != null)
            {
                vertex.Select();
                _selectedVertices.Add(vertex);
            }
        }

        //��������� �����
        public void SelectEdge(EdgeDisplayObject edge)
        {
            if (edge != null)
            {
                edge.Select();
                _selectedEdges.Add(edge);
            }
        }

        //����� ���� ���������� ������
        public void DeselectVertices()
        {
            foreach (var vertex in _selectedVertices)
            {
                vertex.Deselect();
            }
            _selectedVertices.Clear();
        }

        //����� ���� ���������� ����
        public void DeselectEdges()
        {
            foreach (var edge in _selectedEdges)
            {
                edge.Deselect();
            }
            _selectedEdges.Clear();
        }
    }
}