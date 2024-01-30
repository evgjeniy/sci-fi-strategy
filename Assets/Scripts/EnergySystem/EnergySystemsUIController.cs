using System;
using System.Collections.Generic;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.EnergySystem
{
    public class EnergySystemsUIController : MonoBehaviour
    {
        //[Inject] public ResourceManager Manager { get; private set; }
        public EnergyController Controller { get; private set; }
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
            ui.MaxBarsCount = system.MaxEnergy;
            uiButton.OnLeftMouseClick += system.TrySpendEnergy;
            uiButton.OnRightMouseClick += system.TryRefillEnergy;
            system.OnCurrentEnergyChanged += ui.ChangeEnergy;
        }
        
        [Inject]
        public void InitializeComponent(EnergyController controller)
        {
            Controller = controller;
            foreach (var system in Controller.Systems)
            {
                GenerateNewUI(system);
            }
            Controller.OnSystemAdded += GenerateNewUI;
        }

        private void OnDisable()
        {
            Controller.OnSystemAdded -= GenerateNewUI;
        }
    }
}