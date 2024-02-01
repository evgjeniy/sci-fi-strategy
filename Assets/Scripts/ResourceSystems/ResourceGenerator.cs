using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SustainTheStrain.ResourceSystems
{
    public abstract class ResourceGenerator : MonoBehaviour
    {
        [field: SerializeField] public GeneratorSettings MGeneratorSettings { get; private set; }
        
        private float _generationTime;
        protected float _cooldown;
        protected float _minimalCooldown;
        protected float _maximalCooldown;
        protected int _generateCount;
        
        protected bool _canGenerate;
        protected bool _generationReseted = true;
        
        protected Action<int> _resourceGenerated;
        protected Action<float> _generatedPercentChanged;
        
        public float Cooldown
        {
            get => _cooldown;
            protected set
            {
                if (value < 0) return;
                _cooldown = Math.Clamp(value, _minimalCooldown, _maximalCooldown);
                EndGeneration();
            }
        }
        public event Action<float> OnGeneratedPercentChanged
        { 
            add
            {
                if (value != null)
                {
                    _generatedPercentChanged += value;
                }
            } 
            remove => _generatedPercentChanged -= value;
        }
        public event Action<int> OnResourceGenerated
        {
            add
            {
                if (value != null)
                {
                    _resourceGenerated += value;
                }
            }
            remove => _resourceGenerated -= value;
        }
        
        public void StartGeneration()
        {
            _canGenerate = true;
        }
        
        private void Update()
        {
            if (_canGenerate)
            {
                _generationReseted = false;
                _generationTime += Time.deltaTime;
                CheckGeneration();
                _generatedPercentChanged?.Invoke(_generationTime/Cooldown);
            }
            else
            {
                if (!_generationReseted)
                {
                    EndGeneration();
                }
            }
        }

        private void CheckGeneration()
        {
            if (_generationTime < Cooldown) return;
            _resourceGenerated?.Invoke(_generateCount);
            _generationTime = 0;
        }

        public void EndGeneration()
        {
            _generationTime = 0;
            _generationReseted = true;
            _generatedPercentChanged?.Invoke(0);
        }

        protected void UpgradeAll()
        {
            IncreaseGenerateSpeed(MGeneratorSettings.UpgradeStats.CooldownChange);
            IncreaseGenerateCount(MGeneratorSettings.UpgradeStats.IncomeChange);
        }
        
        protected void DowngradeAll()
        {
            IncreaseGenerateSpeed(-MGeneratorSettings.UpgradeStats.CooldownChange);
            IncreaseGenerateCount(-MGeneratorSettings.UpgradeStats.IncomeChange);
        }

        public void IncreaseGenerateCount(int count)
        {
            _generateCount += count;
        }

        public void IncreaseGenerateSpeed(float count)
        {
            Cooldown -= count;
        }

        public virtual void LoadSettings()
        {
            _cooldown = MGeneratorSettings.BaseCooldown;
            _minimalCooldown = MGeneratorSettings.MinimalCooldown;
            _maximalCooldown = MGeneratorSettings.MaximalCooldown;
            _generateCount = MGeneratorSettings.GenerateCount;
        }
        
    }
}