using System;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.ResourceSystems
{
    public class ResourceManager : MonoBehaviour
    {
        public List<IEnergySystem> Generators => _generators;
        private List<IEnergySystem> _generators = new List<IEnergySystem>();
        
        /*
        public event Action<int> OnExplorePointsChanged;
        [SerializeField] private int _maxExplorePoints;
        private ExplorePointGenerator _explorePointGenerator;
        private int _currentExplorePoints;
        public int CurrentExplorePoints
        {
            get => _currentExplorePoints;
            private set
            {
                if (value > _maxExplorePoints) return;
                _currentExplorePoints = value;
                OnExplorePointsChanged?.Invoke(_currentExplorePoints);
            }
        }*/
        
        public event Action<int> OnGoldChanged;
        [SerializeField] private int _maxGold;
        private GoldGenerator _goldGenerator;
        private int _currentGold;
        public int CurrentGold
        {
            get => _currentGold;
            set
            {
                _currentGold = Mathf.Clamp(value, 0, _maxGold);
                OnGoldChanged?.Invoke(_currentGold);
            }
        }

        [Inject]
        public void AddGenerators(GoldGenerator goldGenerator/*, ExplorePointGenerator explorePointGenerator*/)
        {
            //_explorePointGenerator = explorePointGenerator;
            //_generators.Add(_explorePointGenerator);
            _goldGenerator = goldGenerator;
            _generators.Add(_goldGenerator);
            Subscribe();
        }

        private void Subscribe()
        {
            _goldGenerator.OnResourceGenerated += AddGold;
            //_explorePointGenerator.OnResourceGenerated += AddExplorePoint;
        }
        
        void AddGold(int count)
        {
            CurrentGold += count;
        }

        // void AddExplorePoint(int count)
        // {
        //     CurrentExplorePoints += count;
        // }
        
        private void UnSubscribe()
        {
            _goldGenerator.OnResourceGenerated -= AddGold;
            //_explorePointGenerator.OnResourceGenerated -= AddExplorePoint;
        }
        
        private void OnDisable()
        {
            UnSubscribe();
        }
    }
}