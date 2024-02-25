using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public abstract class MonoUIFactory : MonoBehaviour, IEnergySystemUIFactory
    {
        [Inject] protected EnergyController _energyController;
        [SerializeField] protected EnergySystemUI _uiPrefab;
        [SerializeField] protected Transform _spawnParent;
        [SerializeField] protected Image _backgroundImage;
        [SerializeField] protected EnergySystemControllButton _controllButton;
        
        public virtual EnergySystemUI Create(IEnergySystem system)
        {
            throw new System.NotImplementedException();
        }
    }
}