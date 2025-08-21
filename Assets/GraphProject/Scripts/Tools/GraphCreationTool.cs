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

        /// <summary>
        /// Выполнение функции выбранного режима по нажатию ЛКМ
        /// </summary>
        /// <param name="context"></param>
        private void GraphMouseAction(InputAction.CallbackContext context)
        {
            if (_isOverUI)
                return;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (_creationState != null)
            {
                _creationState.OnAction(worldPosition);
            }
        }

        private void Update()
        {
            if (_creationState != null)
                _isOverUI = EventSystem.current.IsPointerOverGameObject();
        }

        public void CreateVertex()
        {
            CancelAnyState();
            _creationState = new VertexCreationState(_graphContainer, _partSelector);
        }

        public void CreateEdge()
        {
            CancelAnyState();
            _creationState = new EdgeCreationState(_graphContainer, _partSelector);
        }

        public void FindPath()
        {
            CancelAnyState();
            _creationState = new PathFindingState(_graphContainer, _partSelector);
        }

        //Сброс режима работы инструмента
        public void CancelAnyState()
        {
            if (_creationState != null)
            {
                _creationState.Exit();
                _creationState = null;
            }
        }
    }
}