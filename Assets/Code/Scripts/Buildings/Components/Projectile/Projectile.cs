using System;
using System.Collections;
using NTC.Pool;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class Projectile : MonoBehaviour, ISpawnable
    {
        [SerializeField] private float flyingTime = 2.0f;
        [SerializeField] private ParticleSystem explosionParticle;

        [SerializeField] private GameObject _visualComponentTransform;

        //private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();

        public void OnSpawn() => _visualComponentTransform.SetActive(true);

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
                if (target == null) { Destroy(gameObject); yield break; }

                var position = target.transform.position;
                Transform transform1;
                (transform1 = transform).position = Vector3.Lerp(startPosition, position, t);
                //Rotation to target
                Quaternion lookRotation = Quaternion.LookRotation((position - transform1.position).normalized);
                transform.localRotation = lookRotation;
                
                yield return null;
            }
            
            if (target == null) { Destroy(gameObject); yield break; }

            transform.position = target.transform.position;
            onComplete?.Invoke(target);

            if (explosionParticle != null)
            {
                explosionParticle.Play();
                _visualComponentTransform.SetActive(false);
                yield return new WaitForSeconds(explosionParticle.main.duration);
            }

            NightPool.Despawn(gameObject);
        }
    }
}