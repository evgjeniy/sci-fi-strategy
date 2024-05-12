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

        public Vector3 last = Vector3.zero;

        public bool IsOnSpline(out SplineSample resultSample)
        {
            SplineSample result = new SplineSample();
            var position = context.transform.position;
            _splineFollower.Project(position, ref result);
            Vector3 s;
            if (_splineFollower.motion.offset.x > 0)
                s = Vector3.Cross(result.forward, Vector3.down).normalized * _splineFollower.motion.offset.x;
            else
                s = Vector3.Cross(result.forward, Vector3.up).normalized * _splineFollower.motion.offset.x;

            var newPos = new Vector3(result.position.x + s.x, result.position.y + _splineFollower.motion.offset.y, result.position.z + s.z);

            result.position = newPos;
            resultSample = result;
            last = result.position;
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
