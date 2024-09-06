using System;
using System.Collections.Generic;
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
    // TODO : Create 'private List<Damageable> GetTargets(RaycashHit hit)' method
    // TODO : Use this in the 'OnSelectedUpdate' to enable/disable Outline 
    // TODO : Use this in the 'OnSelectedLeftClick' to damage targets, enable LineRenderer, disable Outline
    public class ChainDamageAbility : IAbility
    {
        private readonly Area<Collider> _area = new(conditions: collider => collider.TryGetComponent<Damageble>(out var d) && d.Team != Team.Player);
        private readonly ChainDamageAbilityConfig _config;
        private readonly BaseAim _aim;
        private readonly Timer _timer;
        private int _currentEnergy;
        private Damageble _damageable;
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

        public void CacheUiTip(TMP_Text uiTip) { _uiTip = uiTip; UpdateTip(this); }
        private void UpdateTip(IEnergySystem system) => _uiTip.IfNotNull(x => x.text = $"Active cells: {system.CurrentEnergy}");

        public ChainDamageAbility(IConfigProviderService configProvider, Timer timer)
        {
            _config = configProvider.GetAbilityConfig<ChainDamageAbilityConfig>();
            _aim = new BaseAim(_config.DamageMask, _config.RaycastDistance);
            _timer = timer;
            _timer.IsPaused = true;
            _timer.ResetTime(_config.Cooldown);
        }

        IInputState IInputSelectable.OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (!_timer.IsOver) return currentState;
            if (!_aim.TryRaycast(ray, out var hit)) return currentState;

            UseAbility(hit);

            _timer.ResetTime(_config.Cooldown);
            return new InputIdleState();
        }

        private void UseAbility(RaycastHit hit)
        {
            if (_damageable == null) return;
            
            var line = SpawnLineRenderer();
            var currentTarget = hit.collider;
            List<Collider> usedTargets = new(_config.MaxTargets) { hit.collider };

            //TODO DAMAGE TYPE INSTALLER
            currentTarget.GetComponent<Damageble>().Damage(_config.Damage, DamageType.Physical);

            line.positionCount++;
            line.SetPosition(line.positionCount - 1, currentTarget.transform.position);

            while (line.positionCount < _config.MaxTargets)
            {
                _area.Update(currentTarget.transform.position, _config.Distance, _config.DamageMask);
                Collider nearest = null;
                var bestDistance = float.MaxValue;

                foreach (var collider in _area.Entities)
                {
                    if (usedTargets.Contains(collider)) continue;

                    var distance = Vector3.Distance(collider.transform.position, currentTarget.transform.position);
                    if (distance >= bestDistance) continue;
                    
                    nearest = collider;
                    bestDistance = distance;
                }

                if (nearest == null) break;

                currentTarget = nearest;
                //TODO DAMAGE TYPE INSTALLER
                currentTarget.GetComponent<Damageble>().Damage(_config.Damage, DamageType.Physical);
                usedTargets.Add(currentTarget);

                line.positionCount++;
                line.SetPosition(line.positionCount - 1, currentTarget.transform.position);
            }

            line.gameObject.AddComponent<ChainUpdate>().SetData(_config.LineVisibilityDuration, usedTargets);
            _damageable.GetComponent<Outline>().IfNotNull(outline => outline.Disable());
        }

        IInputState IInputSelectable.OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (_aim.TryRaycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent<Damageble>(out var damageable) && damageable.Team != Team.Player)
                {
                    if (_damageable == null)
                    {
                        _damageable = damageable;
                        _damageable.GetComponent<Outline>().IfNotNull(outline => outline.Enable());
                    }
                }
            }
            else if (_damageable != null)
            {
                _damageable.GetComponent<Outline>().IfNotNull(outline => outline.Disable());
                _damageable = null;
            }

            return currentState;
        }

        private LineRenderer SpawnLineRenderer() => _config.LinePrefab.Spawn()
            .With(line => line.Deactivate())
            .With(line => line.positionCount = 0)
            .With(line => line.startWidth = line.endWidth = 0.2f)
            .With(line => line.Activate());
    }
}