using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Abilities.New
{
    public class ChainUpdate : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private List<Collider> _colliders;
        private float _lifeTime;
        private float _startTime;

        public void SetData(float time, List<Collider> colliders)
        {
            _lifeTime = time;
            _colliders = colliders;
        }

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _startTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - _startTime > _lifeTime)
            {
                End();
                return;
            }
            if (_lineRenderer == null || _colliders == null) return;
            for (var i = 0; i < _colliders.Count; i++)
            {
                _lineRenderer.SetPosition(i, _colliders[i] == null
                        ? _lineRenderer.GetPosition(GetIndex(_colliders.Count, i))
                        : _colliders[i].transform.position);
                
            }
        }

        private static int GetIndex(int size, int index)
        {
            if (size == 1) return 0;
            return index == 0 ? 1 : index - 1;
        }

        private void End() => Destroy(gameObject);
    }
}
