using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class UnitDuelable : Duelable
    {
        private Duelable _opponent;

        public override bool HasOpponent { get => Opponent; }
        public override Duelable Opponent { get => _opponent; }

        public override bool IsDuelPossible(Duelable initiator)
        {
            return _opponent == null && initiator.Damageble.Team != Damageble.Team;
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
    }
}
