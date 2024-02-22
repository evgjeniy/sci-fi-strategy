using System;

namespace SustainTheStrain.Units
{
    public interface IHealth
    {
        public float MaxHP { get; set; }
        public float CurrentHP { get; set; }

        public event Action<float> OnCurrentHPChanged;
    }
}
