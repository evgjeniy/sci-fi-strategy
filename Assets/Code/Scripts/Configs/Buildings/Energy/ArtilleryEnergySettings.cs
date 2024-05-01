using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(menuName = "Configs/Energy/" + nameof(ArtilleryEnergySettings), fileName = nameof(ArtilleryEnergySettings))]
    public class ArtilleryEnergySettings : DamageEnergySettings
    {
        [field: SerializeField, Expandable] public StunConfig PassiveSkill { get; private set; }
    }
}