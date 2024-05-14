using System;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BarrackSystem : IEnergySystem
    {
        private const string EnergySettingsPath = Const.ResourcePath.Buildings.Configs.Root + "/Energy/" + nameof(BarrackEnergySettings);

        private int _currentEnergy;
        private TMP_Text _uiTip;

        public BarrackEnergySettings Settings { get; } = Resources.Load<BarrackEnergySettings>(EnergySettingsPath);
        EnergySystemSettings IEnergySystem.EnergySettings => Settings;
        public int MaxEnergy => Settings.MaxEnergy;

        public int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                _currentEnergy = value;
                Changed(this);
                UpdateTip(this);
            }
        }

        public float DamageMultiplier => Settings.GetDamageMultiplier(CurrentEnergy);
        public float HealthMultiplier => Settings.GetHealthMultiplier(CurrentEnergy);

        public event Action<IEnergySystem> Changed = _ => { };

        [Inject]
        private void Construct(EnergyController energyController) => energyController.AddEnergySystem(this);

        public void CacheUiTip(TMP_Text uiTip) { _uiTip = uiTip; UpdateTip(this); }
        private void UpdateTip(IEnergySystem system) => _uiTip.IfNotNull(x => x.text = $"Active cells: {system.CurrentEnergy}");
    }
}