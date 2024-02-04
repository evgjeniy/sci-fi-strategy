using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.ResourceSystems
{
    public class GeneratorUI
    {
        private Image _completionBar;
        public GeneratorUI(Transform parent, ResourceGenerator generator)
        {
            _completionBar = GameObject.Instantiate(generator.MGeneratorSettings.GeneratingIndicator, parent);
            generator.OnGeneratedPercentChanged += SetCompletionPercent;
        }

        private void SetCompletionPercent(float percent)
        {
            if (_completionBar != null)
            {
                _completionBar.fillAmount = percent;
            }
        }

        public void Unsubscribe(ResourceGenerator generator)
        {
            generator.OnGeneratedPercentChanged -= SetCompletionPercent;
        }
        
    }
}