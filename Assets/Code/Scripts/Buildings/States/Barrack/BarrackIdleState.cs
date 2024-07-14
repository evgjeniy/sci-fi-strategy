using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class BarrackIdleState : IUpdatableState<Barrack>
    {
        public IUpdatableState<Barrack> Update(Barrack barrack)
        {
            foreach (var timer in barrack.Timers)
            {
                timer.Tick();
                if (!timer.IsOver) continue;
                barrack.RecruitGroup.Recruits.RemoveAll(x => x == null);

                var needToSpawn = barrack.RecruitGroup.Recruits.Count < barrack.RecruitGroup.squadMaxSize;

                if (!needToSpawn) continue;
                {
                    var recruit = barrack.RecruitSpawner.Spawn()
                        .With(x => x.SetParent(barrack.transform))
                        .With(x => x.Damage = barrack.Config.UnitAttackDamage * barrack.BarrackSystem.DamageMultiplier)
                        .With(x => x.Duelable.Damageable.MaxHP = barrack.Config.UnitMaxHealth * barrack.BarrackSystem.HealthMultiplier)
                        .With(x => x.Duelable.Damageable.CurrentHP = barrack.Config.UnitMaxHealth * barrack.BarrackSystem.HealthMultiplier)
                        .With(x => x.DamagePeriod = barrack.Config.UnitAttackCooldown);
                    
                    
                    barrack.RecruitGroup.AddRecruit(recruit);
                    timer.ResetTime(barrack.Config.RespawnCooldown);
                    timer.IsPaused = true;
                    recruit.Duelable.Damageable.OnDied += (x) => { timer.IsPaused = false; };
                }
            }
            return this;
        }
    }
}