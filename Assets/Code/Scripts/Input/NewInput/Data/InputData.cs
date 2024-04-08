using System;
using UnityEngine;

namespace SustainTheStrain.Input
{
    [Serializable]
    public class InputData : IInputData
    {
        public Vector2 MousePosition { get; set; }
        
        [field: SerializeField] public float Distance { get; private set; } = float.MaxValue;
        [field: SerializeField] public LayerMask Mask { get; private set; } = -1;
    }
}