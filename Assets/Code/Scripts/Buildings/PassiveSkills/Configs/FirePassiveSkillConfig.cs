using UnityEngine;

namespace SustainTheStrain.Buildings
{
    [CreateAssetMenu(fileName = nameof(FirePassiveSkillConfig), menuName = "Configs/" + nameof(FirePassiveSkillConfig), order = Const.Order.BuildingConfigs)]
    public class FirePassiveSkillConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.0f)] public float DamagePerSecond { get; private set; }
        [field: SerializeField, Min(0.0f)] public float FireDuration { get; private set; }
        [field: SerializeField, Min(0)] public int AttackFrequency { get; private set; }
        
        [field: SerializeField] public ParticleSystem FireParticle { get; private set; }
        
        public void EnableSkill(GameObject gameObject)
        {
            var effect = gameObject.GetComponent<FireEffect>();
            if (effect == null) effect = gameObject.AddComponent<FireEffect>();
            
            effect.Initialize(this);
        }
    }
}