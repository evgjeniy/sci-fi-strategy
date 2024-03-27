using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SustainTheStrain.Buildings.FSM
{
    public class Area<TComponent> where TComponent : Component
    {
        private readonly Func<Vector3> _getPosition;
        private readonly Func<float> _getRadius;
        private readonly Func<int> _getLayerMask;

        private readonly Dictionary<Collider, TComponent> _entities = new(16);
        private readonly Collider[] _buffer = new Collider[32];
        private int _bufferSize;

        public IReadOnlyCollection<TComponent> Entities => _entities.Values;

        public Area(Func<Vector3> getPosition, Func<float> getRadius, Func<int> getLayerMask)
        {
            _getPosition = getPosition;
            _getRadius = getRadius;
            _getLayerMask = getLayerMask;
        }

        public void Update()
        {
            Array.Clear(_buffer, 0, _bufferSize);
            _bufferSize = Physics.OverlapSphereNonAlloc(_getPosition(), _getRadius(), _buffer, _getLayerMask());

            for (var i = 0; i < _bufferSize; i++)
                if (!_buffer[i].TryGetComponent<TComponent>(out var component))
                    if (!_entities.ContainsKey(_buffer[i]))
                        _entities.Add(_buffer[i], component);
            
        }

        private bool RemoveCondition(TComponent component)
        {
            return component == null || !_buffer.Contains(component.GetComponent<Collider>());
        }
    }
}