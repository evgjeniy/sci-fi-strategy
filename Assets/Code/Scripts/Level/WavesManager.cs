using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Spawners;
using UnityEngine;

namespace SustainTheStrain.Level
{
    public class WavesManager : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;
        [SerializeField] private List<EnemySpawner> _spawners;

        public int EnemiesAlive
        {
            get
            {
                int enemies = 0;
                foreach (var spawner in _spawners)
                {
                    enemies += spawner.SpawnedEnemiesAlive;
                }

                return enemies;
            }
        }


        private readonly Dictionary<int, bool> _waveCoroutines = new();
        private int _currentWave = 0;
        private bool _waveInProgress;
        
        public event Action OnLastWaveEnded;
        public event Action<int> OnWaveEnded;
        public event Action<int> OnWaveStarted; 

        private void Update()
        {
            if (_waveInProgress)
            {
                foreach (var coroutineProgress in _waveCoroutines)
                {
                    if(!coroutineProgress.Value) return;
                }

                WaveEnded();
            }
        }

        [Button("StartWaves")]
        public void StartWaves()
        {
            StartWave(_levelData.waves[0]);
        }
        
        private void StartWave(WaveData waveData)
        {
            OnWaveStarted?.Invoke(_currentWave);
            _waveCoroutines.Clear();
            _waveInProgress = true;
            foreach (var spawnerPart in waveData._spawners)
            {
                Debug.Log($"[WaveManager] Wave {_currentWave} started");
                if (spawnerPart.index >= _spawners.Count)
                {
                    Debug.LogError($"[WaveManager] No such spawner with index {spawnerPart.index}");
                    continue;
                }
                
                _waveCoroutines.Add(spawnerPart.index, false);
                
                StartCoroutine(Wave(spawnerPart, _spawners[spawnerPart.index], spawnerPart.index));
            }
            
        }

        private void WaveEnded()
        {
            OnWaveEnded?.Invoke(_currentWave);
            Debug.Log($"[WaveManager] Wave {_currentWave} ended");
            _waveInProgress = false;
            if (_levelData.waves.Count <= _currentWave + 1)
            {
                OnLastWaveEnded?.Invoke();
                return;
            }
            
            NextWave();
        }
        
        private void NextWave()
        {
            _currentWave++;
            StartWave(_levelData.waves[_currentWave]);
        }

        private IEnumerator Wave(SpawnerPart part, Spawner<Enemy> spawner, int index)
        {
            if (part.delay > 0)
                yield return new WaitForSeconds(part.delay);

            foreach (var subwave in part.subwaves)
            {
                for (int i = 0; i < subwave.enemyCount; i++)
                {
                    spawner.Spawn();
                    yield return new WaitForSeconds(subwave.spawnPeriod);
                }
                yield return new WaitForSeconds(subwave.delay);
            }

            _waveCoroutines[index] = true;
        }
    }
}
