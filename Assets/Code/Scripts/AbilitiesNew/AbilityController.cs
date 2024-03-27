using NTC.FiniteStateMachine;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Installers;
using Zenject;

namespace SustainTheStrain.AbilitiesNew
{
    public class AbilityController : MonoEnergySystem
    {
        private StateMachine<AbilityController> _abilityStateMachine;
        private IAbilityInput _abilityInput;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IAbilityInput abilityInput)
        {
            _abilityInput = abilityInput;
            _abilityStateMachine = new StateMachine<AbilityController>
            (
                new DisabledState(),
                new ZoneDamageAbility(this, staticDataService),
                new ChainDamageAbility(this, staticDataService)
            ) { TransitionsEnabled = false };
            
            _abilityStateMachine.SetState<DisabledState>();
        }

        private void OnEnable()
        {
            _abilityInput.OnAbilityEnter += OnAbilityEnter;
            _abilityInput.OnAbilityExit += OnAbilityExit;
        }

        private void OnDisable()
        {
            _abilityInput.OnAbilityEnter -= OnAbilityEnter;
            _abilityInput.OnAbilityExit -= OnAbilityExit;
        }

        private void OnAbilityEnter(int index)
        {
            switch (index)
            {
                case 1: _abilityStateMachine.SetState<ZoneDamageAbility>(); break;
                case 2: _abilityStateMachine.SetState<ChainDamageAbility>(); break;
            }
        }

        private void OnAbilityExit(int _) => _abilityStateMachine.SetState<DisabledState>();

        private void Update() => _abilityStateMachine.Run();
        private void OnDestroy() => _abilityStateMachine.CurrentState.OnExit();
    }
    
    public class DisabledState : IState<AbilityController> { public AbilityController Initializer => null; }
}