using SustainTheStrain.Abilities;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class EnergySystemsUIPrefabsHolder : ScriptableObject
    {
        [Inject] private EnergyController _energyController;
        [Inject] private AbilitiesUIController mAbilitiesUIController;
        [SerializeField] private EnergySystemUI _uiPrefab;
        [SerializeField] private Transform _standartUISpawnParent;
        [SerializeField] private Transform _abilitiesUISpawnParent;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private EnergySystemControllButton _controllButton;
        //private Image _backgroundImage;
        [SerializeField] private float _abilitiesScaleMultiplayer;
        [SerializeField] private Slider _abilitiesSliderPrefab;
    }
}