using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    #region State Machine Variables

    protected UnitSplineMoveState _splineMoveState;
    protected UnitAttackState _attackState;
    protected UnitAgroState _agroState;

    #endregion

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        _splineMoveState = new UnitSplineMoveState(this, _stateMachine);
        _attackState = new UnitAttackState(this, _stateMachine);
        _agroState = new UnitAgroState(this, _stateMachine);

        _stateMachine.Initialize(_splineMoveState);
    }
}
