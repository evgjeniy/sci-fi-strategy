using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Tips
{
    public class AdviceSequence : MonoBehaviour
    {
        [SerializeField] private Advice[] _childAdvices;
        [Inject] private IPauseManager _pauseManager;

        private int _currentIndex;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponentInParent<AdviceController>().BackgroundCanvas;
            _childAdvices.ForEach(advice => advice.Deactivate());
        }

        public void OnEnable()
        {
            if (_childAdvices.Length == 0) return;
            
            _pauseManager.Pause();
            _canvas.Enable();
            
            _currentIndex = 0;
            _childAdvices[_currentIndex].Activate();
        }

        public void NextAdvice()
        {
            if (enabled is false) return;
            
            if (_currentIndex == _childAdvices.Length - 1)
            {
                this.Deactivate();
            }
            else
            {
                _childAdvices[_currentIndex].Deactivate();
                _currentIndex += 1;
                _childAdvices[_currentIndex].Activate();
            }
        }

        public void OnDisable()
        {
            _childAdvices[_currentIndex].Deactivate();
            _pauseManager.Unpause();
            _canvas.Deactivate();
        }
    }
}