using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SustainTheStrain.Buildings.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseBuildingButton<TMenu> : MonoBehaviour
    {
        private Button _button;
        protected TMenu Menu;

        protected abstract UnityAction ButtonAction { get; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            Menu = GetComponentInParent<TMenu>();
        }

        private void OnEnable() => _button.onClick.AddListener(ButtonAction);
        private void OnDisable() => _button.onClick.RemoveListener(ButtonAction);
    }
}