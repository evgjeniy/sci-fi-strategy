using System;


namespace SustainTheStrain.Units.Components
{
    public interface IDamageable : IHealth, ITeam
    {
        public event Action<Damageable> OnDied;

        public void Damage(float damage);
        public void Die();
    }
}
