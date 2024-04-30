using System;
using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class RocketSystem : IEnergySystem
    {
        private const string EnergySettingsPath = Const.ResourcePath.Buildings.Configs.Root + "/EnergySettings/Rocket";
        private RocketBuildingConfig _config;

        public EnergySystemSettings EnergySettings { get; } = Resources.Load<EnergySystemSettings>(EnergySettingsPath);
        public int MaxEnergy => EnergySettings.MaxEnergy;
        public int CurrentEnergy
        {
            get => _config.CurrentEnergy;
            set
            {
                _config.CurrentEnergy = value;
                Changed(this);
            }
        }

        public event Action<IEnergySystem> Changed = _ => {};

        [Inject]
        private void Construct(IConfigProviderService configProvider, EnergyController energyController)
        {
            _config = configProvider.GetBuildingConfig<RocketBuildingConfig>();
            energyController.AddEnergySystem(this);
        }
    }
}