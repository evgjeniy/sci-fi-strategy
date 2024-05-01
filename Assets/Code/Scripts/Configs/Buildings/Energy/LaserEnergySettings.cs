using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Energy/" + nameof(LaserEnergySettings), fileName = nameof(LaserEnergySettings))]
    public class LaserEnergySettings : DamageEnergySettings
    {
        [field: SerializeField, Expandable] public ShieldDeactivatorConfig PassiveSkill { get; private set; }
    }
}