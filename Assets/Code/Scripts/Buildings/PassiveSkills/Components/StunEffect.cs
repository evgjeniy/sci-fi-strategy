using System.Collections;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class StunEffect : MonoBehaviour
    {
        private Coroutine _routine;
        private Unit _unit;
        private float _oldSpeed;

        private ParticleSystem _stunEffect;

        public void Initialize(StunConfig config) => Initialize(config, config.Duration);

        public void Initialize(StunConfig config, float duration)
        {
            if (TryGetComponent(out _unit) is false)
            {
                Destroy(this);
            }
            else
            {
                if (_routine == null) _oldSpeed = _unit.CurrentPathFollower.Speed;
                else StopCoroutine(_routine);

                _routine = StartCoroutine(StunRoutine(duration));
            
                if (_stunEffect == null && config.StunParticle != null)
                    _stunEffect = config.StunParticle.Spawn(transform);
            }
        }

        private IEnumerator StunRoutine(float duration)
        {
            _unit.Freeze();

            yield return new WaitForSeconds(duration);

            _unit.Unfreeze(_oldSpeed);
            _stunEffect.IfNotNull(Destroy);
        }
    }
}