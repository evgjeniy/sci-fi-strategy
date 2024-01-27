using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Area
    {
        private readonly Rocket _rocket;

        private readonly List<Collider> _entities = new(16);
        private readonly Collider[] _buffer = new Collider[32];
        private int _bufferSize;

        public IReadOnlyCollection<Collider> Entities => _entities;

        public Area(Rocket rocket) => _rocket = rocket;

        public void Update()
        {
            Array.Clear(_buffer, 0, _bufferSize);
            _bufferSize = Physics.OverlapSphereNonAlloc
            (
                _rocket.transform.position,
                _rocket.CurrentStats.AttackRadius,
                _buffer, _rocket.Data.AttackMask.value
            );
            
            for (var i = 0; i < _bufferSize; i++)
                if (!_entities.Contains(_buffer[i])) 
                    _entities.Add(_buffer[i]);

            _entities.RemoveAll(RemoveCondition);
        }

        private bool RemoveCondition(Collider collider) => collider == null || !_buffer.Contains(collider);
    }
}