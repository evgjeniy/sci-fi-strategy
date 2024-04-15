using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units
{
    public class EnemyFactoryManager
    {
        private Dictionary<string,IFactory<Enemy>> factories = new();

        public Dictionary<string, IFactory<Enemy>> Factories => factories;

        public Enemy CreateEnemy(string type)
        {
            if(factories.TryGetValue(type, out var factory))
            {
                return factory.Create();
            }
            else
                Debug.LogError($"[EnemyFactoryManager] Can't find factory for enemy with name: {type}");

            return null;
        }
    }
}
