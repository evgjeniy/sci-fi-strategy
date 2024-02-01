using System;
using UnityEngine;

namespace SustainTheStrain.ResourceSystems
{
    [CreateAssetMenu(fileName = "GeneratorUpgradeStats", menuName = "EnergySystemUpgradeSettings/GeneratorUpgradeStats", order = 1)]
    public class GeneratorUpgradeStats : ScriptableObject
    {
        [SerializeField] private float _cooldownChangePerEnergy;
        public float CooldownChange => _cooldownChangePerEnergy;

        [SerializeField] private int _incomeChangePerEnergy;
        public int IncomeChange => _incomeChangePerEnergy;
    }
}