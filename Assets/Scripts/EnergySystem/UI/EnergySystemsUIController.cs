using System;
using System.Collections.Generic;
using SustainTheStrain.AbilitiesScripts;
using SustainTheStrain.EnergySystem.UI;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.EnergySystem
{
    public class EnergySystemsUIController : MonoBehaviour
    {
        [Inject] public AbilitiesController MAbilitiesController { get; private set;}

        [SerializeField] private AbilitiesUIController _abilitiesUIController;
        //[Inject] public ResourceManager Manager { get; private set; }
        public EnergyController EnergyController { get; private set; }
        [SerializeField] private Transform _spawnParent;
        private EnergySystemUIFactory _uiFactory;
        private Dictionary<IEnergySystem, EnergySystemUI> _systemsUis = new();

        private void GenerateNewUI(IEnergySystem system)
        {
            var ui = _uiFactory.CreateUI(system, _spawnParent);
            _systemsUis.TryAdd(ui.Key, ui.Value);
        }

        [Inject]
        public void AddSystemsUIFactory(EnergySystemUIFactory energySystemUIFactory)
        {
            _uiFactory = energySystemUIFactory;
            _uiFactory.MAbilitiesUIController = _abilitiesUIController;
        }
        
        [Inject]
        public void InitializeComponent(EnergyController controller)
        {
            EnergyController = controller;
            foreach (var system in EnergyController.Systems)
            {
                GenerateNewUI(system);
            }
            EnergyController.OnSystemAdded += GenerateNewUI;
        }

        private void OnDisable()
        {
            EnergyController.OnSystemAdded -= GenerateNewUI;
        }
    }
}