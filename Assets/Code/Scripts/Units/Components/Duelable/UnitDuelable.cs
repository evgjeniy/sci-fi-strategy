using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace SustainTheStrain.Units
{
    public class UnitDuelable : Duelable
    {
        
        private Queue<Duelable> _opponents = new Queue<Duelable>();
        
        [SerializeField] private Vector3[] _duelOffsets = new Vector3[3];
        
        public override bool HasOpponent => !_opponents.IsEmpty();

        public override Vector3 DuelPosition => transform.position + _duelOffsets[_opponents.Count];
        public override Duelable Opponent => _opponents.Peek();
        
        public override Vector3 GetNearestDuelPosition(Vector3 position, Duelable requester)
        {
            return transform.position + _duelOffsets[_opponents.Count-1];
        }

        public override bool IsDuelPossible(Duelable initiator)
        {
            return initiator.Damageable.Team != Damageable.Team && !initiator.Damageable.IsFlying && _opponents.Count<3;
        }

        public override bool RequestDuel(Duelable dueler)
        {
            if (dueler.IsDuelPossible(this))
            {
                
                //if(_opponent != null) BreakDuel();
                
                dueler.SetOpponent(this);
                SetOpponent(dueler);
                return true;
            }
            return false;
        }

        public override void SetOpponent(Duelable dueler)
        {
            if (!_opponents.Contains(dueler))
            {
                _opponents.Enqueue(dueler);
                dueler.Damageable.OnDied += OpponentDead;
            }
            //_opponent = dueler;
            //dueler.Damageable.OnDied += OpponentDead;
        }

        public override void BreakDuel()
        {
            if (!HasOpponent) return;

            _opponents.Peek().RemoveOpponent(this);
            RemoveOpponent(_opponents.Peek());
        }

        public override void RemoveOpponent(Duelable dueler)
        {
            if (!HasOpponent) return;
            
            _opponents.Dequeue().Damageable.OnDied -= OpponentDead;
        }

        private void OpponentDead(Damageble damageble)
        {
            if (HasOpponent)
            {
                BreakDuel();
            }

            if (_opponents.Count > 1)
            {
                _opponents.Peek().RequestDuel(this);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(DuelPosition, 0.5f);
        }
    }
}
