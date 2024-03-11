using UnityEngine;

namespace SustainTheStrain.Scriptable.AbilitySettings
{
    [CreateAssetMenu(fileName = "ZoneDamageAbilitySettings", menuName = "AbilitySettings/ZoneDamage", order = 1)]
    public class ZoneDamageAbilitySettings : AbilitySettings
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float ZoneRadius { get; private set; }
        [field: SerializeField] public GameObject ExplosionPrefab { get; private set; }
    }
}