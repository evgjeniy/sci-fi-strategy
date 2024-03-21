using SustainTheStrain.Input.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.Abilities
{
    public class AbilitiesUIController 
    {

        private AbilityControlButton[] _buttons;
        private AbilitiesController _abilitiesController;

        private int spawnedCount = 0;

        
        [Inject]
        private void Init(AbilitiesController controller)
        {
            _abilitiesController = controller;
            _abilitiesController.Init(); //temporary, because now we don't have MainController
            _buttons = new AbilityControlButton[_abilitiesController.Abilities.Count];
        }

        private void SetZoneDamageButtonData(int idx, float load, bool ready)
        {
            _buttons[idx].GetSlider().value = load;
            //if (_buttons[idx].IsReady() ^ ready) // ready-state updated
                //_buttons[idx].GetButton().image.color = _buttons[idx].ChangeReady() ? _readyColor : _loadingColor;
        }

        public void AddControlButton(InputSystemButtonBridge b, Slider s)
        {
            _buttons[spawnedCount] = new AbilityControlButton(b, s);
            b.SetNumberButton(spawnedCount + 1);
            _abilitiesController.ReloadListAdd(spawnedCount, SetZoneDamageButtonData);
            spawnedCount++;
        }
    }
}