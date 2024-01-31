using SustainTheStrain.Units;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Enemy _refEnemyPrefab;
        [SerializeField] private Recruit _refRecruitPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<IFactory<Enemy>>().FromInstance(new Enemy.Factory(_refEnemyPrefab)).AsSingle();
            Container.Bind<IFactory<Recruit>>().FromInstance(new Recruit.Factory(_refRecruitPrefab)).AsSingle();
        }
    }
}