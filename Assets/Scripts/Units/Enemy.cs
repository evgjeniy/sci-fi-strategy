using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    #region State Machine Variables

    protected EnemySplineMoveState _splineMoveState;
    protected UnitAttackState _attackState;
    protected UnitAgroState _aggroState;

    #endregion

    public SplinePathFollower SplinePathFollower { get; protected set; }

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        if (TryGetComponent<SplineFollower>(out var splineFollower))
            SplinePathFollower = new SplinePathFollower(splineFollower);

        _splineMoveState = new EnemySplineMoveState(this, _stateMachine);
        _attackState = new UnitAttackState(this, _stateMachine);
        _aggroState = new UnitAgroState(this, _stateMachine);
        _splineMoveState.Init(_aggroState);
        _aggroState.Init(_attackState, _splineMoveState);

        _stateMachine.Initialize(_splineMoveState);
    }
}
