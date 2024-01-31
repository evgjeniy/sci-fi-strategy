using SustainTheStrain.Input.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.AbilitiesScripts
{
    public class AbilitiesUIController : MonoBehaviour
    {
        [SerializeField] private InputSystemButtonBridge ButtonPrefab;
        [SerializeField] private Slider SliderPrefab;
        [SerializeField] private GameObject buttonHolder;
        [SerializeField] private GameObject sliderHolder;

        private readonly Color _readyColor = Color.green; //temporary UI
        private readonly Color _loadingColor = Color.red;

        private AbilityControlButton[] _buttons;
        private AbilitiesController _abilitiesController;

        private int spawnedCount = 0;

        
        [Inject]
        private void Init(AbilitiesController Controller)
        {
            _abilitiesController = Controller;
            _abilitiesController.Init(); //temporary, because now we don't have MainController
            _buttons = new AbilityControlButton[_abilitiesController.Abilities.Count];
        }

        private void SetZoneDamageButtonData(int idx, float load, bool ready)
        {
            _buttons[idx].GetSlider().value = load;
            //if (_buttons[idx].IsReady() ^ ready) // ready-state updated
                //_buttons[idx].GetButton().image.color = _buttons[idx].ChangeReady() ? _readyColor : _loadingColor;
        }

        public void SpawnControlButton(Transform sliderHolder/*, Transform buttonHolder*/)
        {
            var b = Instantiate(ButtonPrefab, /*buttonHolder*/ buttonHolder.transform);
            var s = Instantiate(SliderPrefab, sliderHolder);
            _buttons[spawnedCount] = new AbilityControlButton(b, s);
            s.value = 1;
            //b.image.color = _readyColor;
            b.GetComponentInChildren<TextMeshProUGUI>().text = _abilitiesController.Abilities[spawnedCount].GetType().Name;//temp
            b.SetNumberButton(spawnedCount + 1);
            _abilitiesController.ReloadListAdd(spawnedCount, SetZoneDamageButtonData);
            spawnedCount++;
        }
    }
}