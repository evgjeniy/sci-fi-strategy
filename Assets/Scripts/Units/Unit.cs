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
    private AggroRadiusCheck aggroRadius;

    protected StateMachine _stateMachine = new StateMachine();

    public NavPathFollower NavPathFollower { get; protected set; }

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        CurrentHP = MaxHP;

        aggroRadius = new AggroRadiusCheck();
        aggroRadius.onUnitEnteredAgroZone += UnitEneterdAgroZone;
        aggroRadius.onUnitEnteredAgroZone += UnitLeftAgroZone;

        NavPathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());
    }
    private void UnitEneterdAgroZone(Unit unit)
    {
        throw new NotImplementedException();
    }

    private void UnitLeftAgroZone(Unit unit)
    {
        throw new NotImplementedException();
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
        Destroy(gameObject);
    }  
}
 