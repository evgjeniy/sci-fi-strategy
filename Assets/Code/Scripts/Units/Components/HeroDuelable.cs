namespace SustainTheStrain.Units
{
    public class HeroDuelable : UnitDuelable
    {
        private Hero _hero;
        
        protected override void Init()
        {
            base.Init();

            _hero = GetComponent<Hero>();
        }
        
        public override bool IsDuelPossible(Duelable initiator)
        {
            return _opponent == null && initiator.Damageble.Team != Damageble.Team && !_hero.IsMoving;
        }
    }
}