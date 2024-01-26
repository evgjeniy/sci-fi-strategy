using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public interface IHealth
    {
        public float MaxHP { get; set; }
        public float CurrentHP { get; set; }

        public event Action<float> OnCurrentHPChanged;
    }
}
