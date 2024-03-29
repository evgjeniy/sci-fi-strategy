using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BarrackManagementMenu : BuildingManagementMenu
    {
        [SerializeField] private Button _unitsPointButton;

        [Inject] private Barrack _barrack;

        private void OnEnable()
        {
            SubscribeBaseEvents(_barrack);

            _barrack.Data.Config.Changed += OnConfigChanged;
            _unitsPointButton.onClick.AddListener(_barrack.SetUnitsPointState);
        }

        private void OnDisable()
        {
            UnsubscribeBaseEvents(_barrack);

            _barrack.Data.Config.Changed -= OnConfigChanged;
            _unitsPointButton.onClick.RemoveListener(_barrack.SetUnitsPointState);
        }
    }
}