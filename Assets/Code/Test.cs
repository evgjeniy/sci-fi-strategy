using SustainTheStrain.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SustainTheStrain
{
    public class Test : MonoBehaviour
    {
        private IInputService _input;

        private void Awake() => _input = new InputService();
        private void Start() => _input.Enable();
        private void OnEnable() => _input.Player.AbilityMove.performed += Move;
        private void OnDisable() => _input.Player.AbilityMove.performed -= Move;

        private static void Move(InputAction.CallbackContext obj)
        {
            Debug.Log($"{obj.ReadValueAsObject()}");
        }
    }
}