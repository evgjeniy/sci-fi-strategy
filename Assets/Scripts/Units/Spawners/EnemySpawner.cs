using Dreamteck.Splines;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Spawners
{
    public class EnemySpawner : Spawner<Enemy>
    {
        [SerializeField] private int _spawnCount;
        [SerializeField] protected float _spawnPeriod;
        [SerializeField] protected SplineComputer _spline;

        private Coroutine _spawnCoroutine;

        private void Start()
        {
            StartSpawning();
        }

        public override void Spawn()
        {
            var unit = _factory.Create();
            unit.GetComponent<SplineFollower>().spline = _spline;
            unit.GetComponent<SplineFollower>().RebuildImmediate();
            Debug.Log(string.Format("[EnemySpawner {0}] Spawned unit", gameObject.name));
        }

        [Button("Spawn")]
        public void StartSpawning()
        {
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);

            _spawnCoroutine = StartCoroutine(SpawnWithPeriod());
        }

        public IEnumerator SpawnWithPeriod()
        {
            for(int i = 0; i < _spawnCount; i++)
            {
                Spawn();
                yield return new WaitForSeconds(_spawnPeriod);
            }
        }
    }
}
