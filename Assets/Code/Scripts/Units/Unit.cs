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
        protected StateMachine.StateMachine _stateMachine = new StateMachine.StateMachine();
    
        public Duelable Duelable { get; protected set; }
        //public AggroRadiusCheck AggroRadiusCheck { get; protected set;}
        //public AttackRadiusCheck AttackRadiusCheck { get; protected set;}

        [SerializeField] private float _aggroRadius = 5f;
        [SerializeField] private float _attackRadius = 5f;

        public Area<Duelable> AggroZone = new Area<Duelable>();
        public Area<Duelable> AttackZone = new Area<Duelable>();

        public StateMachine.StateMachine StateMachine => _stateMachine;
        public NavPathFollower NavPathFollower { get; protected set; }

        public bool IsAnnoyed { get; protected set; } 
        public bool IsOpponentInAggroZone { get; protected set; }
        public bool IsOpponentInAttackZone { get; protected set; }

        private Timer _timer = new Timer(0.2f);

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            Duelable = GetComponent<Duelable>();

            //AggroRadiusCheck = GetComponentInChildren<AggroRadiusCheck>();
            //AggroRadiusCheck.OnUnitEnteredAggroZone += UnitEnteredAggroZone;
            //AggroRadiusCheck.OnUnitLeftAggroZone += UnitLeftAggroZone;

            //AttackRadiusCheck = GetComponentInChildren<AttackRadiusCheck>();
            //AttackRadiusCheck.OnUnitEnteredAttackZone += UnitEnteredAttackZone;
            //AttackRadiusCheck.OnUnitLeftAttackZone += UnitLeftAttackZone;

            NavPathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());

            SwitchPathFollower(NavPathFollower);
        }

        #region UNIT_TRIGGER_LOGIC

        public virtual Duelable FindOpponent()
        {
            AggroZone.Update(transform.position, _aggroRadius, LayerMask.GetMask("Unit"));

            var enemies = AggroZone.Entities.
                Where((e) =>
                    e.Damageable.Team != Duelable.Damageable.Team /*&&*/
                   /* Duelable.Opponent != null ? e.Priority > Duelable.Opponent.Priority : true*/).
                OrderBy((e) => Vector3.Distance(e.transform.position, transform.position));

            if (enemies.Count() > 0) 
                return enemies.First();
            else return null;
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

        //private void UnitEnteredAggroZone(Duelable unit)
        //{
        //    IsAnnoyed = true;
        //}

        //private void UnitLeftAggroZone(Duelable unit)
        //{
        //    IsAnnoyed = AggroRadiusCheck.AggroZoneUnits.Count != 0;

        //    IsOpponentInAggroZone = Duelable.Opponent == unit;
        //}

        //private void UnitEnteredAttackZone(Duelable unit)
        //{
        //    if (!Duelable.HasOpponent)
        //    {
        //        IsOpponentInAttackZone = false;
        //        return;
        //    }

        //    if(IsOpponentInAttackZone) return;

        //    IsOpponentInAttackZone = unit == Duelable.Opponent;
        //}

        //private void UnitLeftAttackZone(Duelable unit)
        //{
        //    if (!Duelable.HasOpponent) 
        //    {
        //        IsOpponentInAttackZone = false;
        //        return;
        //    }

        //    if(IsOpponentInAttackZone)
        //        IsOpponentInAttackZone = !(unit == Duelable.Opponent);
        //}

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

        private void FixedUpdate()
        {
            //_stateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
 