using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.UI;

namespace SustainTheStrain.Tips
{
    public class Advice : DotWeenGraphicAnimation
    {
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _nextButton;
        
        private Graphic[] _graphics;

        public void OnEnable() => _graphics.ForEach(Show);

        public void OnDisable() => _graphics.ForEach(Hide);

        private void Awake() => _graphics = GetComponentsInChildren<Graphic>();

        private void Start()
        {
            var adviceSequence = GetComponentInParent<AdviceSequence>();
            if (adviceSequence == null) return;
            
            _skipButton.IfNotNull(button => button.onClick.AddListener(adviceSequence.Disable));
            _nextButton.IfNotNull(button => button.onClick.AddListener(adviceSequence.NextAdvice));
        }
    }
}