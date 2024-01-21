using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace ResourceSystems
{
    public class ExplorePointsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private ResourceManager _resourceManager;

        [Inject]
        public void GetManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            _resourceManager.OnExplorePointsChanged += UpdateCount;
        }

        private void UpdateCount(int count)
        {
            _textField.text = count.ToString();
        }

        private void UnSubscribe()
        {
            _resourceManager.OnExplorePointsChanged -= UpdateCount;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
    }
}