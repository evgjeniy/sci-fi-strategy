﻿using System.Collections;
using SustainTheStrain.Units.Components;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class FireEffect : MonoBehaviour
    {
        private float _damagePerSecond;
        private float _duration;
        private float _startTime;
        
        private Coroutine _routine;
        private IDamageable _damageable;
        private ParticleSystem _fireEffect;

        public void Initialize(FirePassiveSkillConfig config)
        {
            _damagePerSecond = config.DamagePerSecond;
            _duration = config.FireDuration;
            _startTime = Time.time;

            if (TryGetComponent(out _damageable) is false)
            {
                Destroy(this);
                return;
            }
            
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(TakeFireDamage());
            
            if (_fireEffect == null && config.FireParticle != null)
                _fireEffect = config.FireParticle.Spawn(transform);
        }

        private IEnumerator TakeFireDamage()
        {
            _damageable.Damage(_damagePerSecond);

            if (Time.time - _startTime > _duration)
            {
                _fireEffect.IfNotNull(Destroy);
                Destroy(this);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}