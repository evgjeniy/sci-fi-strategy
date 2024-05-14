using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Energy/" + nameof(BarrackEnergySettings), fileName = nameof(BarrackEnergySettings))]
    public class BarrackEnergySettings : DamageEnergySettings
    {
        [field: SerializeField, Expandable] public AdditionalBarrackRecruitConfig PassiveSkill { get; set; }
    }
}