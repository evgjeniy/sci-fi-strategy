using TMPro;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Level.UI
{
    public class RestartUIController : MonoBehaviour
    {
        [Inject] private GameModeController _gameModeController;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _holder;
        [SerializeField] private RectTransform _restartButton;

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
            _restartButton.Activate();
            _text.text = "¬€ œ–Œ»√–¿À»";
        }

        private void GameWon(int stars)
        {
            _holder.SetActive(true);
            _restartButton.Deactivate();
            _text.text = "¬€ ¬€»√–¿À»";
        }
    }
}
