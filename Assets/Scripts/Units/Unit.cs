using SustainTheStrain.Units.Components;
using SustainTheStrain.Units.PathFollowers;
using SustainTheStrain.Units.StateMachine.ConcreteStates;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using static UnityEngine.UI.CanvasScaler;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Damageble))]
    public class Unit : MonoBehaviour
    {
        [field:SerializeField] public float Damage { get; private set; }
        [field:SerializeField] public float DamagePeriod { get; private set; }

        public IPathFollower CurrentPathFollower { get; protected set; }   
        protected StateMachine.StateMachine _stateMachine = new StateMachine.StateMachine();

        public Damageble Damageble { get; protected set; }
        public AggroRadiusCheck AggroRadiusCheck { get; protected set;}
        public AttackRadiusCheck AttackRadiusCheck { get; protected set;}
        public StateMachine.StateMachine StateMachine => _stateMachine;
        public NavPathFollower NavPathFollower { get; protected set; }
        public Unit Opponent { get; protected set; }

        public bool IsAnnoyed { get; protected set; } 
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

            SwitchPathFollower(NavPathFollower);
        }

        #region UNIT_TRIGGER_LOGIC

        private void UnitEneterdAggroZone(Unit unit)
        {
            IsAnnoyed = true;
        }

        private void UnitLeftAggroZone(Unit unit)
        {
            IsAnnoyed = AggroRadiusCheck.AggroZoneUnits.Count != 0;

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

        public bool IsDuelPossible(Unit innitiator)
        {
            return Opponent == null && innitiator.Damageble.Team != Damageble.Team;
        }

        public bool RequestDuel(Unit unit)
        {
            if(unit.IsDuelPossible(this))
            {
                unit.SetOpponent(this);
                SetOpponent(unit);
                return true;
            }
            else return false;
        }

        public void SetOpponent(Unit unit)
        {
            Opponent = unit;
            unit.Damageble.OnDied += OpponentDead;
        }

        public void BreakDuel()
        {
            if (Opponent == null) return;

            Opponent.RemoveOpponent();
            RemoveOpponent();
        }

        public void RemoveOpponent()
        {
            Opponent.Damageble.OnDied -= OpponentDead;
            Opponent = null;
        }

        public void OpponentDead(Damageble damageble)
        {
            BreakDuel();
        }

        #endregion

        public void SwitchPathFollower(IPathFollower pathFollower)
        {
            if (pathFollower == null) return;

            CurrentPathFollower?.Stop();
            CurrentPathFollower = pathFollower;
            CurrentPathFollower.Start();
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
 