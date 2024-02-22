namespace SustainTheStrain._Architecture
{
    public interface IModel<out TModel>
    {
        public event System.Action<TModel> Changed;
    }
}