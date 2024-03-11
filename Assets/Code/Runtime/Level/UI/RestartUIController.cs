using TMPro;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Level.UI
{
    public class RestartUIController : MonoBehaviour
    {
        [Inject] private GameModeController _gameModeController;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _holder;
        private void OnEnable()
        {
            _holder.SetActive(false);
            
            _gameModeController.OnGameWon += GameWon;
            _gameModeController.OnGameLost += GameLost;
        }

        private void OnDisable()
        {
            _gameModeController.OnGameWon -= GameWon;
            _gameModeController.OnGameLost -= GameLost;
        }

        private void GameLost()
        {
            _holder.SetActive(true);
            _text.text = "YOU LOST";
        }

        private void GameWon()
        {
            _holder.SetActive(true);
            _text.text = "YOU WON";
        }
    }
}
