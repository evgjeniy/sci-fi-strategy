using Systems;
using UnityEngine;
using Zenject;

public class EnergyManagerInstaller : MonoInstaller
{
    [SerializeField] private EnergyManager _energyManager;
    public override void InstallBindings()
    {
        Container.Bind<EnergyManager>().FromInstance(_energyManager).AsSingle();
    }
}