using UnityEngine;

namespace SustainTheStrain
{
    public interface IAreaDataProvider
    {
        public Vector3 Center { get; }
        public float Radius { get; }
        public LayerMask Mask { get; }
    }
}