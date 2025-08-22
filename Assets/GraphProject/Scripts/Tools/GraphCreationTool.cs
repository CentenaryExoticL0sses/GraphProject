using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using GraphProject.InputControls;
using GraphProject.Tools.States;
using GraphProject.Visualization;

namespace GraphProject.Tools
{
    public class GraphCreationTool : MonoBehaviour
    {
        private GraphContainer _graphContainer;
        private GraphPartSelector _partSelector;

        private ICreationState _creationState;
        private GraphActions _actions;

        private bool _isOverUI;

        public void Initialize(GraphContainer graphContainer, GraphPartSelector partSelector)
        {
            _creationState = null;
            _graphContainer = graphContainer;
            _partSelector = partSelector;
        }

        private void OnEnable()
        {
            _actions = new GraphActions();
            _actions.Tool.MouseAction.performed += GraphMouseAction;
            _actions.Tool.Cancel.performed += CancelMode;
            _actions.Tool.Enable();
        }

        private void OnDisable()
        {
            _actions.Tool.MouseAction.performed -= GraphMouseAction;
            _actions.Tool.Cancel.performed -= CancelMode;
            _actions.Tool.Disable();
        }

        private void CancelMode(InputAction.CallbackContext context)
        {
            CancelAnyState();
        }

        private void GraphMouseAction(InputAction.CallbackContext context)
        {
            if (_isOverUI) return;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _creationState?.OnAction(worldPosition);
        }

        private void Update()
        {
            if (_creationState != null)
            {
                _isOverUI = EventSystem.current.IsPointerOverGameObject();
            }
        }

        /// <summary>
        /// Режим создания вершин.
        /// </summary>
        public void CreateVertex() => SetState(new VertexCreationState(_graphContainer, _partSelector));

        /// <summary>
        /// Режим удаления вершин.
        /// </summary>
        public void DeleteVertex() => SetState(new VertexRemovalState(_graphContainer, _partSelector));

        /// <summary>
        /// режим создания рёбер.
        /// </summary>
        public void CreateEdge() => SetState(new EdgeCreationState(_graphContainer, _partSelector));

        /// <summary>
        /// Режим удаления рёбер.
        /// </summary>
        public void DeleteEdge() => SetState(new EdgeRemovalState(_graphContainer, _partSelector));

        /// <summary>
        /// Режим поиска пути.
        /// </summary>
        public void FindPath() => SetState(new PathFindingState(_graphContainer, _partSelector));

        /// <summary>
        /// Сброс любого режима.
        /// </summary>
        public void CancelAnyState() => SetState(null);

        private void SetState(ICreationState state)
        {
            _creationState?.Exit();
            _creationState = state;
            _creationState?.Enter();
        }
    }
}