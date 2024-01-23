using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;
using SustainTheStrain.Units.Components;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.Components
{
    public class Laser : Building
    {
        [Header("TEMP")]
        [SerializeField] private GizmosData _gizmos;
        [SerializeField] private float _gizmosRadius;
        
        private PricedLevelStats<LaserData.Stats>[] _stats;
        private float _currentCooldown;
        
        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _stats = staticDataService.GetBuilding<LaserData>().LaserStats;
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

            foreach (var hitResult in results)
            {
                var hasComponent = hitResult.TryGetComponent<Damageble>(out var damageable);
                if (!hasComponent) continue;
                
                Debug.Log($"{damageable.name} get {currentStats.Damage} damage by Laser");
                damageable.Damage(currentStats.Damage);
                
                _currentCooldown = currentStats.AttackCooldown;
                break;
            }
        }

        private void OnDrawGizmos() => _gizmos.DrawSphere(transform.position, _gizmosRadius);
    }
}