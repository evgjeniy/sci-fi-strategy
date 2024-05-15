using SustainTheStrain.Tips;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    public class MonoPause : MonoBehaviour
    {
        [Zenject.Inject] private IPauseManager pauseManager;

        public void Pause()
        {
            pauseManager.Pause();
        }

        public void UnPause()
        {
            pauseManager.Unpause();
        }
    }
}
