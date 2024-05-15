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
    public class LaserSystem : IEnergySystem
    {
        private const string EnergySettingsPath = Const.ResourcePath.Buildings.Configs.Root + "/Energy/" + nameof(LaserEnergySettings);
        private int _currentEnergy;
        private TMP_Text _uiTip;

        public LaserEnergySettings Settings { get; } = Resources.Load<LaserEnergySettings>(EnergySettingsPath);
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

        public event Action<IEnergySystem> Changed = _ => {};

        public float DamageMultiplier => Settings.GetDamageMultiplier(CurrentEnergy);

        [Inject]
        private void Construct(EnergyController energyController) => energyController.AddEnergySystem(this);

        public void CacheUiTip(TMP_Text uiTip) { _uiTip = uiTip; UpdateTip(this); }
        private void UpdateTip(IEnergySystem system)
        {
            _uiTip.IfNotNull(x => x.text = 
                $@"<b><align=""center"">Система лазеров (энергия: <color=""green"">{system.CurrentEnergy}</color>)</align></b>
Блок 3: урон <b><#FF0000>120%</color></b> + пассивная способность: каждый
            второй выстрел лишает врага щита на <b><#00FF00>1,5</color> сек.</b>
Блок 2: урон <b><#FF0000>100%</color></b>
Блок 1: урон <b><#FF0000>70%</color></b>
Энергии нет: урон <b><#FF0000>50%</color></b>");
        }
    }
}