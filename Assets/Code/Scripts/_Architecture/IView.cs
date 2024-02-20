namespace SustainTheStrain._Architecture
{
    public interface IView<in TModel>
    {
        public void Display(TModel model) {}
    }
}