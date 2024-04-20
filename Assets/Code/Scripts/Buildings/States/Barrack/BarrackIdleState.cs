using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class BarrackIdleState : IUpdatableState<Barrack>
    {
        public IUpdatableState<Barrack> Update(Barrack barrack)
        {
            if (barrack.Timer.IsOver)
            {
                barrack.RecruitGroup.Recruits.RemoveAll(x => x == null);

                var needToSpawn = barrack.RecruitGroup.Recruits.Count < barrack.RecruitGroup.squadMaxSize;
                
                if (needToSpawn)
                {
                    var recruit = barrack.RecruitSpawner.Spawn()
                        .With(x => x.SetParent(barrack.transform))
                        .With(x => x.Damage = barrack.Config.UnitAttackDamage)
                        .With(x => x.Duelable.Damageable.MaxHP = barrack.Config.UnitMaxHealth)
                        .With(x => x.Duelable.Damageable.CurrentHP = barrack.Config.UnitMaxHealth)
                        .With(x => x.DamagePeriod = barrack.Config.UnitAttackCooldown);
                    
                    
                    barrack.RecruitGroup.AddRecruit(recruit);
                    barrack.Timer.ResetTime(barrack.Config.RespawnCooldown);
                }
            }

            return this;
        }
    }
}