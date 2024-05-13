using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using SustainTheStrain.Scriptable;
using SustainTheStrain.Units.Spawners;
using UnityEngine;

namespace SustainTheStrain.Level
{
    public class WavesManager : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;
        [SerializeField] private List<EnemySpawner> _spawners;

        private bool _skipWaveDelay = false;

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

        public LevelData LevelData { get => _levelData; }

        private readonly Dictionary<int, bool> _waveCoroutines = new();
        private int _currentWave = 0;
        private bool _waveInProgress;
        
        public event Action OnLastWaveEnded;
        public event Action<int> OnWaveEnded;
        public event Action<int> OnWaveStarted;
        public event Action<int> OnWaveStartedDelayed;

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
            _skipWaveDelay = false;
            OnWaveStarted?.Invoke(_currentWave);
            _waveCoroutines.Clear();
            _waveInProgress = true;
            _skipWaveDelay = false;

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

        public void SkipWaveDelay()
        {
            _skipWaveDelay = true;
        }

        private IEnumerator Wave(SpawnerPart part, EnemySpawner spawner, int index)
        {
            var timer = new Timer(part.delay);

            while (!timer.IsOver && !_skipWaveDelay)
            {
                timer.Tick();
                yield return null;
            }

            OnWaveStartedDelayed?.Invoke(_currentWave);

            foreach (var subwave in part.subwaves)
            {
                for (int i = 0; i < subwave.enemyCount; i++)
                {
                    foreach (var groupItem in subwave.enemyGroup)
                    {
                        var enemy = spawner.Spawn(groupItem.enemyType);
                        enemy.RoadOffset = groupItem.xOffset;
                    }
                    yield return new WaitForSeconds(subwave.spawnPeriod);
                }
                yield return new WaitForSeconds(subwave.delay);
            }

            _waveCoroutines[index] = true;
        }
    }
}
