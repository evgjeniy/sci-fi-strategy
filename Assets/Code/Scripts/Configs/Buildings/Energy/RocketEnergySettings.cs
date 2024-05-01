using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Energy/" + nameof(RocketEnergySettings), fileName = nameof(RocketEnergySettings))]
    public class RocketEnergySettings : DamageEnergySettings
    {
        [field: SerializeField] public FirePassiveSkillConfig PassiveSkill { get; private set; }
    }
}