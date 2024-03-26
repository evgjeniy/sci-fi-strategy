using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BuildingMenuView : MonoBehaviour
    {
        [SerializeField] private Canvas _menuRoot;

        public void Enable() => _menuRoot.Activate();
        public void Disable() => _menuRoot.IfNotNull(x => x.Deactivate());
    }
}