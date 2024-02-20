using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Units.Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Unit
    {
        [SerializeField] private GizmosData _sphereGizmos;
        [SerializeField] private Transform _spawnPositon;
        [Inject] protected IFactory<T> _factory;

        public Vector3 SpawnPosition
        {
            get => _spawnPositon == null ? transform.position : _spawnPositon.position;
            set => _spawnPositon.position = value;
        }

        private void OnDrawGizmos() => _sphereGizmos.DrawSphere(SpawnPosition, 1.0f);
        
        public abstract T Spawn();
    }
}
