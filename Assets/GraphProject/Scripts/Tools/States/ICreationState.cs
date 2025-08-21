using UnityEngine;

namespace GraphProject.Tools.States
{
    public interface ICreationState
    {
        public void Enter();

        void OnAction(Vector2 position);

        public void Exit();
    }
}