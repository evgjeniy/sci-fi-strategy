using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class BarrackIdleState : IBarrackState
    {
        public IBarrackState Update(Barrack barrack)
        {
            var barrackData = barrack.Data;
            var barrackConfig = barrackData.Config.Value;
            
            barrackData.Timer.Time -= Time.deltaTime;

            if (barrackData.Timer.IsTimeOver)
            {
                barrackData.RecruitGroup.Recruits.RemoveAll(x => x == null);

                var needToSpawn = barrackData.RecruitGroup.Recruits.Count < barrackData.RecruitGroup.squadMaxSize;
                
                if (needToSpawn)
                {
                    var recruit = barrackData.RecruitSpawner.Spawn()
                        .With(x => x.SetParent(barrack.transform))
                        .With(x => x.Damage = barrackConfig.UnitAttackDamage)
                        .With(x => x.Duelable.Damageable.MaxHP = barrackConfig.UnitMaxHealth)
                        .With(x => x.Duelable.Damageable.CurrentHP = barrackConfig.UnitMaxHealth)
                        .With(x => x.DamagePeriod = barrackConfig.UnitAttackCooldown);
                    
                    
                    barrackData.RecruitGroup.AddRecruit(recruit);
                    
                    barrackData.Timer.Time = barrackConfig.RespawnCooldown;
                }
            }

            return this;
        }
    }
}