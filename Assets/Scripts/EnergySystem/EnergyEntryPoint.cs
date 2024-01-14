using System;
using UnityEngine;

namespace Systems
{
    public class EnergyEntryPoint : MonoBehaviour
    {
        [SerializeField] private EnergySystem _system;
        [SerializeField] private EnergyUI _ui;

        private void OnEnable()
        {
            _ui.Bind(_system);
        }
    }
}