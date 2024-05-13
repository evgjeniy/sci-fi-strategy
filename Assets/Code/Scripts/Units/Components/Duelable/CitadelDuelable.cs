using Dreamteck.Splines;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SustainTheStrain.Units
{
    public class CitadelDuelable : Duelable
    {
        private Dictionary<Duelable, Vector3> _positions = new Dictionary<Duelable, Vector3>();
        private List<Duelable> _opponents = new List<Duelable>();
        
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

        public override Vector3 GetNearestDuelPosition(Vector3 position, Duelable requester)
        {
            if(_positions.ContainsKey(requester)) return _positions[requester];

            _duelSpline.Evaluate(0.5f);

            int d = _opponents.Count % 2 == 0 ? -1 : 1;

            _positions.Add(requester, _duelSpline.Evaluate(Mathf.Clamp01(0.5f + d * _duelOffset * (_opponents.Count % (1f/ _duelOffset)))).position);


            return _positions[requester];
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
        }

        public override void RemoveOpponent(Duelable dueler)
        {
            if (!_opponents.Contains(dueler)) return;

            dueler.Damageable.OnDied -= OpponentDead;
            _positions.Remove(dueler);
            _opponents.Remove(dueler);
        }

        private void OpponentDead(Damageble damageble)
        {
            if (_opponents.Count == 0) return;

            var d = damageble.GetComponent<Duelable>();

            d.RemoveOpponent(this);
            RemoveOpponent(d);
        }
    }
}
