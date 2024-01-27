using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Timer
    {
        private float _time;

        public float Time
        {
            get => _time;
            set => _time = Mathf.Clamp(value, 0.0f, Mathf.Infinity);
        }

        public bool IsTimeOver => Time == 0.0f;

        public Timer(float time = 0) => Time = time;
    }
}