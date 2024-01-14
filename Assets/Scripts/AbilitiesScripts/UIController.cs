using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button zoneDamageButton;
    private TextMeshProUGUI zoneDamageButtonText;
    private Color readyColor = Color.green; //temporary UI
    private Color loadingColor = Color.red;
    private bool zoneDamageButtonReady = true;

    void Start()
    {
        zoneDamageButtonText = zoneDamageButton.GetComponentInChildren<TextMeshProUGUI>();
        zoneDamageButtonText.text = "ZoneDMG";
    }

    public void setZoneDamageButtonData(float load, bool ready)
    {
        zoneDamageButtonText.text = load.ToString();    //temporary UI
        if (zoneDamageButtonReady ^ ready) //ready-state updated
        {
            zoneDamageButtonReady = ready;
            zoneDamageButton.image.color = zoneDamageButtonReady ? readyColor : loadingColor;
        }
    }
}
