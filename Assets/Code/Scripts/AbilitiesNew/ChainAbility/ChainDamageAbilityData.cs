using System;
using SustainTheStrain._Architecture;
using UnityEngine;

namespace SustainTheStrain.AbilitiesNew
{
    [CreateAssetMenu(fileName = "ChainAbilitySettings", menuName = "NewAbilitySettings/ChainAbility", order = 1)]
    public class ChainDamageAbilityData : AbilityData, IModel<ChainDamageAbilityData>
    {
        [field: SerializeField] public ChainDamageAbilityView ViewPrefab { get; private set; }
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; }
        [field: SerializeField, Min(0)] public int MaxTargets { get; private set; }
        [field: SerializeField, Min(0)] public int Distance { get; private set; }
        [field: SerializeField] public LineRenderer LinePrefab { get; private set; }

        protected override void CurrentReloadChanged(float _) => Changed?.Invoke(this);

        public event Action<ChainDamageAbilityData> Changed;
    }
}