using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using VolumetricFogAndMist;
using Zenject;

public class TestUI : MonoBehaviour
{
    [Inject] private ResourceManager resourceManager;
    [Inject] private EnergyController energyManager;
    
    
    void Start()
    {
        
    }

    public void SetGold(int amount){

        int minAmount = 0;
        int maxAmount = 999999;

        int clampedAmount= Mathf.Clamp(amount, minAmount, maxAmount);

        int currentGold = resourceManager.Gold;
        int diff = clampedAmount - currentGold;

        resourceManager.AddGold(diff);
    }

    public void AddGold(int amount){

        int currentGold = resourceManager.Gold;
        int sum = currentGold + amount;

        SetGold(sum);
            
    }
    
    
    public void ZeroEnergy()
    {
        foreach (var system in energyManager.Systems)
        {
            for (int i=0; i<= system.CurrentEnergy + 1; i++)
            {
                energyManager.TryReturnEnergyFromSystem(system);
            }
        }
    }
}
