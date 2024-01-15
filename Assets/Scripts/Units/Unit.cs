using Dreamteck.Splines;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, IDamageble
{
    [field: SerializeField] 
    public float MaxHP { get; protected set; }
    [field: SerializeField] 
    public float CurrentHP { get; protected set; }
    [field: SerializeField]
    public int Team { get; protected set; }
    [field: SerializeField]
    public Unit AgroUnit { get; protected set; }
    [SerializeField]
    private float _agroCheckDelay;

    #region State Machine Variables

    protected StateMachine<Unit> _stateMachine = new StateMachine<Unit>(); 

    #endregion

    private IPathFollower currentPathFollower;
    private float _agroRadius;

    public NavPathFollower NavPathFollower { get; protected set; }
    public SplinePathFollower SplinePathFollower { get; protected set; }

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        CurrentHP = MaxHP;

        SplinePathFollower = new SplinePathFollower(GetComponent<SplineFollower>());
        NavPathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());
    }

    private void Update()
    {
        _stateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }

    [Button("Stop")]
    public void StopMovement()
    {
        currentPathFollower?.Stop();
    }

    [Button("Start")]
    public void StartMovement()
    {
        
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

    private IEnumerator CheckForAgro()
    {
        while (AgroUnit == null)
        {
            foreach (var hit in Physics.SphereCastAll(transform.position, _agroRadius, transform.forward))
            {
                if (hit.collider.TryGetComponent<Unit>(out var unit) && unit.Team != this.Team)
                {
                    AgroUnit = unit;
                    yield break;
                }
            }
            yield return new WaitForSeconds(_agroCheckDelay);
        }
    }
}
 