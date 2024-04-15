using UnityEngine;
using Zenject;

namespace SustainTheStrain
{
    [System.Serializable]
    public class Timer : ITimer, ITickable
    {
        [field: SerializeField] public float Time { get; private set; }
        [field: SerializeField] public bool IsPaused { get; set; }

        public bool IsTimeOver => Time <= 0.0f;

        public Timer(float time = 0) => Time = time;
        
        public void Tick()
        {
            if (IsTimeOver || IsPaused) return;

            Time -= UnityEngine.Time.deltaTime;
        }

        public void ResetTime(float newTime) => Time = newTime;
    }

    public interface ITimer
    {
        float Time { get;  }
        bool IsPaused { get; set; }
        bool IsTimeOver { get; }

        void ResetTime(float newTime);
    }
}