using System;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    [Serializable]
    public struct GuardPost
    {
        [SerializeField]
        private Vector3 _position;
        [SerializeField]
        private float _radius;

        public Vector3 Position 
        { 
            get { return _position; }
            set { _position = value; OnValuesChanged?.Invoke(); } }
        public float Radius 
        { 
            get { return _radius; } 
            set { _radius = value; OnValuesChanged?.Invoke();} 
        }

        public event Action OnValuesChanged;
    }
}
