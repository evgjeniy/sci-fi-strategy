using UnityEngine;

namespace SustainTheStrain.Tips
{
    public class PauseManager : IPauseManager
    {
        private float _lastSaveTimeScale = 1.0f;

        public bool Paused { get; private set; }

        public void Pause()
        {
            if (Paused) return;
            Paused = true;
            
            _lastSaveTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
        }

        public void Unpause()
        {
            if (Paused is false) return;
            Paused = false;
            
            Time.timeScale = _lastSaveTimeScale;
        }
    }
}