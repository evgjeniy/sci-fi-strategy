using NaughtyAttributes;
using SustainTheStrain.Buildings;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Energy/" + nameof(BarrackEnergySettings), fileName = nameof(BarrackEnergySettings))]
    public class BarrackEnergySettings : EnergySystemSettings
    {
        [field: SerializeField, Expandable] public AdditionalBarrackRecruitConfig PassiveSkill { get; set; }
    }
}