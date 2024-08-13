using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using Zenject;

public class TestUI : MonoBehaviour
{
    [Inject] private ResourceManager manager;
    [Inject] private EnergyController energyManager;
    
    
    void Start()
    {
        
    }
    
    
    public void ZeroEnergy()
    {
        foreach (var system in energyManager.Systems)
        {
            for (int i=0; i<= system.CurrentEnergy; i++)
            {
                energyManager.TryReturnEnergyFromSystem(system);
            }
        }
    }
}
