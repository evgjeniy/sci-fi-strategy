using System;
using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Abilities;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Scriptable.EnergySettings;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Abilities.New
{
    public class EnemyHackAbility : IAbility
    {
        private readonly EnemyHackAbilityConfig _config;
        private readonly BaseAim _aim;
        private readonly Timer _timer;
        
        private int _currentEnergy;
        private Damageble _currentDamageable;

        public IObservable<ITimer> CooldownTimer => _timer;
        public EnergySystemSettings EnergySettings => _config.EnergySettings;
        public int MaxEnergy => _config.EnergySettings.MaxEnergy;

        public int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                if (value < 0 || value > MaxEnergy) return;
                _currentEnergy = value;

                var isEnergyEmpty = _currentEnergy == 0;
                _timer.IsPaused = isEnergyEmpty;

                if (isEnergyEmpty)
                    _timer.ResetTime(_config.Cooldown);

                Changed(this);
            }
        }

        public event Action<IEnergySystem> Changed = _ => { };

        public EnemyHackAbility(IConfigProviderService configProvider, EnergyController energyController, Timer timer)
        {
            _config = configProvider.GetAbilityConfig<EnemyHackAbilityConfig>();
            _aim = new BaseAim(_config.EnemyMask, _config.RaycastDistance);
            _timer = timer;
            _timer.IsPaused = true;
            _timer.ResetTime(_config.Cooldown);

            energyController.AddEnergySystem(this);
        }

        IInputState IInputSelectable.OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (!_timer.IsOver) return currentState;
            if (_currentDamageable == null) return currentState;

            _currentDamageable.Team = _config.NewTeam;
            _currentDamageable.GetComponent<Outline>().IfNotNull(outline => outline.Disable());

            _timer.ResetTime(_config.Cooldown);
            return new InputIdleState();
        }

        IInputState IInputSelectable.OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (_aim.TryRaycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent<Damageble>(out var damageable) && damageable.Team != _config.NewTeam)
                {
                    if (_currentDamageable == null)
                    {
                        _currentDamageable = damageable;
                        _currentDamageable.GetComponent<Outline>().IfNotNull(outline => outline.Enable());
                    }
                }
            }
            else if (_currentDamageable != null)
            {
                _currentDamageable.GetComponent<Outline>().IfNotNull(outline => outline.Disable());
                _currentDamageable = null;
            }

            return currentState;
        }
    }
}