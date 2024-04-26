using SustainTheStrain.Units.Components;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Damageble))]
    public class Unit : MonoBehaviour
    {
        [field:SerializeField] public float Damage { get; set; }
        [field:SerializeField] public float DamagePeriod { get; set; }

        [SerializeField] public Animator Animator;
        [SerializeField] public GameObject _afterDeath;
        public IPathFollower CurrentPathFollower { get; protected set; }   
        protected StateMachine.StateMachine _stateMachine = new();
    
        public Duelable Duelable { get; protected set; }

        [SerializeField] private float _aggroRadius = 5f;
        [SerializeField] private float _attackRadius = 5f;

        public Area<Duelable> AggroZone = new();
        public Area<Duelable> AttackZone = new();

        public StateMachine.StateMachine StateMachine => _stateMachine;
        public NavPathFollower NavPathFollower { get; protected set; }
        
        public bool IsOpponentInAttackZone { get; protected set; }

        private Timer _timer = new(0.2f);

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            Duelable = GetComponent<Duelable>();

            NavPathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());

            SwitchPathFollower(NavPathFollower);
        }

        #region UNIT_TRIGGER_LOGIC

        public virtual Duelable FindOpponent()
        {
            AggroZone.Update(transform.position, _aggroRadius, LayerMask.GetMask("Unit"));

            var enemies = AggroZone.Entities.
                Where((e) =>
                {
                    if (e.Damageable.Team == Duelable.Damageable.Team) return false;

                    if (Duelable.Opponent == null) return true;
                    
                    return e.Priority > Duelable.Opponent.Priority;
                }).
                OrderBy((e) => Vector3.Distance(e.transform.position, transform.position));

            if (enemies.Any()) 
                return enemies.First();
            
            return null;
        }

        public virtual bool CheckIfInAttackZone(Duelable duelable)
        {
            if(duelable == null) return false;

            AttackZone.Update(transform.position, _attackRadius, LayerMask.GetMask("Unit"));
            return duelable.IsIn(AttackZone);
        }

        public virtual bool CheckIfInAggroZone(Duelable duelable)
        {
            if (duelable == null) return false;

            AggroZone.Update(transform.position, _aggroRadius, LayerMask.GetMask("Unit"));
            return duelable.IsIn(AggroZone);
        }

        #endregion

        private void OnDestroy()
        {
            if(_afterDeath != null)
                Instantiate(_afterDeath, transform.position, Quaternion.identity);
        }

        public void SwitchPathFollower(IPathFollower pathFollower)
        {
            if (pathFollower == null) return;

            CurrentPathFollower?.Stop();
            CurrentPathFollower = pathFollower;
            CurrentPathFollower.Start();
        }

        public void Freeze()
        {
            CurrentPathFollower.Speed = 0;
            Debug.Log("FREEZED");
        }

        public void Unfreeze(float oldSpeed)
        {
            CurrentPathFollower.Speed = oldSpeed;
            Debug.Log("UNFREEZ");
        }

        private void Update()
        {
            _timer.Tick();
            _stateMachine.CurrentState.FrameUpdate();

            if (_timer.IsTimeOver)
            {
                _stateMachine.CurrentState.PhysicsUpdate();
                _timer.ResetTime(0.2f);
            }
        }
    }
}
 