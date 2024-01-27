using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.ResourceSystems
{
    public abstract class ResourceGenerator : MonoBehaviour
    {
        [field: SerializeField] public Image GeneratingIndicator { get; private set; }
        public float Cooldown
        {
            get => _cooldown;
            protected set
            {
                if (value < 0) return;
                _cooldown = Math.Clamp(value, _minimalCooldown, _maximalCooldown);
                EndGeneration();
                StartGeneration();
            }
        }

        private float _generationTime;

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
        protected Action<float> _generatedPercentChanged;
        
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
        protected Action<int> _resourceGenerated;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected float _minimalCooldown;
        [SerializeField] protected float _maximalCooldown;
        [SerializeField] protected int _generateCount;
        
        protected bool _canGenerate;
        protected bool _generationReseted = true;
        
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

        public void IncreaseGenerateCount(int count)
        {
            _generateCount += count;
        }

        public void IncreaseGenerateSpeed(float count)
        {
            Cooldown -= count;
        }
        
    }
}