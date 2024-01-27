using System;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;
using SustainTheStrain.Units.Components;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.Components
{
    public class Artillery : Building
    {
        [Header("TEMP")]
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private GizmosData _gizmos;
        [SerializeField] private float _gizmosRadius;

        private PricedLevelStats<ArtilleryData.Stats>[] _stats;
        private float _currentCooldown;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _stats = staticDataService.GetBuilding<ArtilleryData>().ArtilleryStats;
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
            
            foreach (var result in results)
            {
                if (!result.TryGetComponent<Damageble>(out var damageable)) continue;
                
                Attack(damageable);
                _currentCooldown = currentStats.AttackCooldown;
                
                break;
            }
        }

        private void Attack(Damageble damageable)
        {
            var projectileInstance = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            projectileInstance.LaunchTo(damageable, Explosion); // change moving logic (need's to be a parabola)
        }

        private void Explosion(Damageble obj)
        {
            var currentStats = _stats[CurrentUpgradeLevel].Stats;

            foreach (var result in Physics.OverlapSphere(obj.transform.position, currentStats.ExplosionRadius))
            {
                if (!result.TryGetComponent<Damageble>(out var damageable)) continue;
                
                Debug.Log($"{damageable.name} get {currentStats.Damage} damage by Explosion");
                
                damageable.Damage(currentStats.Damage);
            }
        }

        private void OnDrawGizmos() => _gizmos.DrawSphere(transform.position, _gizmosRadius);
    }
}