using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class BarrackInitState : IUpdatableState<Barrack>
    {
        public IUpdatableState<Barrack> Update(Barrack barrack)
        {
            int index = 0;
            while (barrack.RecruitGroup.Recruits.Count < barrack.RecruitGroup.squadMaxSize)
            {
                var recruit = barrack.RecruitSpawner.Spawn()
                    .With(x => x.SetParent(barrack.transform))
                    .With(x => x.Damage = barrack.Config.UnitAttackDamage * barrack.BarrackSystem.DamageMultiplier)
                    .With(x => x.Duelable.Damageable.MaxHP = barrack.Config.UnitMaxHealth * barrack.BarrackSystem.HealthMultiplier)
                    .With(x => x.Duelable.Damageable.CurrentHP = barrack.Config.UnitMaxHealth * barrack.BarrackSystem.HealthMultiplier)
                    .With(x => x.DamagePeriod = barrack.Config.UnitAttackCooldown);

                barrack.RecruitGroup.AddRecruit(recruit);
                var timer = barrack.Timers[index];
                recruit.Duelable.Damageable.OnDied += (x) => { timer.IsPaused = false; };
                index++;
            }

            return new BarrackIdleState();
        }
    }
}