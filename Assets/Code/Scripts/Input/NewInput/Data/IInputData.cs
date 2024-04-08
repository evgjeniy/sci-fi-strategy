namespace SustainTheStrain.Input
{
    public interface IInputData
    {
        public UnityEngine.Vector2 MousePosition { get; }
        public float Distance { get; }
        public UnityEngine.LayerMask Mask { get; }
    }
}