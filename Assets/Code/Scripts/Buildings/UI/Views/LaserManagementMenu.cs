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
                ? $@"<b>Лазер (Уровень {config.Level})</b>
<b>Stats</b>
Радиус: {config.Radius} {config.NextLevelConfig.Radius.GetUpgradeString(config.Radius)}
Урон: {config.Damage} {config.NextLevelConfig.Damage.GetUpgradeString(config.Damage)}
Перезарядка: {config.Cooldown} {config.NextLevelConfig.Cooldown.GetUpgradeString(config.Cooldown)}"
                : $@"<b>Лазер (Уровень {config.Level})</b>
<b>Stats</b>
Радиус: {config.Radius}
Урон: {config.Damage}
Перезарядка: {config.Cooldown}";
        }
    }
}