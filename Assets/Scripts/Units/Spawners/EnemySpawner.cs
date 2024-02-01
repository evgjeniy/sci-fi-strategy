using Dreamteck.Splines;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Spawners
{
    public class EnemySpawner : Spawner<Enemy>
    {
        [SerializeField] protected SplineComputer _spline;

        public override Enemy Spawn()
        {
            var unit = _factory.Create();
            unit.transform.position = transform.position;
            unit.GetComponent<SplineFollower>().spline = _spline;
            unit.GetComponent<SplineFollower>().RebuildImmediate();
            Debug.Log(string.Format("[EnemySpawner {0}] Spawned unit", gameObject.name));
            return unit;
        }
    }
}
