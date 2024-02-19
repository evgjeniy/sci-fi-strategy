namespace SustainTheStrain._Architecture
{
    public interface IController<out TModel, out TView>
        where TModel : IModel<TModel>
        where TView : IView<TModel>
    {
        public TModel Model { get; }
        public TView View { get; }

        public void OnEnable() => Model.Changed += View.Display;
        public void OnDisable() => Model.Changed -= View.Display;
    }
}