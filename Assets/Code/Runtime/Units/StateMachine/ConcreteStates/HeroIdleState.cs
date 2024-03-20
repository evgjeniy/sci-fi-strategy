using UnityEngine;

namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class HeroIdleState : State<Hero>
    {
        private IState _aggroState;
        
        public HeroIdleState(Hero context, StateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public void Init(IState aggroState)
        {
            _aggroState = aggroState;
        }
        
        public override void EnterState()
        {
            Debug.Log(string.Format("[StateMachine {0}] HeroIdleState entered", context.gameObject.name));

            context.SwitchPathFollower(context.NavPathFollower);
            context.CurrentPathFollower.Stop();
        }

        public override void ExitState()
        {
            context.CurrentPathFollower.Stop();
        }

        public override void FrameUpdate()
        {
            if(context.IsAnnoyed && !context.Duelable.HasOpponent) InitiateDuel();

            if (context.Duelable.HasOpponent) context.StateMachine.ChangeState(_aggroState);
        }

        public override void PhysicsUpdate()
        {
            
        }
        
        private void InitiateDuel()
        {
            if (context.AggroRadiusCheck.AggroZoneUnits.Count == 0) return;

            foreach (var unit in context.AggroRadiusCheck.AggroZoneUnits)
            {
                if (context.Duelable.RequestDuel(unit))
                    break;
            }
        }
    }
}