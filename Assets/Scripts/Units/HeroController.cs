using System;
using SustainTheStrain.Input;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units
{
    public class HeroController : MonoBehaviour
    {
        [Inject] private IHeroInput _heroInput;
        [Inject] private Hero _hero;
        
        private void OnEnable()
        {
            _heroInput.OnSelected += HeroSelected;
            _heroInput.OnPointerEnter += HeroSelected;
            _heroInput.OnMove += HeroMove;
        }

        private void OnDisable()
        {
            _heroInput.OnSelected -= HeroSelected;
            _heroInput.OnPointerEnter -= HeroSelected;
            _heroInput.OnMove -= HeroMove;
        }
        
        private void HeroMove(Hero arg1, RaycastHit arg2)
        {
            Debug.LogWarning("HeroMoved");
            _hero.Move(arg2.point);
        }
        
        private void HeroSelected(Hero obj)
        {
            Debug.LogWarning("HeroSelected");
        }
    }
}
