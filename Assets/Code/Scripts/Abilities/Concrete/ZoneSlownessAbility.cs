using System;
using Cysharp.Threading.Tasks;
using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Abilities;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Scriptable.EnergySettings;
using SustainTheStrain.Units;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Abilities
{
    public class ZoneSlownessAbility : IAbility
    {
        private readonly Area<Damageble> _damageArea = new(conditions: damageable => damageable.Team != Team.Player);
        private readonly ZoneSlownessAbilityConfig _config;
        private readonly ZoneAim _aim;
        private readonly Timer _timer;
        private int _currentEnergy;
        private TMP_Text _uiTip;

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
                UpdateTip(this);
            }
        }

        public event Action<IEnergySystem> Changed = _ => { };

        public ZoneSlownessAbility(IConfigProviderService configProvider, Timer timer)
        {
            _config = configProvider.GetAbilityConfig<ZoneSlownessAbilityConfig>();
            _aim = new ZoneAim(_config.Radius, _config.AimPrefab, _config.GroundMask, _config.RaycastDistance);
            _timer = timer;
            _timer.IsPaused = true;
            _timer.ResetTime(_config.Cooldown);
            _timer.Changed += t => _aim.DisplayReload(t);
        }

        public void CacheUiTip(TMP_Text uiTip) { _uiTip = uiTip; UpdateTip(this); }
        private void UpdateTip(IEnergySystem system) => _uiTip.IfNotNull(x => x.text = $"Active cells: {system.CurrentEnergy}");

        void IInputSelectable.OnSelected()
        {
            _aim.SpawnAimZone();
            _aim.DisplayReload(_timer);
        }

        void IInputSelectable.OnDeselected() => _aim.Destroy();

        IInputState IInputSelectable.OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (!_timer.IsOver) return currentState;
            if (!_aim.TryRaycast(ray, out var hit)) return currentState;

            _damageArea.Update(hit.point, _config.Radius, _config.DamageMask);

            foreach (var damageable in _damageArea.Entities)
            {
                if (damageable.TryGetComponent<Unit>(out var unit) is false)
                    continue;

                var pathFollower = unit.CurrentPathFollower;
                if (pathFollower == null) continue;

                pathFollower.Speed *= _config.SpeedMultiplier;
                RestoreSpeed(_config.Duration, pathFollower);
            }

            _timer.ResetTime(_config.Cooldown);
            return new InputIdleState();
        }

        private async void RestoreSpeed(float slownessDuration, IPathFollower pathFollower)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(slownessDuration));
            if (pathFollower != null) 
                pathFollower.Speed /= _config.SpeedMultiplier;
        }

        IInputState IInputSelectable.OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (_aim.TryRaycast(ray, out var hit))
                _aim.UpdatePosition(hit);

            return currentState;
        }
    }
}