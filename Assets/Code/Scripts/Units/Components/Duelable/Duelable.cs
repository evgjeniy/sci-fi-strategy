using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Units
{
    public abstract class Duelable : MonoBehaviour, IDuelable
    {
        [field: SerializeField]
        public int Priority { get; protected set; }

        public IDamageable Damageable { get; protected set; }

        public abstract Duelable Opponent { get; }
        public abstract bool HasOpponent { get; }
        public abstract Vector3 DuelPosition { get; }

        private void OnEnable()
        {
            Init();
        }

        protected virtual void Init()
        {
            Damageable = GetComponent<IDamageable>();
        }

        public abstract Vector3 GetNearestDuelPosition(Vector3 position, Duelable requester);
        
        public abstract bool IsDuelPossible(Duelable initiator);
        public abstract bool RequestDuel(Duelable dueler);
        public abstract void BreakDuel();
        public abstract void SetOpponent(Duelable dueler);
        public abstract void RemoveOpponent(Duelable dueler);
    }
}
