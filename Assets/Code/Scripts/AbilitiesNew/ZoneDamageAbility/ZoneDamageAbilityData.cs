using System;
using SustainTheStrain._Architecture;
using UnityEngine;

namespace SustainTheStrain.AbilitiesNew
{
    [CreateAssetMenu(fileName = "ZoneDamageAbilityData", menuName = "NewAbilityData/ZoneDamage", order = 1)]
    public class ZoneDamageAbilityData : AbilityData, IModel<ZoneDamageAbilityData>
    {
        [field: SerializeField] public ZoneDamageAbilityView ViewPrefab { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float ZoneRadius { get; private set; }
        [field: SerializeField] public ParticleSystem ExplosionPrefab { get; private set; }

        protected override void CurrentReloadChanged(float _) => Changed?.Invoke(this);

        public event Action<ZoneDamageAbilityData> Changed;
    }
}