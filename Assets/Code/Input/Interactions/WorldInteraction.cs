using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace SustainTheStrain.Input.Interactions
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public class WorldInteraction : IInputInteraction
    {
        public int LayerMask = -1;
        public float RayCastMaxDistance = 100;

        private readonly List<RaycastResult> _rayCastUIResults = new();

        static WorldInteraction() => InputSystem.RegisterInteraction(typeof(WorldInteraction));

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() {}

        public void Reset() {}

        public void Process(ref InputInteractionContext context)
        {
            var camera = Camera.main;
            if (camera == null || context.control.device is not Pointer pointer) return;

            var mousePosition = pointer.position.value;
            
            var eventSystem = EventSystem.current;
            if (eventSystem != null)
            {
                eventSystem.RaycastAll(new PointerEventData(eventSystem) { position = mousePosition }, _rayCastUIResults);
                if (_rayCastUIResults.Count != 0) return;
            }

            var fromCameraRay = camera.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(fromCameraRay, out var hit, RayCastMaxDistance, LayerMask)) return;

            switch (context.phase)
            {
                case InputActionPhase.Waiting: context.Started(); break;
                case InputActionPhase.Started: context.Performed(); break;
                case InputActionPhase.Performed: context.Canceled(); break;
            }
        }
    }
}