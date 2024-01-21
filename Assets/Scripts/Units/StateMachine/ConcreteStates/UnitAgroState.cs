using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class UnitAgroState : State<Unit>
{
    private IState _attackState;
    private IState _idleState;

    private float _disaggroTime = 0.5f;
    private float _time;

    public UnitAgroState(Unit context, StateMachine stateMachine) : base(context, stateMachine)
    {
    }

    public void Init(IState attackState, IState idleState)
    {
        _attackState = attackState;
        _idleState = idleState;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        context.NavPathFollower.Stop();
    }

    public override void FrameUpdate()
    {
        if(!context.IsOpponentInAggroZone && _time > _disaggroTime)
        {
            context.BreakDuel();
            return;
        }

        if (context.Opponent == null )
            context.StateMachine.ChangeState(_idleState);

        context.NavPathFollower.MoveTo(context.Opponent.transform.position);

        if(context.IsOpponentInAttackZone)
            context.StateMachine.ChangeState(_attackState);

        _time += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
    }
}
