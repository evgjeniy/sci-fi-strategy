using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems
{
    public class EnergyEntryPoint : MonoBehaviour
    {
        [SerializeField] private EnergyManager manager;
        [SerializeField] private EnergyUI _ui;

        private void OnEnable()
        {
            _ui.Bind(manager);
        }
    }
}