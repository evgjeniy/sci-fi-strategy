using SustainTheStrain.Units;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Enemy _refEnemyPrefab;
        public override void InstallBindings()
        {
            Container.Bind<IFactory<Enemy>>().FromInstance(new Enemy.Factory(_refEnemyPrefab)).AsSingle();
        }
    }
}