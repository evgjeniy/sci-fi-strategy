using TMPro;
using UnityEngine;
using Zenject;

namespace ResourceSystems
{
    public class GoldUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private ResourceManager _resourceManager;

        [Inject]
        public void GetManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            _resourceManager.OnGoldChanged += UpdateCount;
        }

        private void UpdateCount(int count)
        {
            _textField.text = count.ToString();
        }

        private void UnSubscribe()
        {
            _resourceManager.OnGoldChanged -= UpdateCount;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
    }
}