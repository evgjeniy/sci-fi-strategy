using System;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.ResourceSystems
{
    public class ResourceManager : MonoBehaviour
    {
        private int _currentExplorePoints;
        [SerializeField] private int _maxExplorePoints;
        public event Action<int> OnExplorePointsChanged;
        
        public int CurrentExplorePoints
        {
            get => _currentExplorePoints;
            private set
            {
                if (value > _maxExplorePoints) return;
                _currentExplorePoints = value;
                OnExplorePointsChanged?.Invoke(_currentExplorePoints);
            }
        }
        
        private int _currentGold;
        [SerializeField] private int _maxGold;
        public event Action<int> OnGoldChanged;
        
        public int CurrentGold
        {
            get => _currentGold;
            private set
            {
                if (value > _maxGold) return;
                _currentGold = value;
                OnGoldChanged?.Invoke(_currentGold);
            }
        }
        
        [SerializeField]private ExplorePointGenerator _explorePointGenerator;
        [SerializeField]private GoldGenerator _goldGenerator;

        [Inject]
        public void AddGenerators(ExplorePointGenerator explorePointGenerator, GoldGenerator goldGenerator)
        {
            _explorePointGenerator = explorePointGenerator;
            _goldGenerator = goldGenerator;
            Subscribe();
        }

        private void Subscribe()
        {
            _goldGenerator.OnResourceGenerated += AddGold;
            _explorePointGenerator.OnResourceGenerated += AddExplorePoint;
        }
        
        void AddGold(int count)
        {
            CurrentGold += count;
        }

        void AddExplorePoint(int count)
        {
            CurrentExplorePoints += count;
        }
        
        private void UnSubscribe()
        {
            _goldGenerator.OnResourceGenerated -= AddGold;
            _explorePointGenerator.OnResourceGenerated -= AddExplorePoint;
        }
        
        private void OnDisable()
        {
            UnSubscribe();
        }
        
    }
}