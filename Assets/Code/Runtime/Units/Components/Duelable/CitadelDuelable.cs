using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units
{
    public class CitadelDuelable : Duelable
    {
        private List<Duelable> _opponents = new List<Duelable>();
        
        [SerializeField]
        private Vector3 _duelOffset;
        public override bool HasOpponent { get => _opponents.Count > 0; }
        public override Vector3 DuelPosition
        {
            get => transform.position + _duelOffset;
        }

        public override Duelable Opponent { 
            get    
            {
                if(_opponents.Count > 0) return _opponents[0];
                else return null;
            }
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
            Gizmos.DrawWireSphere(DuelPosition, 0.5f);
        }
    }
}
