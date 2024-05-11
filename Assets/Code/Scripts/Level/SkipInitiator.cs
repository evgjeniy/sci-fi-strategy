using SustainTheStrain.Units;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        [SerializeField]
        private RectTransform _tip;
        [SerializeField]
        private TMPro.TextMeshProUGUI _text;

        public int SpawnerIndex { get => _spawnerIndex; set => _spawnerIndex = value; }

        public void ActivateInit(float delay, List<string> enemies)
        {
            gameObject.SetActive(true);
            _tip.gameObject.SetActive(false);
            StringBuilder sb = new StringBuilder();
            foreach(var enemyName in enemies)
            {
                sb.Append(enemyName + '\n');
            }
            _text.text = sb.ToString();

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
