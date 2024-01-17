using System;
using System.Collections;
using Systems;
using UnityEngine;

namespace ResourceSystems
{
    public class ExplorePointGenerator : ResourceGenerator, IEnergySystem
    {
        // private void Start()
        // {
        //     StartGeneration();
        // }

        public override void StartGeneration()
        {
            _canGenerate = true;
            //Temp value
            _generatingRoutine = StartCoroutine(GenerateResource());
        }

        public override IEnumerator GenerateResource()
        {
            while (_canGenerate)
            {
                yield return new WaitForSeconds(_cooldown);
                OnResourceGenerated?.Invoke(_generateCount);
            }
            EndGeneration();
        }

        public override void EndGeneration()
        {
            _generatingRoutine = null;
        }

        public override void IncreaseGenerateCount()
        {
            base.IncreaseGenerateCount();
        }

        public override void IncreaseGenerateSpeed()
        {
            base.IncreaseGenerateSpeed();
        }
        
        public EnergyManager _energyManager { get; }
        public int _energySpendCount { get; }
        public int MaxEnergy { get; }
        public Action<int> OnCurrentEnergyChanged { get; }
        public Action<int> OnMaxEnergyChanged { get; }
        public int CurrentEnergy { get; }


        private void OnDisable()
        {
            EndGeneration();
        }
    }
}