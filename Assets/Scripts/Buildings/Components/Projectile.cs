using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float flyingTime = 2.0f;
        [SerializeField] private AnimationCurve flyingCurve;
        
        public async void LaunchTo<T>(T target, Action<T> onComplete = null) where T : Component => await LaunchToAsync(target, onComplete);

        // TODO: Redo (need's to be a homing missile)
        public async UniTask LaunchToAsync<T>(T target, Action<T> onComplete = null) where T : Component
        {
            var startPosition = transform.position;
            
            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / flyingTime)
            {
                if (target == null)
                {
                    Destroy(gameObject);
                    return;
                }
                
                transform.position = Vector3.Lerp(startPosition, target.transform.position, flyingCurve.Evaluate(t));
                await UniTask.NextFrame();
            }

            transform.position = Vector3.Lerp(startPosition, target.transform.position, flyingCurve.Evaluate(1.0f));
            onComplete?.Invoke(target);
            Destroy(gameObject);
        } 
    }
}