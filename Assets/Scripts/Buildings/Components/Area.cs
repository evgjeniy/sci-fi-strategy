using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Area
    {
        private readonly Func<Vector3> _getPosition;
        private readonly Func<float> _getRadius;
        private readonly Func<int> _getLayerMask;

        private readonly List<Collider> _entities = new(16);
        private readonly Collider[] _buffer = new Collider[32];
        private int _bufferSize;

        public IReadOnlyCollection<Collider> Entities => _entities;

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
                if (!_entities.Contains(_buffer[i]))
                    _entities.Add(_buffer[i]);

            _entities.RemoveAll(RemoveCondition);
        }

        private bool RemoveCondition(Component component) => component == null || !_buffer.Contains(component);
    }
}