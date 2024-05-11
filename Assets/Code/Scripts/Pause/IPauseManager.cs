namespace SustainTheStrain.Tips
{
    public interface IPauseManager
    {
        public bool Paused { get; }
        public void Pause();
        public void Unpause();
    }
}