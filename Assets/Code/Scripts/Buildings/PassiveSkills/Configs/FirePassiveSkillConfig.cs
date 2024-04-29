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
        
        public override void EnableSkill(GameObject gameObject)
        {
            var effect = gameObject.GetComponent<FireEffect>();
            if (effect == null) effect = gameObject.AddComponent<FireEffect>();
            
            effect.Initialize(DamagePerSecond, FireDuration);
        }

        public override void DisableSkill(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<FireEffect>(out var effect)) 
                Destroy(effect);
        }
    }
}