using SustainTheStrain.Configs.Buildings;
using TMPro;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class LaserManagementMenu : BuildingManagementMenu<LaserBuildingConfig>
    {
        [SerializeField] private TMP_Text _uiTip;
        
        [Inject] protected override IBuilding Building { get; }

        protected override void OnConfigChanged(LaserBuildingConfig buildingConfig)
        {
            base.OnConfigChanged(buildingConfig);
            UpdateTipContent(buildingConfig);
        }

        private void UpdateTipContent(LaserBuildingConfig config)
        {
            _uiTip.text = config.NextLevelConfig != null
                ? $@"<b>Laser (Level {config.Level})</b>
<b>Stats</b>
Radius: {config.Radius} {config.NextLevelConfig.Radius.GetUpgradeString(config.Radius)}
Damage: {config.Damage} {config.NextLevelConfig.Damage.GetUpgradeString(config.Damage)}
Cooldown: {config.Cooldown} {config.NextLevelConfig.Cooldown.GetUpgradeString(config.Cooldown)}"
                : $@"<b>Laser (Level {config.Level})</b>
<b>Stats</b>
Radius: {config.Radius}
Damage: {config.Damage}
Cooldown: {config.Cooldown}";
        }
    }
}