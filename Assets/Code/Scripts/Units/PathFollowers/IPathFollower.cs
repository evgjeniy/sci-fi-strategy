namespace SustainTheStrain.Units
{
    public interface IPathFollower
    {
        public float Speed { get; set; }

        public void Start();
        public void Stop();
    }
}
