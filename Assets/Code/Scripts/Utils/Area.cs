using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SustainTheStrain
{
    public class Area<TComponent> where TComponent : Component
    {
        private readonly Dictionary<Collider, TComponent> _entities = new(16);
        private readonly Collider[] _buffer = new Collider[32];
        private readonly Func<TComponent, bool>[] _conditions;
        
        private int _bufferSize;

        public Area(params Func<TComponent, bool>[] conditions) => _conditions = conditions;

        public IReadOnlyCollection<TComponent> Entities => _entities.Values;

        public void Update(Vector3 center, float radius, LayerMask mask)
        {
            Array.Clear(_buffer, 0, _bufferSize);
            _bufferSize = Physics.OverlapSphereNonAlloc(center, radius, _buffer, mask);
            
            for (var i = 0; i < _bufferSize; i++)
            {
                if (_entities.ContainsKey(_buffer[i])) continue;
                if (_buffer[i].TryGetComponent<TComponent>(out var component) is false) continue;
                if (_conditions.Any(condition => condition(component) is false)) continue;

                _entities.Add(_buffer[i], component);
            }

            var collidersToRemove = _entities.Keys.Where(c => c == null || !_buffer.Contains(c)).ToArray();
            foreach (var collider in collidersToRemove) _entities.Remove(collider);
        }
    }
}