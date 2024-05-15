using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SustainTheStrain
{
    public class AsyncLoader : MonoBehaviour
    {
        [SerializeField] private RectTransform _loadingScreen;
        [SerializeField] private RectTransform _mainMenu;
        [SerializeField] private Slider _loadingSlider;

        public void LoadLevel(string levelName)
        {
            _loadingScreen.Activate();

            StartCoroutine(LoadLevelAsync(levelName));
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        IEnumerator LoadLevelAsync(string levelName)
        {
            var loadOperation = SceneManager.LoadSceneAsync(levelName);

            while (!loadOperation.isDone)
            {
                _loadingSlider.value = Mathf.Clamp01(loadOperation.progress/0.9f);
                yield return null;
            }
        }
    }
}
