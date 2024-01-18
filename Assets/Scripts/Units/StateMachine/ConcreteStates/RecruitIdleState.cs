using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitIdleState : State<Recruit>
{
    public RecruitIdleState(Recruit context, StateMachine stateMachine) : base(context, stateMachine)
    {
    }

    public override void EnterState()
    {
        if(context.transform.position != context.GuardPosition)
            context.NavPathFollower.MoveTo(context.GuardPosition);
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        
    }
}
