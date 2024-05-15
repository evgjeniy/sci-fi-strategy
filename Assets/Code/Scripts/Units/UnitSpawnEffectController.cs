using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    public class UnitSpawnEffectController : MonoBehaviour
    {
        [SerializeField] private Renderer nrenderer;

        [SerializeField] private float _spawnSpeed;
        void OnEnable()
        {
            StartCoroutine(ReviewEffect());
        }
        
        private IEnumerator ReviewEffect()
        {
            float progress = 1;
            while (progress > 0.0f)
            {
                progress -= Time.deltaTime * _spawnSpeed;
                nrenderer.material.SetFloat("_Progress", progress);
                yield return null;
            }
        }
    }
}
