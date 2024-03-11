using SustainTheStrain.Units;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Bestiary _bestiary;
        [Header("Temp")]
        [SerializeField] private Recruit _refRecruitPrefab;
        [SerializeField] private Hero _hero;

        public override void InstallBindings()
        {
            Container.Bind<EnemyFactoryManager>().FromInstance(BindFactories(_bestiary)).AsSingle();

            Container.Bind<IFactory<Recruit>>().FromInstance(new Recruit.Factory(_refRecruitPrefab)).AsSingle();
            Container.Bind<Hero>().FromInstance(_hero).AsSingle();
        }

        private EnemyFactoryManager BindFactories(Bestiary bestiary)
        {
            EnemyFactoryManager factoryManager = new EnemyFactoryManager();
            foreach(var enemy in bestiary.Catalogue)
            {
                if (enemy.enemy == null)
                {
                    Debug.LogWarning($"[EnemyInstaller] Bestiary item with name {enemy.name} dont has prefab");
                    continue;
                }
                if (!factoryManager.Factories.TryAdd(enemy.name, new Enemy.Factory(enemy.enemy)))
                {
                    Debug.LogWarning($"[EnemyInstaller] Bestiary has duplicate names - {enemy.name}");
                    continue;
                }
            }

            return factoryManager;
        }
    }
}