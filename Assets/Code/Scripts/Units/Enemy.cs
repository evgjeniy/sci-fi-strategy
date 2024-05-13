using Dreamteck.Splines;
using SustainTheStrain.Units.StateMachine.ConcreteStates;
using Zenject;

namespace SustainTheStrain.Units
{
    public class Enemy : Unit
    {
        #region State Machine Variables

        protected EnemySplineMoveState _splineMoveState;
        protected UnitAttackState _attackState;
        protected UnitAgroState _aggroState;

        #endregion

        private float _roadOffset;
        
        public float RoadOffset
        {
            get => _roadOffset;
            set
            {
                _roadOffset = value;
                if (TryGetComponent<SplineFollower>(out var splineFollower))
                {
                    splineFollower.motion.offset = new(value, splineFollower.motion.offset.y);
                }
            }
        }
        
        public int CoinsDrop;
        
        public SplinePathFollower SplinePathFollower { get; protected set; }

        private void Start()
        {
            InitLogic();
        }

        protected override void Init()
        {
            base.Init();

            if (TryGetComponent<SplineFollower>(out var splineFollower))
            {
                SplinePathFollower = new SplinePathFollower(splineFollower);
                splineFollower.motion.offset = new(RoadOffset, splineFollower.motion.offset.y);
            }

        }

        protected virtual void InitLogic()
        {
            _splineMoveState = new EnemySplineMoveState(this, _stateMachine);
            _attackState = new UnitAttackState(this, _stateMachine);
            _aggroState = new UnitAgroState(this, _stateMachine);
            _splineMoveState.Init(_aggroState);
            _aggroState.Init(_attackState, _splineMoveState);
            _attackState.Init(_aggroState, _splineMoveState);

            _stateMachine.Initialize(_splineMoveState);
        }
        
        public class Factory : IFactory<Enemy>
        {
            private readonly Enemy _refEnemyPrefab;

            public Factory(Enemy refEnemyPrefab)
            {
                this._refEnemyPrefab = refEnemyPrefab;
            }

            public Enemy Create() => Instantiate(_refEnemyPrefab);
        }
    }
}
