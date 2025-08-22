using GraphProject.Visualization;
using UnityEngine;

namespace GraphProject.Tools.States
{
    public class EdgeRemovalState : ICreationState
    {
        private readonly GraphContainer _graphContainer;
        private readonly GraphPartSelector _partSelector;

        public EdgeRemovalState(GraphContainer graphContainer, GraphPartSelector partSelector)
        {
            _graphContainer = graphContainer;
            _partSelector = partSelector;
        }

        public void Enter()
        {

        }

        public void OnAction(Vector2 position)
        {

        }

        public void Exit()
        {

        }
    }
}