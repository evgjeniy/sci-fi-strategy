using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button zoneDamageButton;
    [SerializeField] Slider zoneDamageSlider;
    private WeaponController weaponController;
    private Color readyColor = Color.green; //temporary UI
    private Color loadingColor = Color.red;
    private bool zoneDamageButtonReady = true;

    void Start()
    {
        weaponController = GetComponent<WeaponController>();
        weaponController.zoneDamageAdd(setZoneDamageButtonData);
        zoneDamageButton.GetComponentInChildren<TextMeshProUGUI>().text = "ZoneDMG";
        zoneDamageSlider.value = 1;
        zoneDamageButton.image.color = readyColor;
    }

    public void setZoneDamageButtonData(float load, bool ready)
    {
        zoneDamageSlider.value = load;
        if (zoneDamageButtonReady ^ ready) //ready-state updated
        {
            zoneDamageButtonReady = ready;
            zoneDamageButton.image.color = zoneDamageButtonReady ? readyColor : loadingColor;
        }
    }
}
