using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Energy/" + nameof(BarrackEnergySettings), fileName = nameof(BarrackEnergySettings))]
    public class BarrackEnergySettings : DamageEnergySettings
    {
        [field: SerializeField] public float[] EnergyHealthMultipliers { get; private set; } = { 0.5f, 0.7f, 1.0f, 1.2f };
        [field: SerializeField, Expandable] public AdditionalBarrackRecruitConfig PassiveSkill { get; set; }
        
        public float GetHealthMultiplier(int currentEnergy)
        {
            var clampEnergy = Mathf.Clamp(currentEnergy, 0, EnergyHealthMultipliers.Length);
            return EnergyHealthMultipliers[clampEnergy];
        }
    }
}