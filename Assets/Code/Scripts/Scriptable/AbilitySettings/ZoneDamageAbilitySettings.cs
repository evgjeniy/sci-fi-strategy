using UnityEngine;

namespace SustainTheStrain
{
    [CreateAssetMenu(fileName = "ZoneDamageAbilitySettings", menuName = "AbilitySettings/ZoneDamage", order = 1)]
    public class ZoneDamageAbilitySettings : AbilitySettings
    {
        public float Damage;
        public float ZoneRadius;
        public GameObject ExplosionPrefab;
    }
}