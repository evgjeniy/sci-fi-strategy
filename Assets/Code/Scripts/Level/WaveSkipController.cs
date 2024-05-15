using Cysharp.Threading.Tasks;
using SustainTheStrain.Level;
using SustainTheStrain.Scriptable;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace SustainTheStrain
{
    public class WaveSkipController : MonoBehaviour
    {
        [SerializeField]
        private List<SkipInitiator> _skipInitiators;

        private WavesManager _waveManager;
        [SerializeField] private Bestiary _bestiary;

        [Inject]
        private void Construct(WavesManager waveManager)
        {
            _waveManager = waveManager;
            waveManager.OnWaveStarted += WaveStarted;
            waveManager.OnWaveStartedDelayed += _ => { DeactivateInitiators(); };
        }

        private void WaveStarted(int wave)
        {
            ActivateInitiators(wave);
        }

        public void SkipInitiated()
        {
            DeactivateInitiators();

            _waveManager.SkipWaveDelay();
        }

        private void ActivateInitiators(int wave)
        {
            foreach (var initiator in _skipInitiators.Where((init)=>
            {
                foreach (var spawner in _waveManager.LevelData.waves[wave]._spawners)
                {
                    if (init.SpawnerIndex == spawner.index) return true;
                }
                return false;
            }))
            {
                List<string> enemyeTypes = new List<string>();

                foreach (var subwave in _waveManager.LevelData.waves[wave]._spawners.Find((i) => { return i.index == initiator.SpawnerIndex; }).subwaves)
                    foreach (var group in subwave.enemyGroup)
                    {
                        if (!enemyeTypes.Contains(_bestiary.GetFullName(group.enemyType)))
                        {
                            enemyeTypes.Add(_bestiary.GetFullName(group.enemyType));
                        }
                    }
                initiator.ActivateInit(_waveManager.LevelData.waves[wave]._spawners[0].delay, enemyeTypes);
            }
        }

        private void DeactivateInitiators()
        {
            foreach (var initiator in _skipInitiators)
            {
                initiator.DeactivateInit();
            }
        }
    }
}
