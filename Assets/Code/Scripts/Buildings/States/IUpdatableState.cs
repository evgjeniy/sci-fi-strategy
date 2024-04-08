namespace SustainTheStrain.Buildings.States
{
    public interface IUpdatableState<in TContext>
    {
        public IUpdatableState<TContext> Update(TContext context);
    }
}