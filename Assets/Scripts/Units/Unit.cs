using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Damageble))]
public class Unit : MonoBehaviour
{
    protected StateMachine _stateMachine = new StateMachine();

    public Damageble Damageble { get; protected set; }
    public AggroRadiusCheck AggroRadiusCheck { get; protected set;}
    public AttackRadiusCheck AttackRadiusCheck { get; protected set;}
    public StateMachine StateMachine => _stateMachine;
    public NavPathFollower NavPathFollower { get; protected set; }
    public Unit Opponent { get; protected set; }

    public bool IsOpponentInAggroZone { get; protected set; }
    public bool IsOpponentInAttackZone { get; protected set; }


    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Damageble = GetComponent<Damageble>();

        AggroRadiusCheck = GetComponentInChildren<AggroRadiusCheck>();
        AggroRadiusCheck.OnUnitEnteredAggroZone += UnitEneterdAggroZone;
        AggroRadiusCheck.OnUnitLeftAggroZone += UnitLeftAggroZone;

        AttackRadiusCheck = GetComponentInChildren<AttackRadiusCheck>();
        AttackRadiusCheck.OnUnitEnteredAttackZone += UnitEneterdAttackZone;
        AttackRadiusCheck.OnUnitLeftAttackZone += UnitLeftAttackZone;

        NavPathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());
    }

    #region UNIT_TRIGGER_LOGIC

    private void UnitEneterdAggroZone(Unit unit)
    {
        if (Opponent != null) return;
    }

    private void UnitLeftAggroZone(Unit unit)
    {
        IsOpponentInAggroZone = Opponent == unit;
    }

    private void UnitEneterdAttackZone(Unit unit)
    {
        IsOpponentInAttackZone = unit == Opponent;
    }

    private void UnitLeftAttackZone(Unit unit)
    {
        IsOpponentInAttackZone = unit != Opponent;
    }

    public bool IsDuelPossible()
    {
        return Opponent == null;
    }

    public void RequestDuel(Unit unit)
    {
        if(unit.IsDuelPossible())
        {
            unit.SetOpponent(this);
            SetOpponent(unit);
        }
    }

    public void SetOpponent(Unit unit)
    {
        Opponent = unit;
        unit.Damageble.OnDied += OpponentDead;
    }

    public void BreakDuel()
    {

    }

    public void RemoveOpponent()
    {

    }

    public void OpponentDead(Damageble damageble)
    {
        BreakDuel();
    }

    #endregion

    private void Update()
    {
        _stateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }
}
 