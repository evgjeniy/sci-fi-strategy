using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain
{
    public class SkipInitiator : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;
        [SerializeField]
        private int _spawnerIndex;

        public int SpawnerIndex { get => _spawnerIndex; set => _spawnerIndex = value; }

        public void ActivateInit(float delay, List<string> enemies)
        {
            gameObject.SetActive(true);
            StartCoroutine(AsyncFillProgress(delay, Time.time));
        }

        public void DeactivateInit()
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
        }


        private IEnumerator AsyncFillProgress(float delay, float startTime)
        {
            while(Time.time - startTime < delay)
            {
                _fillImage.fillAmount =  1 - (Time.time - startTime) / delay;
                yield return null;
            }
        }
    }
}
