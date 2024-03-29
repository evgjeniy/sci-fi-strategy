using System;
using SustainTheStrain.Input;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class MouseMoveFollower : MonoBehaviour
    {
        [Inject] private IMouseMove _move;

        private void OnEnable() => _move.OnMouseMove += InputOnOnMouseMove;
        private void OnDisable() => _move.OnMouseMove -= InputOnOnMouseMove;

        private void InputOnOnMouseMove(RaycastHit hit) => transform.position = hit.point;
    }
}