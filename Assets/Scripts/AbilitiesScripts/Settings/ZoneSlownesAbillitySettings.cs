using UnityEngine;

namespace SustainTheStrain
{
    [CreateAssetMenu(fileName = "ZoneSlownessnAbilitySettings", menuName = "AbilitySettings/ZoneSlowness", order = 1)]
    public class ZoneSlownesAbillitySettings : AbilitySettings
    {
        public float ZoneRadius;
        public float SpeedKoeficient;
        public float DurationTime;
    }
}