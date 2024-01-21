using System;
using System.Collections;
using UnityEngine;

namespace SustainTheStrain.ResourceSystems
{
    public abstract class ResourceGenerator : MonoBehaviour
    {
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
        
        protected Coroutine _generatingRoutine;
        protected bool _canGenerate;
        
        public void StartGeneration()
        {
            _generatingRoutine = StartCoroutine(GenerateResource());
        }

        public IEnumerator GenerateResource()
        {
            while (_canGenerate)
            {
                yield return new WaitForSeconds(Cooldown);
                _resourceGenerated?.Invoke(_generateCount);
            }
            EndGeneration();
        }

        public void EndGeneration()
        {
            if (_generatingRoutine != null)
            {
                StopCoroutine(_generatingRoutine);
            }
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