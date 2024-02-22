using UnityEngine;

namespace SustainTheStrain.Units
{
    public class UnitDuelable : Duelable
    {
        protected Duelable _opponent;
        [SerializeField]
        private Vector3 _duelOffset;
        public override bool HasOpponent => Opponent != null;
        public override Vector3 DuelPosition => transform.position + _duelOffset;
        public override Duelable Opponent => _opponent;

        public override bool IsDuelPossible(Duelable initiator)
        {
            return !HasOpponent && initiator.Damageble.Team != Damageble.Team;
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
            _opponent = dueler;
            dueler.Damageble.OnDied += OpponentDead;
        }

        public override void BreakDuel()
        {
            if (_opponent == null) return;

            _opponent.RemoveOpponent(this);
            RemoveOpponent(_opponent);
        }

        public override void RemoveOpponent(Duelable dueler)
        {
            _opponent.Damageble.OnDied -= OpponentDead;
            _opponent = null;
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
