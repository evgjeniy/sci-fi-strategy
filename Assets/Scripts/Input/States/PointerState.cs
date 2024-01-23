using System;
using NTC.FiniteStateMachine;

namespace SustainTheStrain.Input.States
{
    public class SelectionState<T> : PointerState<T>
    {
        public SelectionState(Func<T> get, Action<T> onSelected, Action<T> onDeselected) : base(get, onSelected, onDeselected) {}
    }

    public class PointerState<T> : IState<InputService>
    {
        private readonly Func<T> _get;
        private readonly Action<T> _onEnter;
        private readonly Action<T> _onExit;

        public virtual InputService Initializer => null;

        public PointerState(Func<T> get, Action<T> onEnter, Action<T> onExit)
        {
            _get = get;
            _onEnter = onEnter;
            _onExit = onExit;
        }

        public virtual void OnEnter() => _onEnter?.Invoke(_get.Invoke());
        public virtual void OnExit() => _onExit?.Invoke(_get.Invoke());
    }
}