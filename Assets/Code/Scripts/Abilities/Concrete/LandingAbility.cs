using System;
using System.Collections.Generic;
using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Abilities;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Scriptable.EnergySettings;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Abilities
{
    public class LandingAbility : IAbility
    {
        private readonly List<GameObject> _activeSquads = new();
        private readonly LandingAbilityConfig _config;
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

        public LandingAbility(IConfigProviderService configProvider, Timer timer)
        {
            _config = configProvider.GetAbilityConfig<LandingAbilityConfig>();
            _aim = new ZoneAim(_config.SquadPrefab.GuardPost.Radius, _config.AimPrefab, _config.GroundMask, _config.RaycastDistance);
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

            if (_activeSquads.Count == _config.MaxSquads)
            {
                UnityEngine.Object.Destroy(_activeSquads[0]);
                _activeSquads.RemoveAt(0);
            }
            
            var squad = _config.SquadPrefab.Spawn(hit.point)
                .With(group => group.GuardPost.Position = hit.point)
                .With(group => group.OnGroupEmpty += () =>
                {
                    _activeSquads.Remove(group.gameObject);
                    group.DestroyObject();
                })
                .With(group => SpawnParticles(hit.point + Vector3.up, group.GuardPost.Radius));
            
            _activeSquads.Add(squad.gameObject);

            _timer.ResetTime(_config.Cooldown);
            return new InputIdleState();
        }

        IInputState IInputSelectable.OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (_aim.TryRaycast(ray, out var hit))
                _aim.UpdatePosition(hit);

            return currentState;
        }

        private void SpawnParticles(Vector3 position, float radius)
        {
            _config.SpawnEffect.IfNotNull(effectPrefab => effectPrefab.Spawn(position)
                .With(effect => effect.SetActive(false))
                .With(effect =>
                {
                    var main = effect.GetComponent<ParticleSystem>().main;
                    main.startSize = new ParticleSystem.MinMaxCurve
                    {
                        mode = ParticleSystemCurveMode.TwoConstants,
                        constantMin = Mathf.Max(0, radius + 1f),
                        constantMax = radius + 2f
                    };
                })
                .With(effect => effect.SetActive(true))
                .DestroyObject(delay: 2.0f));
        }
    }
}