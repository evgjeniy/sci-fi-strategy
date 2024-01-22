using SustainTheStrain.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units.Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Unit
    {
        [SerializeField] protected Transform _spawnPositon;
        protected IFactory<T> _factory;

        [Inject]
        private void InitFactory(IFactory<T> factory)
        {
            _factory = factory;
        }

        public abstract void Spawn();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if (_spawnPositon != null)
                Gizmos.DrawSphere(_spawnPositon.position, 1f);
            else
                Gizmos.DrawSphere(transform.position, 1f);
        }
    }
}
