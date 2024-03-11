namespace SustainTheStrain.Input
{
    public interface IInputService
    {
        public InputService.UIActions UI { get; }
        public InputService.PlayerActions Player { get; }

        public void Enable();
        public void Disable();
    }

    public partial class InputService : IInputService {}
}