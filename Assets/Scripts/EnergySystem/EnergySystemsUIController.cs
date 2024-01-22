using System;
using System.Collections.Generic;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.EnergySystem
{
    public class EnergySystemsUIController : MonoBehaviour
    {
        public EnergyController Controller { get; private set; }
        [SerializeField] private EnergySystemUI UIPrefab;
        [SerializeField] private Transform _spawnParent;

        public void GenerateNewUI(IEnergySystem system)
        {
            var ui = Instantiate(UIPrefab, _spawnParent.transform);
            var uiButton = ui.SpawnButton(system.ButtonImage);
            ui.MaxBarsCount = system.MaxEnergy;
            uiButton.OnLeftMouseClick += system.TrySpendEnergy;
            uiButton.OnRightMouseClick += system.TryRefillEnergy;
            system.OnCurrentEnergyChanged += ui.ChangeEnergy;
        }
        
        [Inject]
        public void InitializeComponent(EnergyController controller)
        {
            Controller = controller;
            Controller.OnSystemAdded += GenerateNewUI;
            foreach (var system in Controller.Systems)
            {
                GenerateNewUI(system);
            }
        }

        private void OnDisable()
        {
            Controller.OnSystemAdded -= GenerateNewUI;
        }
    }
}