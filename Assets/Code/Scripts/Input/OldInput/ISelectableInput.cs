using System;
using UnityEngine;

namespace SustainTheStrain.Input
{
    public interface IAbilityInput
    {
        public event Action<Ray> OnAbilityMove;
        public event Action<Ray> OnAbilityClick;
        public event Action<int> OnAbilityEnter;
        public event Action<int> OnAbilityChanged;
        public event Action<int> OnAbilityExit;
    }
}