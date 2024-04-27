using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Abilities;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Scriptable.EnergySettings;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Abilities
{
    public class FreezeAbility : IAbility
    {
        private readonly Area<Unit> _freezeArea = new(bufferMaxSize: 64, conditions: unit => unit.TryGetComponent<Damageble>(out var damageable) && damageable.Team != Team.Player);
        private readonly BaseAim _aim;
        private readonly FreezeAbilityConfig _config;
        private readonly Timer _timer;
        private int _currentEnergy;

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

        public FreezeAbility(IConfigProviderService configProvider, Timer timer)
        {
            _config = configProvider.GetAbilityConfig<FreezeAbilityConfig>();
            _aim = new BaseAim(_config.RaycastMask, _config.RaycastDistance);
            _timer = timer;
            _timer.IsPaused = true;
            _timer.ResetTime(_config.Cooldown);
        }

        IInputState IInputSelectable.OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (!_timer.IsOver) return currentState;
            if (_freezeArea.Entities.Count == 0) return currentState;

            UseAbility();

            _timer.ResetTime(_config.Cooldown);
            return new InputIdleState();
        }

        private async void UseAbility()
        {
            var oldSpeeds = new List<float>(_freezeArea.Entities.Count);

            foreach (var unit in _freezeArea.Entities)
            {
                if (unit.TryGetComponent<Outline>(out var outline))
                    outline.Disable();

                oldSpeeds.Add(unit.CurrentPathFollower.Speed);
                unit.Freeze();
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_config.Duration));

            var currentIndex = 0;
            foreach (var unit in _freezeArea.Entities) 
                unit.Unfreeze(oldSpeeds[currentIndex++]);
        }

        IInputState IInputSelectable.OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (_aim.TryRaycast(ray, out var hit)) 
                _freezeArea.Update(hit.point, _config.Radius, _config.DamageMask);

            foreach (var unit in _freezeArea.Entities)
                if (unit.TryGetComponent<Outline>(out var outline))
                    outline.Enable();
            
            return currentState;
        }
    }
}