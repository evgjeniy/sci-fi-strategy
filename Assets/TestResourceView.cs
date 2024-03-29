using SustainTheStrain.ResourceSystems;
using TMPro;
using UnityEngine.Extensions;
using Zenject;

public class TestResourceView : MonoCashed<TMP_Text>
{
    [Inject] private IResourceManager _resourceManager;

    private void OnEnable() => _resourceManager.Gold.Changed += DisplayCurrentGold;
    private void OnDisable() => _resourceManager.Gold.Changed -= DisplayCurrentGold;

    private void DisplayCurrentGold(int goldValue) => Cashed1.text = $"GOLD: {goldValue}";
}
