using System.Collections.Generic;
using Dreamteck.Splines;
using SustainTheStrain.ResourceSystems;
using SustainTheStrain.Units.Components;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units.Spawners
{
    public class EnemySpawner : Spawner<Enemy>
    {
        [SerializeField] protected SplineComputer _spline;
        [Inject] private ResourceManager _resourceManager;

        private List<Enemy> _spawnedEnemies = new();

        public int SpawnedEnemiesAlive => _spawnedEnemies.Count;
        
        public override Enemy Spawn()
        {
            var unit = _factory.Create();
            unit.transform.position = SpawnPosition;
            unit.GetComponent<SplineFollower>().spline = _spline;
            unit.GetComponent<SplineFollower>().RebuildImmediate();
            _spawnedEnemies.Add(unit);
            unit.GetComponent<Damageble>().OnDied += (Damageble d) => { _resourceManager.CurrentGold += unit._coinsDrop;
                _spawnedEnemies.Remove(unit);
            };
            Debug.Log($"[EnemySpawner {name}] Spawned unit");
            return unit;
        }
    }
}
