using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.ResourceSystems
{
    public class GeneratorUI
    {
        private Image _completionBar;
        public GeneratorUI(Transform parent, ResourceGenerator generator)
        {
            _completionBar = GameObject.Instantiate(generator.GeneratingIndicator, parent);
            generator.OnGeneratedPercentChanged += SetCompletionPercent;
        }

        private void SetCompletionPercent(float percent)
        {
            _completionBar.fillAmount = percent;
        }
        
    }
}