using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Citadels
{
    public class CitadelShieldController : MonoEnergySystem
    {
        [SerializeField] private Shield _shield;

        private void OnEnable() => Changed += UpdateCellsCount;
        private void OnDisable() => Changed -= UpdateCellsCount;
        private void UpdateCellsCount(IEnergySystem system) => _shield.CellsCount = system.CurrentEnergy;

        public override void SetEnergySettings(EnergySystemSettings settings)
        {
            EnergySettings = settings;
            _maxEnergy = settings.MaxEnergy;
            _currentEnergy = 0;
        }
    }
}