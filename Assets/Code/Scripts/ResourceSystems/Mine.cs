﻿using System;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.ResourceSystems
{
    public class Mine : ResourceGenerator, IEnergySystem
    {
        [Inject] public EnergyController EnergyController { get; set; }

        [field: SerializeField] public EnergySystemSettings EnergySettings { get; private set; }
        [field: SerializeField] public float[] GoldMultipliers { get; private set; }
        
        public Sprite ButtonImage => EnergySettings.ButtonImage;
   
        private int _currentEnergy;
        private TMP_Text _uiTip;

        public int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                if (value < 0 || value > MaxEnergy) return;
                if (!_canGenerate && value > 0)
                {
                    //StartGeneration();
                }
                _currentEnergy = value;
                _canGenerate = value != 0;
                Changed?.Invoke(this);
                UpdateTip(this);
            }
        }

        public int MaxEnergy { get; private set; }

        private void OnEnable()
        {
            LoadSettings();
        }

        public void IncreaseMaxEnergy(int value)
        {
            MaxEnergy += value;
        }

        public bool TrySpendEnergy(int count)
        {
            CurrentEnergy += count;
            //UpgradeAll();
            return true;
        }

        public bool TryRefillEnergy(int count)
        {
            CurrentEnergy -= count;
            //DowngradeAll();
            return true;
        }

        private void OnDisable()
        {
            EndGeneration();
        }

        public override void LoadSettings()
        {
            base.LoadSettings();
            MaxEnergy = EnergySettings.MaxEnergy;
        }

        public IEnergySystem Value => this;
        public event Action<IEnergySystem> Changed;
        public void CacheUiTip(TMP_Text uiTip) { _uiTip = uiTip; UpdateTip(this); }
        private void UpdateTip(IEnergySystem system) => _uiTip.IfNotNull(x => x.text = $"Active cells: {system.CurrentEnergy}");
    }
}