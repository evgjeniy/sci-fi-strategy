using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public interface IDuelable
    {
        public int Priority { get; }
        public IDamageable Damageable { get; }
        public bool HasOpponent { get; }
        public Duelable Opponent { get; }
        public Vector3 DuelPosition { get; }
        
        public bool IsDuelPossible(Duelable initiator);
        public bool RequestDuel(Duelable dueler);
        public void BreakDuel();
    }
}
