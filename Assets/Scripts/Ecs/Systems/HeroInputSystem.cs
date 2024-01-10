using UnityEngine;
using UnityEngine.InputSystem;
using Unity.AI.Navigation;
using Leopotam.Ecs;
using Ecs.Components.Requests;
using Ecs.Components.Tag;
using Ecs.Utilities;

namespace Ecs.Systems
{
    public class HeroInputSystem : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<EnableInputRequest> _enableFilter;
        private EcsFilter<DisableInputRequest> _disableFilter;

        private HeroActions _playerActions;

        public void PreInit() => _playerActions = new HeroActions();

        public void Init()
        {
            _playerActions.Enable();
            _playerActions.Player.Move.started += OnMouseClick;
        }

        public void Destroy()
        {
            _playerActions.Player.Move.started -= OnMouseClick;
            _playerActions.Disable();
        }

        public void Run()
        {
            if (!_enableFilter.IsEmpty()) _playerActions.Enable();
            if (!_disableFilter.IsEmpty()) _playerActions.Disable();
        }

        private void OnMouseClick(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            var clickPosition = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(clickPosition);

            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.collider.TryGetComponent<NavMeshSurface>(out _)) return;

            _world.GetEntity<Hero>().SendMessage(new SetDestinationRequest { destination = hit.point });
        }
    }
}