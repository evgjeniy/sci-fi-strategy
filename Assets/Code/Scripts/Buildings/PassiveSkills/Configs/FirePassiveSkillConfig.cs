using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    [CreateAssetMenu(fileName = nameof(FirePassiveSkillConfig), menuName = "Configs/" + nameof(FirePassiveSkillConfig), order = Const.Order.BuildingConfigs)]
    public class FirePassiveSkillConfig : PassiveSkillConfig
    {
        [field: SerializeField, Min(0.0f)] public float DamagePerSecond { get; private set; }
        [field: SerializeField, Min(0.0f)] public float FireDuration { get; private set; }
        [field: SerializeField, Min(0)] public int AttackFrequency { get; private set; }

        private readonly Dictionary<GameObject, FireEffect> _effects = new(capacity: 16);
        
        public override void EnableSkill(GameObject gameObject)
        {
            if (_effects.TryGetValue(gameObject, out var fireEffect) is false)
            {
                fireEffect = gameObject.AddComponent<FireEffect>();
                _effects.Add(gameObject, fireEffect);
            }
            
            fireEffect.Initialize(DamagePerSecond, FireDuration);
            
        }

        public override void DisableSkill(GameObject gameObject)
        {
            if (_effects.Remove(gameObject, out var fireEffect)) 
                Destroy(fireEffect);
        }
    }
}