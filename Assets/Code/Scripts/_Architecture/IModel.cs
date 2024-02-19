namespace SustainTheStrain._Architecture
{
    public interface IModel<out TModel> where TModel : IModel<TModel>
    {
        public event System.Action<TModel> Changed;
    }
}