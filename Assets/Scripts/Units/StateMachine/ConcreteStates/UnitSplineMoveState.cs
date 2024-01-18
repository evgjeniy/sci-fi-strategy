using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSplineMoveState : State<Unit>
{
    private bool _isOnSpline = false;
    private SplineFollower _splineFollower;

    public UnitSplineMoveState(Unit context, StateMachine stateMachine) : base(context, stateMachine) 
    {
        _splineFollower = context.GetComponent<SplineFollower>();
    }

    public override void EnterState()
    {
        if(IsOnSpline(out var splineSample))
        {
            _isOnSpline = true;
            context.NavPathFollower.MoveTo(splineSample.position);
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
        if(!_isOnSpline)
            if (context.NavPathFollower.IsDestinationReached())
                context.SplinePathFollower?.Start();
    }

    public bool IsOnSpline(out SplineSample resultSample)
    {
        SplineSample result = new SplineSample();
        _splineFollower.Project(context.transform.position, ref result);

        resultSample = result;

        return context.transform.position == result.position;
    }

    public override void PhysicsUpdate()
    {

    }
}
