using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruit : Unit
{
    #region State Machine Variables

    protected RecruitIdleState _recruitIdleState;
    protected UnitAttackState _attackState;
    protected UnitAgroState _agroState;

    #endregion

    private Vector3 _guardPosition;

    public Vector3 GuardPosition => _guardPosition;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        _recruitIdleState = new RecruitIdleState(this, _stateMachine);
        _attackState = new UnitAttackState(this, _stateMachine);
        _agroState = new UnitAgroState(this, _stateMachine);

        _stateMachine.Initialize(_recruitIdleState);
    }

    public void UpdatePosition(Vector3 position)
    {
        _guardPosition = position;
        _stateMachine.ChangeState(_recruitIdleState);
    }
}
