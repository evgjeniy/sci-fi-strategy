using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class BarrackInitState : IUpdatableState<Barrack>
    {
        public IUpdatableState<Barrack> Update(Barrack barrack)
        {
            while (barrack.RecruitGroup.Recruits.Count < barrack.RecruitGroup.squadMaxSize)
            {
                var recruit = barrack.RecruitSpawner.Spawn()
                    .With(x => x.SetParent(barrack.transform))
                    .With(x => x.Damage = barrack.Config.UnitAttackDamage)
                    .With(x => x.Duelable.Damageable.MaxHP = barrack.Config.UnitMaxHealth)
                    .With(x => x.Duelable.Damageable.CurrentHP = barrack.Config.UnitMaxHealth)
                    .With(x => x.DamagePeriod = barrack.Config.UnitAttackCooldown);

                barrack.RecruitGroup.AddRecruit(recruit);
            }

            return new BarrackIdleState();
        }
    }
}