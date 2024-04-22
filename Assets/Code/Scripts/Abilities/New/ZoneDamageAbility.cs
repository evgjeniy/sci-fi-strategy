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
    public class ZoneDamageAbility : IAbility
    {
        private readonly Area<Damageble> _damageArea = new(conditions: damageable => damageable.Team != Team.Player);
        private readonly ZoneDamageAbilityConfig _config;
        private readonly ZoneAim _aim;
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

        public ZoneDamageAbility(IConfigProviderService configProvider, EnergyController energyController, Timer timer)
        {
            _config = configProvider.GetAbilityConfig<ZoneDamageAbilityConfig>();
            _aim = new ZoneAim(_config.Radius, _config.AimPrefab, _config.GroundMask, _config.RaycastDistance);
            _timer = timer;
            _timer.IsPaused = true;
            _timer.ResetTime(_config.Cooldown);
            _timer.Changed += t => _aim.DisplayReload(t);

            energyController.AddEnergySystem(this);
        }

        void IInputSelectable.OnSelected() => _aim.SpawnAimZone();

        void IInputSelectable.OnDeselected() => _aim.Destroy();

        IInputState IInputSelectable.OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (!_timer.IsOver) return currentState;
            if (!_aim.TryRaycast(ray, out var hit)) return currentState;

            _damageArea.Update(hit.point, _config.Radius, _config.DamageMask);

            foreach (var damageable in _damageArea.Entities)
                damageable.Damage(_config.Damage);

            SpawnExplosionParticles(hit.point);

            _timer.ResetTime(_config.Cooldown);
            return new InputIdleState();
        }

        IInputState IInputSelectable.OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (_aim.TryRaycast(ray, out var hit))
                _aim.UpdatePosition(hit);

            return currentState;
        }

        private void SpawnExplosionParticles(Vector3 position) => _config.ExplosionPrefab.IfNotNull(prefab =>
        {
            prefab.Spawn(position)
                .With(explosion => explosion.transform.localScale = Vector3.one * 3.0f)
                .With(explosion => explosion.transform.localPosition += new Vector3(-0.7f, 0, 0.2f))
                .DestroyObject(delay: 3.0f);
        });
    }
}