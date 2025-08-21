using GraphProject.Visualization;
using UnityEngine;

namespace GraphProject.Tools.States
{
    public class EdgeCreationState : ICreationState
    {
        private readonly GraphContainer _graphContainer;
        private readonly GraphPartSelector _partSelector;

        public EdgeCreationState(GraphContainer container, GraphPartSelector selector)
        {
            _partSelector = selector;
            _graphContainer = container;
        }

        public void Enter() { }

        public void OnAction(Vector2 position)
        {
            if (_partSelector.SelectedVertices.Count >= 2 || _partSelector.SelectedEdges.Count == 1)
            {
                _partSelector.DeselectVertices();
                _partSelector.DeselectEdges();
            }

            _partSelector.SelectVertex(position);
            if (_partSelector.SelectedVertices.Count >= 2)
            {
                var edge = _graphContainer.CreateEdge(_partSelector.SelectedVertices[0], _partSelector.SelectedVertices[1]);
                _partSelector.SelectEdge(edge);
            }
        }

        public void Exit()
        {
            _partSelector.DeselectVertices();
            _partSelector.DeselectEdges();
        }
    }
}