using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class CitadelDuelable : Duelable
    {
        private List<Duelable> _opponents = new List<Duelable>();

        public override bool HasOpponent { get => _opponents.Count > 0; }
        public override Duelable Opponent { 
            get    
            {
                if(_opponents.Count > 0) return _opponents[0];
                else return null;
            }
        }

        public override bool IsDuelPossible(Duelable initiator)
        {
            return initiator.Damageble.Team != Damageble.Team;
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
            dueler.Damageble.OnDied += OpponentDead;
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

            dueler.Damageble.OnDied -= OpponentDead;
            _opponents.Remove(dueler);
        }

        private void OpponentDead(Damageble damageble)
        {
            BreakDuel();
        }
    }
}