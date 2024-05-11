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
                ? $@"<b>Артиллерия (Урон {config.Level})</b>
<b>Характеристики</b>
Радиус: {config.Radius} {config.NextLevelConfig.Radius.GetUpgradeString(config.Radius)}
Урон: {config.Damage} {config.NextLevelConfig.Damage.GetUpgradeString(config.Damage)}
Перезарядка: {config.Cooldown} {config.NextLevelConfig.Cooldown.GetUpgradeString(config.Cooldown)}
Радиус взрыва: {config.ExplosionRadius} {config.NextLevelConfig.ExplosionRadius.GetUpgradeString(config.ExplosionRadius)}"
                : $@"<b>Артиллерия (Урон {config.Level})</b>
<b>Характеристики</b>
Радиус: {config.Radius}
Урон: {config.Damage}
Перезарядка: {config.Cooldown}
Радиус взрыва: {config.ExplosionRadius}";
        }
    }
}