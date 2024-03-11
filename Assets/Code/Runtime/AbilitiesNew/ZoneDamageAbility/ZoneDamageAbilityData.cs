using System;
using SustainTheStrain._Architecture;
using UnityEngine;

namespace SustainTheStrain.AbilitiesNew
{
    [CreateAssetMenu(fileName = "ZoneDamageAbilityData", menuName = "NewAbilityData/ZoneDamage", order = 1)]
    public class ZoneDamageAbilityData : AbilityData
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float ZoneRadius { get; private set; }
        [field: SerializeField] public ParticleSystem ExplosionPrefab { get; private set; }
    }
}