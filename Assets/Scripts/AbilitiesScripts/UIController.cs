using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.AbilitiesScripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button ButtonPrefab;
        [SerializeField] private Slider SliderPrefab;
        [SerializeField] private GameObject buttonHolder;
        [SerializeField] private GameObject sliderHolder;

        private readonly Color _readyColor = Color.green; //temporary UI
        private readonly Color _loadingColor = Color.red;

        private bool _zoneDamageButtonReady = true;
        private AbilityButton[] _buttons;
        private AbilitiesController _abilitiesController;

        private void Start() => Init();

        private void Init()
        {
            _abilitiesController = GetComponent<AbilitiesController>();
            _abilitiesController.Init(); //temporary, because now we don't have MainController
            _buttons = new AbilityButton[_abilitiesController.Abilities.Count];
            for (var i = 0; i < _abilitiesController.Abilities.Count; i++)
            {
                var b = Instantiate(ButtonPrefab, buttonHolder.transform);
                var s = Instantiate(SliderPrefab, sliderHolder.transform);
                _buttons[i] = new AbilityButton(b, s);
                s.value = 1;
                b.image.color = _readyColor;
                var cnt = i;
                b.onClick.AddListener(() => { _abilitiesController.OnAbilitySelect(cnt); });
                _abilitiesController.ReloadListAdd(cnt, SetZoneDamageButtonData);
            }
        }

        private void SetZoneDamageButtonData(int idx, float load, bool ready)
        {
            _buttons[idx].GetSlider().value = load;
            if (_zoneDamageButtonReady ^ ready) // ready-state updated
            {
                _zoneDamageButtonReady = ready;
                _buttons[idx].GetButton().image.color = _zoneDamageButtonReady ? _readyColor : _loadingColor;
            }
        }
    }
}