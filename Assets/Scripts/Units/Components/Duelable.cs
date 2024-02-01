using SustainTheStrain.Units.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public abstract class Duelable : MonoBehaviour
    {
        [field: SerializeField]
        public int Priority { get; protected set; }

        public Damageble Damageble { get; protected set; }

        public abstract Duelable Opponent { get; }
        public abstract bool HasOpponent { get; }

        private void OnEnable()
        {
            Init();
        }

        protected virtual void Init()
        {
            Damageble = GetComponent<Damageble>();
        }

        public abstract bool IsDuelPossible(Duelable initiator);

        public abstract bool RequestDuel(Duelable dueler);

        public abstract void SetOpponent(Duelable dueler);

        public abstract void BreakDuel();

        public abstract void RemoveOpponent(Duelable dueler);
    }
}
