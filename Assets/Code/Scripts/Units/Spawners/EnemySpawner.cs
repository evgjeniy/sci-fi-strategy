using System.Collections.Generic;
using Dreamteck.Splines;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Units.Spawners
{
    public class EnemySpawner : Spawner<Enemy>
    {
        [SerializeField] protected SplineComputer _spline;

        private ResourceManager _resourceManager;
        private EnemyFactoryManager _enemyFactoryManager;

        private List<Enemy> _spawnedEnemies = new();

        public int SpawnedEnemiesAlive => _spawnedEnemies.Count;

        [Inject]
        private void Construct(EnemyFactoryManager factoryManager, ResourceManager resourceManager)
        {
            _enemyFactoryManager = factoryManager;
            _resourceManager = resourceManager;
        }
        

        public Enemy Spawn(string type)
        {
            var unit = _enemyFactoryManager.CreateEnemy(type);

            if (unit == null)
            {
                //Debug.LogError($"[EnemySpawner {name}] Enemy spaw failed");
                return null;
            }

            unit.transform.position = SpawnPosition;
            unit.GetComponent<SplineTracer>().spline = _spline;
            unit.GetComponent<SplineTracer>().RebuildImmediate();
            _spawnedEnemies.Add(unit);
            unit.GetComponent<Damageble>().OnDied += (Damageble d) => { _resourceManager.CurrentGold += unit.CoinsDrop;
                _spawnedEnemies.Remove(unit);
            };
            var agent = unit.GetComponent<NavMeshAgent>();;
            //Debug.Log($"[EnemySpawner {name}] Spawned unit");
            return unit;
        }

        public override Enemy Spawn()
        {
            throw new System.NotImplementedException();
        }
    }
}
