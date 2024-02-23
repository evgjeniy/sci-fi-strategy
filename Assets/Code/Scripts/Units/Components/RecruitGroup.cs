using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class RecruitGroup : MonoBehaviour
    {
        [SerializeField] private List<Recruit> _predefinedRecruits;
        [SerializeField] private int _squadMaxSize = 3;

        public int squadMaxSize => _squadMaxSize;

        [field: SerializeField] public GuardPost GuardPost { get; set; }

        private readonly List<Recruit> _recruits = new();

        public event Action OnRecruitRemoved;
        public event Action OnGroupEmpty;

        public List<Recruit> Recruits => _recruits;

        private void Start()
        {
            foreach (var recruit in _predefinedRecruits)
            {
                AddRecruit(recruit);
            }
        }

        private void OnEnable()
        {
            GuardPost.OnValuesChanged += UpdateRecruits;
        }

        private void OnDisable()
        {
            GuardPost.OnValuesChanged -= UpdateRecruits;
        }

        public bool AddRecruit(Recruit recruit)
        {
            if (recruit == null || _recruits.Count >= _squadMaxSize) return false;
            if (_recruits.Contains(recruit)) return false;

            _recruits.Add(recruit);
            recruit.Duelable.Damageable.OnDied += RecruitDied;

            UpdateRecruits();
            return true;
        }

        public void RemoveRecruit(Recruit recruit)
        {
            if (recruit == null) return;
            if (!_recruits.Contains(recruit)) return;

            recruit.Duelable.Damageable.OnDied -= RecruitDied;

            _recruits.Remove(recruit);

            OnRecruitRemoved?.Invoke();

            if (_recruits.Count == 0) OnGroupEmpty?.Invoke();

            UpdateRecruits();
        }

        private void RecruitDied(Damageble recruit)
        {
            recruit.TryGetComponent<Recruit>(out var rec);

            if (rec != null)
                RemoveRecruit(rec);
        }

        [Button("Update")]
        public void UpdateRecruits()
        {
            Vector3[] positions = GetGuardPositions();

            for (int i = 0; i < _recruits.Count; i++)
            {
                _recruits[i].UpdatePosition(positions[i]);
            }
        }

        private Vector3[] GetGuardPositions()
        {
            List<Vector3> positions = new List<Vector3>(_recruits.Count);

            for (float i = 0; i < Mathf.PI * 2; i += Mathf.PI * 2 / _recruits.Count)
            {
                positions.Add(new Vector3(
                    GuardPost.Position.x + GuardPost.Radius * Mathf.Sin(i),
                    GuardPost.Position.y,
                    GuardPost.Position.z + GuardPost.Radius * Mathf.Cos(i)
                ));
            }

            return positions.ToArray();
        }

        private void OnDrawGizmos()
        {
            foreach (var position in GetGuardPositions())
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(position, Vector3.one);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(GuardPost.Position, GuardPost.Radius);
            }
        }
    }
}