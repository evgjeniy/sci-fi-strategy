namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackIdleState : IBarrackState
    {
        public IBarrackState Update(Barrack barrack)
        {
            return this;
        }
    }
}