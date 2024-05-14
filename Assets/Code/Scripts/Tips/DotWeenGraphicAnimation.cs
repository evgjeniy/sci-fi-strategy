using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SustainTheStrain.Tips
{
    public class DotWeenGraphicAnimation : MonoBehaviour
    {
        [Foldout("Show Settings"), SerializeField, Min(0.0f)] private float _showDelay = 0.5f;
        [Foldout("Show Settings"), SerializeField, Min(0.0f)] private float _showDuration = 0.5f;
        [Foldout("Show Settings"), SerializeField, Range(0.0f, 1.0f)] private float _showFadeValue = 1.0f;
        [Foldout("Show Settings"), SerializeField] private Ease _showEase = Ease.OutExpo;
        [Foldout("Show Settings"), SerializeField] private UnityEvent _onShowStart;
        [Foldout("Show Settings"), SerializeField] private UnityEvent _onShowComplete;

        [Foldout("Hide Settings"), SerializeField, Min(0.0f)] private float _hideDelay = 0.0f;
        [Foldout("Hide Settings"), SerializeField, Min(0.0f)] private float _hideDuration = 0.0f;
        [Foldout("Show Settings"), SerializeField, Range(0.0f, 1.0f)] private float _hideFadeValue = 0.0f;
        [Foldout("Hide Settings"), SerializeField] private Ease _hideEase = Ease.Unset;
        [Foldout("Hide Settings"), SerializeField] private UnityEvent _onHideStart;
        [Foldout("Hide Settings"), SerializeField] private UnityEvent _onHideComplete;

        private Tween _tween;

        public void Show(Graphic graphic)
        {
            _tween?.Kill();
            _tween = graphic.DOFade(_showFadeValue, _showDuration)
                .SetDelay(_showDelay).SetEase(_showEase)
                .SetLink(graphic.gameObject)
                .OnStart(_onShowStart.Invoke)
                .OnKill(_onShowComplete.Invoke)
                .Play();
        }

        public void Hide(Graphic graphic)
        {
            _tween?.Kill();
            _tween = graphic.DOFade(_hideFadeValue, _hideDuration)
                .SetDelay(_hideDelay).SetEase(_hideEase)
                .SetLink(graphic.gameObject)
                .OnStart(_onHideStart.Invoke)
                .OnKill(_onHideComplete.Invoke)
                .Play();
        }
    }
}