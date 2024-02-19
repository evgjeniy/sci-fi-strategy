using NaughtyAttributes;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Units.Spawners
{
    public class RecruitSpawner : Spawner<Recruit>
    {
        [Button("Spawn")]
        public override Recruit Spawn()
        {
            var unit = _factory.Create();
            unit.transform.position = SpawnPosition;
            Debug.Log($"[RecruitSpawner {name}] Spawned recruit");
            return unit;
        }

        public Recruit Spawn(BarrackData.Stats stats)
        {
            var newUnit = Spawn();
            newUnit.UpdateStats(stats);
            return newUnit;
        }
    }
}
