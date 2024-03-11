namespace SustainTheStrain.Global
{
    public interface IController<out TModel, out TView>
    {
        public TModel Model { get; }
        public TView View { get; }
    }
}