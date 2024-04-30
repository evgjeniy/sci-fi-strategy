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
        
        public float CurrentMultiplayer => _mine.GoldMultipliers[_mine.CurrentEnergy];
        
        private Mine _mine;

        [Inject]
        public void AddGenerators([Inject(Optional = true)] Mine mine)
        {
            _mine = mine;
            _generators.Add(_mine);

            Gold = new Observable<int>(_currentGold);
        }

        private void OnEnable()
        {
            //_mine.OnResourceGenerated += AddGold;
        }

        private void OnDisable()
        {
            //_mine.OnResourceGenerated -= AddGold;
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