using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float flyingTime = 2.0f;
        [SerializeField] private AnimationCurve flyingCurve;
        [SerializeField] private ParticleSystem explosionParticle;
        
        public void LaunchTo<T>(T target, Action<T> onComplete = null) where T : Component
        {
            StartCoroutine(LaunchToAsync(target, onComplete));
        }

        // TODO: Redo (need's to be a homing missile)
        private IEnumerator LaunchToAsync<T>(T target, Action<T> onComplete = null) where T : Component
        {
            var startPosition = transform.position;
            
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / flyingTime)
            {
                if (target == null)
                {
                    Destroy(gameObject);
                    yield break;
                }
                
                transform.position = Vector3.Lerp(startPosition, target.transform.position, flyingCurve.Evaluate(t));
                yield return null;
            }

            transform.position = Vector3.Lerp(startPosition, target.transform.position, flyingCurve.Evaluate(1.0f));
            onComplete?.Invoke(target);

            if (explosionParticle != null)
            {
                explosionParticle.Play();
                GetComponentInChildren<MeshRenderer>().IfNotNull(meshRenderer => meshRenderer.Disable());
                yield return new WaitForSeconds(explosionParticle.main.duration);
            }

            gameObject.DestroyObject();
        }
    }
}