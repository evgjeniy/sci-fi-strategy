using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSplineMoveState : State<Unit>
{
    private bool _isOnSpline = false;

    public UnitSplineMoveState(Unit context, StateMachine<Unit> stateMachine) : base(context, stateMachine) {}

    public override void EnterState()
    {
        SplineSample result = new SplineSample();
        context.GetComponent<SplineFollower>().Project(context.transform.position, ref result);

        _isOnSpline = context.transform.position == result.position;

        if(!_isOnSpline)
        {
            context.NavPathFollower.MoveTo(result.position);
        }
        else
        {
            context.SplinePathFollower.Start();
        }
    }

    public override void ExitState()
    {
        context.SplinePathFollower.Stop();
    }

    public override void FrameUpdate()
    {
        if (!context.GetComponent<NavMeshAgent>().pathPending)
        {
            if (context.GetComponent<NavMeshAgent>().remainingDistance <= context.GetComponent<NavMeshAgent>().stoppingDistance)
            {
                if (!context.GetComponent<NavMeshAgent>().hasPath || context.GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
                {
                    context.SplinePathFollower.Start();
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {

    }
}
