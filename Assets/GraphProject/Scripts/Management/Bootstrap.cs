using UnityEngine;
using GraphProject.Core.Data;
using GraphProject.Services;
using GraphProject.Tools;
using GraphProject.Visualization;

namespace GraphProject.Management
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Компоненты")]
        [SerializeField] private GraphContainer _graphContainer;
        [SerializeField] private GraphCreationTool _graphTool;
        [SerializeField] private GraphSaveSystem _saveSystem;

        private Graph _graphModel;
        private GraphPartSelector _partSelector;

        private void Start()
        {
            _graphModel = new Graph();
            _partSelector = new GraphPartSelector(_graphContainer);

            _graphContainer.Initialize(_graphModel);
            _graphTool.Initialize(_graphContainer, _partSelector);
            _saveSystem.Initialize(_graphContainer);
        }
    }
}