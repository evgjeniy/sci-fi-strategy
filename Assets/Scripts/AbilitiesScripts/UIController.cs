using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button ButtonPrefab;
    [SerializeField] Slider SliderPrefab;
    [SerializeField] GameObject buttonHolder;
    [SerializeField] GameObject sliderHolder;
    private AbilityButton[] buttons;
    private AbilitiesController abilitiesController;
    private Color readyColor = Color.green; //temporary UI
    private Color loadingColor = Color.red;
    private bool zoneDamageButtonReady = true;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        abilitiesController = GetComponent<AbilitiesController>();
        abilitiesController.Init(); //temporary, because now we don't have MainController
        buttons = new AbilityButton[abilitiesController.abilities.Count];
        for(int i = 0; i < abilitiesController.abilities.Count; i++)
        {
            Button b = Instantiate(ButtonPrefab, buttonHolder.transform);
            Slider s = Instantiate(SliderPrefab, sliderHolder.transform);
            buttons[i] = new AbilityButton(b, s);
            s.value = 1;
            b.image.color = readyColor;
            int cnt = i;
            b.onClick.AddListener(() => { abilitiesController.OnAbilitySelect(cnt); });
            abilitiesController.reloadListAdd(cnt, SetZoneDamageButtonData);
        }
    }

    public void SetZoneDamageButtonData(int idx, float load, bool ready)
    {
        buttons[idx].getSlider().value = load;
        if (zoneDamageButtonReady ^ ready) //ready-state updated
        {
            zoneDamageButtonReady = ready;
            buttons[idx].getButton().image.color = zoneDamageButtonReady ? readyColor : loadingColor;
        }
    }
}
