using UnityEngine;

namespace SustainTheStrain.Units
{
    public class RecruitDuelable : UnitDuelable
    {
        private Recruit _recruit;
        private Duelable _opponent;
        
        protected override void Init()
        {
            base.Init();

            _recruit = GetComponent<Recruit>();
        }
        
        public override bool IsDuelPossible(Duelable initiator)
        {
            return _opponent == null && initiator.Damageable.Team != Damageable.Team && !_recruit.IsMoving && !initiator.Damageable.IsFlying;
        }

    }
}