using Dreamteck.Splines;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Extensions;

public class Unit : MonoBehaviour, IDamageble
{
    [field: SerializeField] 
    public float MaxHP { get; protected set; }
    [field: SerializeField] 
    public float CurrentHP { get; protected set; }
    [field: SerializeField]
    public int Team { get; protected set; }
    [SerializeField]
    private AggroRadiusCheck _aggroRadius;
    [SerializeField]
    private AttackRadiusCheck _attackRadius;

    protected StateMachine _stateMachine = new StateMachine();

    public StateMachine StateMachine => _stateMachine;
    public NavPathFollower NavPathFollower { get; protected set; }

    public Unit Opponent { get; protected set; }
    public bool IsOpponentInAttackZone { get; protected set; }

    public event Action<Unit> OnDead;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        CurrentHP = MaxHP;

        _aggroRadius = GetComponentInChildren<AggroRadiusCheck>();
        _aggroRadius.OnUnitEnteredAggroZone += UnitEneterdAggroZone;
        _aggroRadius.OnUnitLeftAggroZone += UnitLeftAggroZone;
        _attackRadius = GetComponentInChildren<AttackRadiusCheck>();
        _attackRadius.OnUnitEnteredAttackZone += UnitEneterdAttackZone;
        _attackRadius.OnUnitLeftAttackZone += UnitLeftAttackZone;

        NavPathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());
    }
    private void UnitEneterdAggroZone(Unit unit)
    {
        if (unit.Team == Team) return;

        if (unit.RequestDuel(this))
        {
            Opponent = unit;
            unit.OnDead += OpponentDead;
        }
    }

    private void OpponentDead(Unit unit)
    {
        BreakDuel();
        IsOpponentInAttackZone = false;
    }

    private void UnitLeftAggroZone(Unit unit)
    {
        if (unit == Opponent) BreakDuel();
    }

    private void UnitEneterdAttackZone(Unit unit)
    {
        if(Opponent == unit) IsOpponentInAttackZone = true;
    }

    private void UnitLeftAttackZone(Unit unit)
    {
        if (Opponent == unit) IsOpponentInAttackZone = false;
    }

    public bool RequestDuel(Unit unit)
    {
        if(Opponent != null) return false;

        Opponent = unit;
        return true;
    }

    public void BreakDuel()
    {
        Opponent = null;
    }

    private void Update()
    {
        _stateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }

    public void Damage(float damage)
    {
        CurrentHP -= damage;

        if (CurrentHP < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }  
}
 