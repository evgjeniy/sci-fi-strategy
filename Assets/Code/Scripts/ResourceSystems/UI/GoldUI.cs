using TMPro;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.ResourceSystems.UI
{
    public class GoldUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [Inject] private IResourceManager _resourceManager;

        private void OnEnable() => _resourceManager.Gold.Changed += UpdateCount;
        private void OnDisable() => _resourceManager.Gold.Changed -= UpdateCount;

        private void UpdateCount(int count) => _textField.text = count.ToString();
    }
}