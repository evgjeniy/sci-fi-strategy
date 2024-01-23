using System;
using NTC.FiniteStateMachine;

namespace SustainTheStrain.Input.States
{
    public class MouseMoveState : IState<InputService>
    {
        private readonly Action<UnityEngine.RaycastHit> _onMouseMove;

        public InputService Initializer { get; }

        public MouseMoveState(InputService initializer, Action<UnityEngine.RaycastHit> onMouseMove)
        {
            Initializer = initializer;
            _onMouseMove = onMouseMove;
        }

        public void OnRun() => _onMouseMove?.Invoke(Initializer.Data.Hit);
    }
}