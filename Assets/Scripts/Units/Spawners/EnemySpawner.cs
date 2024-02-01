using Dreamteck.Splines;
using UnityEngine;

namespace SustainTheStrain.Units.Spawners
{
    public class EnemySpawner : Spawner<Enemy>
    {
        [SerializeField] protected SplineComputer _spline;

        public override Enemy Spawn()
        {
            var unit = _factory.Create();
            unit.transform.position = SpawnPosition;
            unit.GetComponent<SplineFollower>().spline = _spline;
            unit.GetComponent<SplineFollower>().RebuildImmediate();
            Debug.Log($"[EnemySpawner {name}] Spawned unit");
            return unit;
        }
    }
}
