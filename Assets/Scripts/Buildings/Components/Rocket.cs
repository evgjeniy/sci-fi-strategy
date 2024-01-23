using System;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;
using SustainTheStrain.Units.Components;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.Components
{
    public class Rocket : Building
    {
        [Header("TEMP")]
        [SerializeField] private Projectile _projectilePrefab; // TODO: TEMP;
        [SerializeField] private GizmosData _gizmos;
        [SerializeField] private float _gizmosRadius;

        private PricedLevelStats<RocketData.Stats>[] _stats;
        private float _currentCooldown;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _stats = staticDataService.GetBuilding<RocketData>().RocketStats;
            CurrentUpgradeLevel = 0;
        }

        private void Update()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
                return;
            }

            Attack();
        }

        private void Attack()
        {
            var currentStats = _stats[CurrentUpgradeLevel].Stats;
            var results = Physics.OverlapSphere(transform.position, currentStats.AttackRadius);

            var damageableCounter = 0;
            foreach (var hitResult in results)
            {
                if (!hitResult.TryGetComponent<Damageble>(out var damageable)) continue;
                if (!IsInSector(damageable, currentStats.AttackSectorAngle)) continue;
                
                AttackEnemy(damageable, currentStats.Damage);
                _currentCooldown = currentStats.AttackCooldown;
                
                if (++damageableCounter == currentStats.MaxEnemiesTargets) break;
            }
        }

        private bool IsInSector(Component enemy, float sectorAngle)
        {
            var directionToEnemy = enemy.transform.position - transform.position;
            return Vector3.Angle(transform.forward, directionToEnemy) <= sectorAngle * 0.5f;
        }
        
        private async void AttackEnemy(Damageble enemy, float damage)
        {
            var projectileInstance = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            await projectileInstance.LaunchTo(enemy, d =>
            {
                Debug.Log($"{d.name} get {damage} damage by Laser");
                d.Damage(damage);
            });
        }

        private void OnDrawGizmos() => _gizmos.DrawSphere(transform.position, _gizmosRadius);
    }
}