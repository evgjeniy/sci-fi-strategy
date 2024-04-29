using System.Collections.Generic;
using Dreamteck.Splines;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using UnityEngine.AI;
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

            if (unit == null) return null;

            unit.GetComponent<NavMeshAgent>().Warp(SpawnPosition);
            unit.GetComponent<SplineTracer>().spline = _spline;
            unit.GetComponent<SplineTracer>().RebuildImmediate();
            _spawnedEnemies.Add(unit);
            unit.GetComponent<Damageble>().OnDiedResult += (_, isSuicide) => 
            { 
                if(!isSuicide) _resourceManager.Gold.Value += unit.CoinsDrop;

                _spawnedEnemies.Remove(unit);
            };

            return unit;
        }

        public override Enemy Spawn()
        {
            throw new System.NotImplementedException();
        }
    }
}
