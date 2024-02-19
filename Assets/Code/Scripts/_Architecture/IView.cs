namespace SustainTheStrain._Architecture
{
    public interface IView<in TModel> where TModel : IModel<TModel>
    {
        public void Display(TModel model) {}
    }
}