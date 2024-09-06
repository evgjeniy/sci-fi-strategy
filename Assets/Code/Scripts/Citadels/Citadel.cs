using System;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Citadels
{
    [RequireComponent(typeof(Damageble))]
    [RequireComponent(typeof(CitadelTrigger))]
    public class Citadel : MonoBehaviour
    {
        public Damageble Damageable { get; private set; }
        public CitadelTrigger Trigger { get; private set; }

        private void Awake()
        {
            Damageable = GetComponent<Damageble>();
            Trigger = GetComponent<CitadelTrigger>();
            Trigger.OnEnemyTriggered += KillEnemy;
        }

        private void KillEnemy(Damageble enemy)
        {
            if (enemy.Team != Team.Enemy) return;
            if (enemy.TryGetComponent<Enemy>(out var enemyUnit))
            {
                //TODO DAMAGE TYPE INSTALLER
                Damageable.Damage(enemyUnit.CitadelDamage, DamageType.Physical);
            }
            enemy.Kill();
        }
        
    }
}
