using System;
using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.Citadels;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain
{
    public class CitadelStagesController : MonoBehaviour
    {
        [SerializeField] private int[] _healthStages;
        [SerializeField] private GameObject[] _citadelStages;
        [SerializeField] private CitadelDamageble _damageable;
        
        private int _currentStage;

        private void Awake()
        {
            if (_citadelStages.Length != _healthStages.Length)
            {
                throw new Exception("Citadel and health length not equal");
            }
            _damageable.OnCurrentHPChanged += CheckHealth;
        }

        public void CheckHealth(float health)
        {
            var percent = health*100 / _damageable.MaxHP;
            if (percent >= _healthStages[_currentStage]) return;
            _citadelStages[_currentStage].SetActive(false);
            _currentStage++;
            if (_currentStage >= _citadelStages.Length) return;
            _citadelStages[_currentStage].SetActive(true);
        }

        private void OnDisable()
        {
            _damageable.OnCurrentHPChanged -= CheckHealth;
        }
    }
}
