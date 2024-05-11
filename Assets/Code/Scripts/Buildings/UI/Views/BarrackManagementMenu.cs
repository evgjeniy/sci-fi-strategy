using SustainTheStrain.Configs.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BarrackManagementMenu : BuildingManagementMenu<BarrackBuildingConfig>
    {
        [SerializeField] private Button _unitsPointButton;
        [SerializeField] private TMP_Text _uiTip;

        [Inject] private Barrack _barrack;

        protected override IBuilding Building => _barrack;

        protected override void Awake()     { base.Awake();     _unitsPointButton.onClick.AddListener(SetUnitsPointState);    }
        protected override void OnDestroy() { base.OnDestroy(); _unitsPointButton.onClick.RemoveListener(SetUnitsPointState); }

        private void SetUnitsPointState()
        {
            _barrack.SetUnitsPointState();
            gameObject.Deactivate();
        }

        protected override void OnConfigChanged(BarrackBuildingConfig buildingConfig)
        {
            base.OnConfigChanged(buildingConfig);
            UpdateTipContent(buildingConfig);
        }

        private void UpdateTipContent(BarrackBuildingConfig config)
        {
            _uiTip.text = config.NextLevelConfig != null
                ? $@"<b>Barrack (Level {config.Level})</b>
<b>Stats</b>
Radius: {config.Radius} {config.NextLevelConfig.Radius.GetUpgradeString(config.Radius)}
UnitMaxHealth: {config.UnitMaxHealth} {config.NextLevelConfig.UnitMaxHealth.GetUpgradeString(config.UnitMaxHealth)}
UnitAttackDamage: {config.UnitAttackDamage} {config.NextLevelConfig.UnitAttackDamage.GetUpgradeString(config.UnitAttackDamage)}
UnitAttackCooldown: {config.UnitAttackCooldown} {config.NextLevelConfig.UnitAttackCooldown.GetUpgradeString(config.UnitAttackCooldown)}
RespawnCooldown: {config.RespawnCooldown} {config.NextLevelConfig.RespawnCooldown.GetUpgradeString(config.RespawnCooldown)}"
                : $@"<b>Barrack (Level {config.Level})</b>
<b>Stats</b>
Radius: {config.Radius}
UnitMaxHealth: {config.UnitMaxHealth}
UnitAttackDamage: {config.UnitAttackDamage}
UnitAttackCooldown: {config.UnitAttackCooldown}
RespawnCooldown: {config.RespawnCooldown}";
        }
    }
}