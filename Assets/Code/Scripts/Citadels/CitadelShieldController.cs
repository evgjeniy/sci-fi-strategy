using SustainTheStrain.EnergySystem;
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
    }
}