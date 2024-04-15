namespace SustainTheStrain.Buildings
{
    public interface IUpdatableState<in TContext>
    {
        public IUpdatableState<TContext> Update(TContext context);
    }
}