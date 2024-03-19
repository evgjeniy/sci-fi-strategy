using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.UI;
using SustainTheStrain.EnergySystem.UI.Factories;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnergySystemsUIFactoriesInstaller : MonoInstaller
    {
        //[SerializeField] private EnergySystemsUIPrefabsHolder _prefabsHolder;
        //[SerializeReference] private IFactory<IEnergySystem, EnergySystemUI> factory;
        
        

        public override void InstallBindings()
        {
            //Container.BindIFactory<AbilityUIFactory>().From
        }
    }
}