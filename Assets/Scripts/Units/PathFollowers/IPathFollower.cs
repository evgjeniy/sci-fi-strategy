namespace SustainTheStrain.Units.PathFollowers
{
    public interface IPathFollower
    {
        public float Speed { get; set; }

        public void Start();
        public void Stop();
    }
}
