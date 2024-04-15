using UnityEngine;
using UnityEngine.AI;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Damageble))]
    public class Unit : MonoBehaviour
    {
        [field:SerializeField] public float Damage { get; set; }
        [field:SerializeField] public float DamagePeriod { get; set; }

        [SerializeField] public Animator Animator;
        public IPathFollower CurrentPathFollower { get; protected set; }   
        protected StateMachine.StateMachine _stateMachine = new StateMachine.StateMachine();
    
        public Duelable Duelable { get; protected set; }
        public AggroRadiusCheck AggroRadiusCheck { get; protected set;}
        public AttackRadiusCheck AttackRadiusCheck { get; protected set;}
        public StateMachine.StateMachine StateMachine => _stateMachine;
        public NavPathFollower NavPathFollower { get; protected set; }

        public bool IsAnnoyed { get; protected set; } 
        public bool IsOpponentInAggroZone { get; protected set; }
        public bool IsOpponentInAttackZone { get; protected set; }

        //[Zenject.Inject]
        //private void Construct(UnitData unitData) 
        //{
            
        //}

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            Duelable = GetComponent<Duelable>();

            AggroRadiusCheck = GetComponentInChildren<AggroRadiusCheck>();
            AggroRadiusCheck.OnUnitEnteredAggroZone += UnitEneterdAggroZone;
            AggroRadiusCheck.OnUnitLeftAggroZone += UnitLeftAggroZone;

            AttackRadiusCheck = GetComponentInChildren<AttackRadiusCheck>();
            AttackRadiusCheck.OnUnitEnteredAttackZone += UnitEnteredAttackZone;
            AttackRadiusCheck.OnUnitLeftAttackZone += UnitLeftAttackZone;

            NavPathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());

            SwitchPathFollower(NavPathFollower);
        }

        #region UNIT_TRIGGER_LOGIC

        private void UnitEneterdAggroZone(Duelable unit)
        {
            IsAnnoyed = true;
        }

        private void UnitLeftAggroZone(Duelable unit)
        {
            IsAnnoyed = AggroRadiusCheck.AggroZoneUnits.Count != 0;

            IsOpponentInAggroZone = Duelable.Opponent == unit;
        }

        private void UnitEnteredAttackZone(Duelable unit)
        {
            if (!Duelable.HasOpponent)
            {
                IsOpponentInAttackZone = false;
                return;
            }
            
            if(IsOpponentInAttackZone) return;
            
            IsOpponentInAttackZone = unit == Duelable.Opponent;
        }

        private void UnitLeftAttackZone(Duelable unit)
        {
            if (!Duelable.HasOpponent) 
            {
                IsOpponentInAttackZone = false;
                return;
            }
            
            if(IsOpponentInAttackZone)
                IsOpponentInAttackZone = !(unit == Duelable.Opponent);
        }

        #endregion

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
            _stateMachine.CurrentState.FrameUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
 