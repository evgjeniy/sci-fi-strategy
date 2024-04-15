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
        public Observable<int> Gold { get; private set; }
        [SerializeField] private int _currentGold;
        
        public List<IEnergySystem> Generators => _generators;
        private List<IEnergySystem> _generators = new List<IEnergySystem>();
        
        private GoldGenerator _goldGenerator;

        [Inject]
        public void AddGenerators([Inject(Optional = true)] GoldGenerator goldGenerator)
        {
            _goldGenerator = goldGenerator;
            _generators.Add(_goldGenerator);

            Gold = new Observable<int>(_currentGold);
        }

        private void OnEnable()
        {
            _goldGenerator.OnResourceGenerated += AddGold;
        }

        private void OnDisable()
        {
            _goldGenerator.OnResourceGenerated -= AddGold;
        }

        public void AddGold(int count)
        {
            Gold.Value += count;
        }
        
        public bool TrySpend(int spendValue)
        {
            var goldAfterSpend = Gold.Value - spendValue;
            if (goldAfterSpend < 0) return false;

            Gold.Value = goldAfterSpend;
            return true;
        }
    }
}