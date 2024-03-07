using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Citadels
{
    public class CitadelShieldController : MonoEnergySystem
    {
        [SerializeField] private Shield _shield;

        private void OnEnable() => OnCurrentEnergyChanged += UpdateCellsCount;
        private void OnDisable() => OnCurrentEnergyChanged -= UpdateCellsCount;
        private void UpdateCellsCount(int currentEnergy) => _shield.CellsCount = currentEnergy;

        public override void SetEnergySettings(EnergySystemSettings settings)
        {
            EnergySettings = settings;
            _maxEnergy = settings.MaxEnergy;
            _currentEnergy = 0;
        }
    }
}