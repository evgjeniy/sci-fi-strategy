using SustainTheStrain.Configs.Buildings;
using TMPro;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class RocketManagementMenu : BuildingManagementMenu<RocketBuildingConfig>
    {
        [SerializeField] private TMP_Text _uiTip;
        
        [Inject] protected override IBuilding Building { get; }

        protected override void OnConfigChanged(RocketBuildingConfig buildingConfig)
        {
            base.OnConfigChanged(buildingConfig);
            UpdateTipContent(buildingConfig);
        }

        private void UpdateTipContent(RocketBuildingConfig config)
        {
            _uiTip.text = config.NextLevelConfig != null
                ? $@"<b>Ракетница (Уровень {config.Level})</b>
<b>Характеристики</b>
Радиус: {config.Radius} {config.NextLevelConfig.Radius.GetUpgradeString(config.Radius)}
Урон: {config.Damage} {config.NextLevelConfig.Damage.GetUpgradeString(config.Damage)}
Перезарядка: {config.Cooldown} {config.NextLevelConfig.Cooldown.GetUpgradeString(config.Cooldown)}
Кол-во целей: {config.MaxTargets} {config.NextLevelConfig.MaxTargets.GetUpgradeString(config.MaxTargets)}"
                : $@"<b>Ракетница (Уровень {config.Level})</b>
<b>Характеристики</b>
Радиус: {config.Radius}
Урон: {config.Damage}
Перезарядка: {config.Cooldown}
Кол-во целей: {config.MaxTargets}";
        }
    }
}