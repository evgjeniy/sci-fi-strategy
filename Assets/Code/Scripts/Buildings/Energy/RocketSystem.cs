using System;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class RocketSystem : IEnergySystem
    {
        private const string EnergySettingsPath = Const.ResourcePath.Buildings.Configs.Root + "/Energy/" + nameof(RocketEnergySettings);
        private int _currentEnergy;

        public RocketEnergySettings EnergySettings { get; } = Resources.Load<RocketEnergySettings>(EnergySettingsPath);
        EnergySystemSettings IEnergySystem.EnergySettings => EnergySettings;
        public int MaxEnergy => EnergySettings.MaxEnergy;

        public int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                _currentEnergy = value;
                Changed(this);
            }
        }

        public event Action<IEnergySystem> Changed = _ => {};

        [Inject]
        private void Construct(EnergyController energyController) => energyController.AddEnergySystem(this);
    }
}