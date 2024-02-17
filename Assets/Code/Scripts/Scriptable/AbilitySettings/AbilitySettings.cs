using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem.Settings;
using UnityEngine;

namespace SustainTheStrain
{
    public abstract class AbilitySettings : ScriptableObject
    {
        public EnergySystemSettings EnergySettings;
        public float ReloadingSpeed;
    }
}
