using System;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class ArtillerySystem : IEnergySystem
    {
        private const string EnergySettingsPath = Const.ResourcePath.Buildings.Configs.Root + "/Energy/" + nameof(ArtilleryEnergySettings);
        private int _currentEnergy;

        public ArtilleryEnergySettings Settings { get; } = Resources.Load<ArtilleryEnergySettings>(EnergySettingsPath);
        EnergySystemSettings IEnergySystem.EnergySettings => Settings;
        public int MaxEnergy => Settings.MaxEnergy;

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

        public float DamageMultiplier => Settings.GetDamageMultiplier(CurrentEnergy);

        [Inject]
        private void Construct(EnergyController energyController) => energyController.AddEnergySystem(this);
    }
}