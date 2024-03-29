using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.ResourceSystems
{
    public interface IResourceManager
    {
        public Observable<int> Gold { get; }
        public bool TrySpend(int spendValue);
        public void AddGold(int count);
    }

    public class ResourceManager : MonoBehaviour, IResourceManager
    {
        [SerializeField] private int _currentGold;
        
        public List<IEnergySystem> Generators => _generators;
        private List<IEnergySystem> _generators = new List<IEnergySystem>();
        
        private GoldGenerator _goldGenerator;

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
        
        public Observable<int> Gold { get; private set; }

        [Inject]
        public void AddGenerators([Inject(Optional = true)] GoldGenerator goldGenerator /*, ExplorePointGenerator explorePointGenerator*/)
        {
            //_explorePointGenerator = explorePointGenerator;
            //_generators.Add(_explorePointGenerator);
            _goldGenerator = goldGenerator;
            _generators.Add(_goldGenerator);

            Gold = new Observable<int>(_currentGold);
        }

        private void OnEnable()
        {
            _goldGenerator.OnResourceGenerated += AddGold;
            //_explorePointGenerator.OnResourceGenerated += AddExplorePoint;
        }

        private void OnDisable()
        {
            _goldGenerator.OnResourceGenerated -= AddGold;
            //_explorePointGenerator.OnResourceGenerated -= AddExplorePoint;
        }

        public void AddGold(int count)
        {
            Gold.Value += count;
        }

        // void AddExplorePoint(int count)
        // {
        //     CurrentExplorePoints += count;
        // }

        public bool TrySpend(int spendValue)
        {
            var goldAfterSpend = Gold.Value - spendValue;
            if (goldAfterSpend < 0) return false;

            Gold.Value = goldAfterSpend;
            return true;
        }
    }
}