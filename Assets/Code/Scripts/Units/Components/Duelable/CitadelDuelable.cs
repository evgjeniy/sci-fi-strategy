using Dreamteck.Splines;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units
{
    public class CitadelDuelable : Duelable
    {
        private List<Duelable> _opponents = new List<Duelable>();
        
        [SerializeField]
        private List<Vector3> _duelOffsets;
        [SerializeField]
        private SplineComputer _duelSpline;

        private float _duelOffset = 0.05f;

        public override bool HasOpponent { get => _opponents.Count > 0; }
        public override Vector3 DuelPosition
        {
            get => transform.position;
        }

        public override Duelable Opponent { 
            get    
            {
                if(_opponents.Count > 0) return _opponents[0];
                else return null;
            }
        }

        public override Vector3 GetNearestDuelPosition(Vector3 position)
        {
            _duelSpline.Evaluate(0.5f);

            int d = _opponents.Count % 2 == 0 ? -1 : 1;

            return _duelSpline.Evaluate(0.5f + d * _duelOffset * _opponents.Count).position;

            //int minIndex = 0;
            //for (int i = 0; i < _duelOffsets.Count; i++)
            //    if (Vector3.Distance(position, transform.position + _duelOffsets[i]) < Vector3.Distance(position, transform.position + _duelOffsets[minIndex]))
            //        minIndex = i;
            //return transform.position + _duelOffsets[minIndex];
        }

        public override bool IsDuelPossible(Duelable initiator)
        {
            return initiator.Damageable.Team != Damageable.Team;
        }

        
        public override bool RequestDuel(Duelable dueler)
        {
            if (dueler.IsDuelPossible(this))
            {
                dueler.SetOpponent(this);
                SetOpponent(dueler);
                return true;
            }
            else return false;
        }

        public override void SetOpponent(Duelable dueler)
        {
            if(_opponents.Contains(dueler)) return;
            _opponents.Add(dueler);
            dueler.Damageable.OnDied += OpponentDead;
        }

        public override void BreakDuel()
        {
            if (_opponents.Count == 0) return;

            //dueler.RemoveOpponent(this);
            //RemoveOpponent(dueler);
        }

        public override void RemoveOpponent(Duelable dueler)
        {
            if (!_opponents.Contains(dueler)) return;

            dueler.Damageable.OnDied -= OpponentDead;
            _opponents.Remove(dueler);
        }

        private void OpponentDead(Damageble damageble)
        {
            BreakDuel();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var offset in _duelOffsets)
            {
                Gizmos.DrawWireSphere(transform.position + offset, 0.5f);
            }
        }
    }
}
