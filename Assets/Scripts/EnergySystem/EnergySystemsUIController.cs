using System;
using System.Collections.Generic;
using SustainTheStrain.AbilitiesScripts;
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
        [SerializeField] private EnergySystemUI UIPrefab;
        [SerializeField] private Transform _spawnParent;

        public void GenerateNewUI(IEnergySystem system)
        {
            var ui = Instantiate(UIPrefab, _spawnParent.transform);
            var uiButton = ui.SpawnButton(system.ButtonImage);
            // if (Manager.Generators.Contains(system))
            // {
            //     var generatorUI = new GeneratorUI(uiButton.transform, (ResourceGenerator)system);
            // }
            if (MAbilitiesController.Abilities.Contains((BaseAbility)system))
            {
                _abilitiesUIController.SpawnControlButton(uiButton.transform);
            }
            ui.MaxBarsCount = system.MaxEnergy;
            uiButton.OnLeftMouseClick += system.TrySpendEnergy;
            uiButton.OnRightMouseClick += system.TryRefillEnergy;
            system.OnCurrentEnergyChanged += ui.ChangeEnergy;
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