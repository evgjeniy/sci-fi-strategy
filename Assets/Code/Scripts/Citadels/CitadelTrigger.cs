using System;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Citadels
{
    [RequireComponent(typeof(Collider))]
    public class CitadelTrigger : MonoBehaviour
    {
        public event Action<Damageble> OnEnemyTriggered;
        
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Damageble>(out var enemy))
            { 
                OnEnemyTriggered?.Invoke(enemy);
            }
        }
    }
}