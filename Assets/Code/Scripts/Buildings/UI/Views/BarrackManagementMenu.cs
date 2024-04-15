using SustainTheStrain.Configs.Buildings;
using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BarrackManagementMenu : BuildingManagementMenu<BarrackBuildingConfig>
    {
        [SerializeField] private Button _unitsPointButton;
        [Inject] private Barrack _barrack;

        protected override IBuilding Building => _barrack;

        protected override void Awake()     { base.Awake();     _unitsPointButton.onClick.AddListener(SetUnitsPointState);    }
        protected override void OnDestroy() { base.OnDestroy(); _unitsPointButton.onClick.RemoveListener(SetUnitsPointState); }

        private void SetUnitsPointState()
        {
            _barrack.SetUnitsPointState();
            gameObject.Deactivate();
        }
    }
}