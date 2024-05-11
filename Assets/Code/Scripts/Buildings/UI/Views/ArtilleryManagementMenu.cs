using SustainTheStrain.Configs.Buildings;
using TMPro;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryManagementMenu : BuildingManagementMenu<ArtilleryBuildingConfig>
    {
        [SerializeField] private TMP_Text _uiTip;

        [Inject] protected override IBuilding Building { get; }

        protected override void OnConfigChanged(ArtilleryBuildingConfig buildingConfig)
        {
            base.OnConfigChanged(buildingConfig);
            UpdateTipContent(buildingConfig);
        }

        private void UpdateTipContent(ArtilleryBuildingConfig config)
        {
            _uiTip.text = config.NextLevelConfig != null
                ? $@"<b>Artillery (Level {config.Level})</b>
<b>Stats</b>
Radius: {config.Radius} {config.NextLevelConfig.Radius.GetUpgradeString(config.Radius)}
Damage: {config.Damage} {config.NextLevelConfig.Damage.GetUpgradeString(config.Damage)}
Cooldown: {config.Cooldown} {config.NextLevelConfig.Cooldown.GetUpgradeString(config.Cooldown)}
Exp. Radius: {config.ExplosionRadius} {config.NextLevelConfig.ExplosionRadius.GetUpgradeString(config.ExplosionRadius)}"
                : $@"<b>Artillery (Level {config.Level})</b>
<b>Stats</b>
Radius: {config.Radius}
Damage: {config.Damage}
Cooldown: {config.Cooldown}
Exp. Radius: {config.ExplosionRadius}";
        }
    }
}