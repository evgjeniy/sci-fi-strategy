using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class EnemySplineMoveState : State<Enemy>
    {
        private IState _aggroState;
        private bool _isOnSpline = false;
        private SplineFollower _splineFollower;

        public EnemySplineMoveState(Enemy context, StateMachine stateMachine) : base(context, stateMachine) 
        {
            _splineFollower = context.GetComponent<SplineFollower>();
        }

        public void Init(IState aggroState)
        {
            _aggroState = aggroState;
        }

        public override void EnterState()
        {
            Debug.Log(string.Format("[StateMachine {0}] EnemySplineMoveState entered", context.gameObject.name));
            SwitchNavigation();
        }

        public override void ExitState()
        {
            context.CurrentPathFollower.Stop();
        }

        public override void FrameUpdate()
        {
            if (!_isOnSpline)
                if (context.NavPathFollower.IsDestinationReached())
                {
                    _isOnSpline = true;
                    context.SwitchPathFollower(context.SplinePathFollower);
                }

            if (context.Duelable.HasOpponent) context.StateMachine.ChangeState(_aggroState);   
        }

        public bool IsOnSpline(out SplineSample resultSample)
        {
            SplineSample result = new SplineSample();
            var position = context.transform.position;
            _splineFollower.Project(position, ref result);

            resultSample = result;

            var newPos = position + (_splineFollower.motion.offset.x * result.forward);
            
            //var newPos = new Vector3(resultSample.position.x + _splineFollower.motion.offset.x, resultSample.position.y, resultSample.position.z);
            position = newPos;
            //context.transform.position = position;

            resultSample.position = newPos;
            return position == result.position;
        }

        public override void PhysicsUpdate()
        {
            context.FindOpponent().IfNotNull(duelable => context.Duelable.RequestDuel(duelable));
        }

        private void SwitchNavigation()
        {
            if (!IsOnSpline(out var splineSample))
            {
                _isOnSpline = false;
                context.SwitchPathFollower(context.NavPathFollower);
                context.NavPathFollower.MoveTo(splineSample.position);
            }
            else
            {
                context.SwitchPathFollower(context.SplinePathFollower);
            }
        }
    }
}
