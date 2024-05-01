namespace SustainTheStrain
{
    [System.Serializable]
    public class Timer : ITimer, Zenject.ITickable, IObservable<ITimer>
    {
        [UnityEngine.SerializeField] private float _time;
        [UnityEngine.SerializeField] private bool _isPaused;

        private float _definedTime;

        public float Time    { get => _time;     private set { _time = value;     Changed(this); }  }
        public bool IsPaused { get => _isPaused;         set { _isPaused = value; Changed(this); }  }
        public float Percent => _definedTime == 0.0f ? 1.0f : (1.0f - _time / _definedTime);
        public bool IsOver => Time <= 0.0f;
        
        public ITimer Value => this;
        public event System.Action<ITimer> Changed = _ => {};

        public Timer(float time = 0) => ResetTime(time);

        public void Tick()
        {
            if (IsOver || IsPaused)
                return;

            Time -= UnityEngine.Time.deltaTime;
        }

        public void ResetTime(float newTime) => Time = _definedTime = newTime;
    }
}