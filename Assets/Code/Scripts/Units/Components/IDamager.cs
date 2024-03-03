using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public interface IDamagger
    {
        public float Damage { get; }
        public float DamagePeriod { get; }
    }
}
