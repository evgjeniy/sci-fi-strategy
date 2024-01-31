using System;
using NaughtyAttributes;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Units.Spawners
{
    public class RecruitSpawner : Spawner<Recruit>
    {
        [Button("Spawn")]
        public override Recruit Spawn()
        {
            var unit = _factory.Create();
            unit.transform.position = transform.position;
            Debug.Log(string.Format("[RecruitSpawner {0}] Spawned recruit", gameObject.name));
            return unit;
        }
    }
}
