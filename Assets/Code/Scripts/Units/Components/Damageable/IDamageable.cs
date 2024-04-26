using System;


namespace SustainTheStrain.Units.Components
{
    public interface IDamageable : IHealth, ITeam
    {
        public event Action<Damageble> OnDied;

        public bool IsFlying { get; }

        public void Damage(float damage);
        public void Kill(bool suicide = false);
    }
}
