using System;
using System.Collections;
using Systems;
using UnityEngine;
using Zenject;

namespace ResourceSystems
{
    public class ExplorePointGenerator : ResourceGenerator, IEnergySystem
    {
        [Inject] public EnergyController EnergyController { get; set; }
        [field:SerializeField] public int EnergySpendCount { get; private set; }
        public int MaxEnergy
        {
            get => _maxEnergy;
            private set
            {
                _maxEnergy = value;
                OnMaxEnergyChanged?.Invoke(_maxEnergy);
            }
        }
        
        [SerializeField] 
        private int _maxEnergy;
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        public int CurrentEnergy
        {
            get => _currentEnergy;
            private set
            {
                if (value < 0 || value > MaxEnergy) return;
                if (!_canGenerate && value > 0)
                {
                    _canGenerate = true;
                    StartGeneration();
                }
                _currentEnergy = value;
                OnCurrentEnergyChanged?.Invoke(_currentEnergy);
                _canGenerate = value != 0;
            }
        }
    
        private int _currentEnergy;
        
        public override void StartGeneration()
        {
            _generatingRoutine = StartCoroutine(GenerateResource());
        }

        public override IEnumerator GenerateResource()
        {
            while (_canGenerate)
            {
                yield return new WaitForSeconds(Cooldown);
                _resourceGenerated?.Invoke(_generateCount);
            }
            EndGeneration();
        }

        public override void EndGeneration()
        {
            if (_generatingRoutine != null)
            {
                StopCoroutine(_generatingRoutine);
            }
        }
        
        public void IncreaseMaxEnergy(int value)
        {
            MaxEnergy += value;
        }

        public void TrySpendEnergy()
        {
            if (FreeEnergyCells<EnergySpendCount) return;
            if (EnergyController.TryGetEnergy(EnergySpendCount))
            {
                CurrentEnergy += EnergySpendCount;
            }
            //Here should be system upgrade logic
        }

        public void TryRefillEnergy()
        {
            if (_currentEnergy <= EnergySpendCount) return;
            if (EnergyController.TryReturnEnergy(EnergySpendCount))
            {
                CurrentEnergy -= EnergySpendCount;
            }
            //Here should be system downgrade logic
        }
        
        private void OnDisable()
        {
            EndGeneration();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                TrySpendEnergy();
            }
        }
    }
}