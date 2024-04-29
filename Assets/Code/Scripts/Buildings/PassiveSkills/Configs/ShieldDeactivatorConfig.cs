using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    [CreateAssetMenu(fileName = nameof(ShieldDeactivatorConfig), menuName = "Configs/" + nameof(ShieldDeactivatorConfig), order = Const.Order.BuildingConfigs)]
    public class ShieldDeactivatorConfig : PassiveSkillConfig
    {
        [field: SerializeField, Min(0.0f)] public float Duration { get; private set; } = 1.5f;
        [field: SerializeField, Min(0)] public int AttackFrequency { get; private set; } = 2;
        
        public override void EnableSkill(GameObject gameObject)
        {
            var effect = gameObject.GetComponent<ShieldDeactivatorEffect>();
            if (effect == null) effect = gameObject.AddComponent<ShieldDeactivatorEffect>();
            
            effect.Initialize(Duration);
        }

        public override void DisableSkill(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<ShieldDeactivatorEffect>(out var effect)) 
                Destroy(effect);
        }
    }
}