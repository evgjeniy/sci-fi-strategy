namespace SustainTheStrain.Buildings
{
    public class Timer
    {
        private float _time;

        public float Time
        {
            get => _time;
            set => _time = UnityEngine.Mathf.Clamp(value, 0.0f, UnityEngine.Mathf.Infinity);
        }

        public bool IsTimeOver => Time == 0.0f;

        public Timer(float time = 0) => Time = time;
    }
}