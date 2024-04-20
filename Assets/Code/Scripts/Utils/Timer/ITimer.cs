namespace SustainTheStrain
{
    public interface ITimer
    {
        float Time { get; }
        float Percent { get; }
        bool IsOver { get; }
        bool IsPaused { get; set; }

        void ResetTime(float newTime);
    }
}