using NaughtyAttributes;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace SustainTheStrain.Units.Spawners
{
    public class RecruitSpawner : Spawner<Recruit>
    {
        private IFactory<Recruit> _factory;

        [Inject]
        private void Construct(IFactory<Recruit> factory)
        {
            _factory = factory;
        }

        [Button("Spawn")]
        public override Recruit Spawn()
        {
            var unit = _factory.Create();
            unit.GetComponent<NavMeshAgent>().Warp(SpawnPosition);
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
