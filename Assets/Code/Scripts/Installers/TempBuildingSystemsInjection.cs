using SustainTheStrain.Buildings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class TempBuildingSystemsInjection : MonoBehaviour
    {
        [Inject]
        private void Construct(RocketSystem system, LaserSystem laserSystem, ArtillerySystem artillerySystem) {}
    }
}