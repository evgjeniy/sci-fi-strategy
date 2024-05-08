using UnityEngine;

namespace SustainTheStrain.Buildings
{
    [CreateAssetMenu(fileName = nameof(StunConfig), menuName = "Configs/" + nameof(StunConfig), order = Const.Order.BuildingConfigs)]
    public class StunConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.0f)] public float Duration { get; private set; } = 1.5f;
        [field: SerializeField, Min(0)] public int AttackFrequency { get; private set; } = 2;
        
        public void EnableSkill(GameObject gameObject)
        {
            var effect = gameObject.GetComponent<StunEffect>();
            if (effect == null) effect = gameObject.AddComponent<StunEffect>();
            
            effect.Initialize(Duration);
        }

	public void EnableSkillWithDuration(GameObject gameObject, float duration)
        {
            var effect = gameObject.GetComponent<StunEffect>();
            if (effect == null) effect = gameObject.AddComponent<StunEffect>();
            
            effect.Initialize(duration);
        }
    }
}