using System;
using System.Collections;
using SustainTheStrain.Citadels;
using SustainTheStrain.Units;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Level
{
    public class GameModeController : MonoBehaviour
    {
        [Inject] private WavesManager _wavesManager;
        [Inject] private Citadel _citadel;

        public event Action OnGameLost;
        public event Action<int> OnGameWon;

        private void OnEnable()
        {
            _citadel.GetComponent<Damageble>().OnDied += _ =>
            {
                Debug.LogWarning("Lost");
                OnGameLost?.Invoke();
            };
            _wavesManager.OnLastWaveEnded += () => { StartCoroutine(CheckWhenLastEnemyDie()); };
        }


        private void Start()
        {
            _wavesManager.StartWaves();
        }

        private IEnumerator CheckWhenLastEnemyDie()
        {
            while (_wavesManager.EnemiesAlive != 0)
            {
                yield return null;
            }

            Debug.LogWarning("Won");
            OnGameWon?.Invoke(GetGameResult());
        }

        private int GetGameResult()
        {
            if (_citadel.damageble.CurrentHP >= _citadel.damageble.MaxHP * 0.85f)
                return 3;
            if (_citadel.damageble.CurrentHP >= _citadel.damageble.MaxHP * 0.50f)
                return 2;
            if (_citadel.damageble.CurrentHP >= _citadel.damageble.MaxHP * 0.25f)
                return 1;
            return 0;
        }
    }
}