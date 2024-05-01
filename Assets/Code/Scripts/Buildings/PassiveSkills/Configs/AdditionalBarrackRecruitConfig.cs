using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    [CreateAssetMenu(fileName = nameof(AdditionalBarrackRecruitConfig), menuName = "Configs/" + nameof(AdditionalBarrackRecruitConfig), order = Const.Order.BuildingConfigs)]
    public class AdditionalBarrackRecruitConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public int RecruitsAmount { get; private set; } = 1;

        public void EnableSkill(GameObject gameObject)
        {
            var effect = gameObject.GetComponent<AdditionalBarrackRecruitEffect>();

            if (effect == null)
            {
                effect = gameObject.AddComponent<AdditionalBarrackRecruitEffect>();
                effect.Initialize(RecruitsAmount);
            }
        }

        public void DisableSkill(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<AdditionalBarrackRecruitEffect>(out var effect)) 
                Destroy(effect);
        }
    }
}