using UnityEngine.Rendering.VirtualTexturing;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    //я бы убрал систему из аргумента и перенес в конструктор конкретной фабрики и вообще этот интерфейс не нужен
    public interface IEnergySystemUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
    }
}