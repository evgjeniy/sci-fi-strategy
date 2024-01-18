using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button zoneDamageButton;
    [SerializeField] Slider zoneDamageSlider;
    private AbilitiesController abilitiesController;
    private Color readyColor = Color.green; //temporary UI
    private Color loadingColor = Color.red;
    private bool zoneDamageButtonReady = true;

    void Start()
    {
        abilitiesController = GetComponent<AbilitiesController>();
        abilitiesController.ZoneDamageAdd(SetZoneDamageButtonData);
        zoneDamageButton.GetComponentInChildren<TextMeshProUGUI>().text = "ZoneDMG";
        zoneDamageSlider.value = 1;
        zoneDamageButton.image.color = readyColor;
    }

    public void SetZoneDamageButtonData(float load, bool ready)
    {
        zoneDamageSlider.value = load;
        if (zoneDamageButtonReady ^ ready) //ready-state updated
        {
            zoneDamageButtonReady = ready;
            zoneDamageButton.image.color = zoneDamageButtonReady ? readyColor : loadingColor;
        }
    }
}
