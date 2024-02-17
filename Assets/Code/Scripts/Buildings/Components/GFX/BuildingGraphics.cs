using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Extensions;
using SustainTheStrain.Buildings.Data;

namespace SustainTheStrain.Buildings.Components.GFX
{
    public class BuildingGraphics<TStats> where TStats : BuildingData.IStats, new()
    {
        private readonly Building _building;
        private readonly GameObject[] _graphicsObjects;
        
        private GameObject _graphicsInstance;

        public BuildingGraphics(Building building, IEnumerable<PricedLevelStats<TStats>> stats)
        {
            _building = building;
            _building.OnLevelUpgrade += UpdateGraphics;
            _graphicsObjects = stats.Select(s => s.Graphics).ToArray();
        }
        
        public void Destroy() => _building.OnLevelUpgrade -= UpdateGraphics;

        private void UpdateGraphics(int currentLevel)
        {
            Object.Destroy(_graphicsInstance);
            
            _graphicsInstance = _graphicsObjects[currentLevel].Spawn(_building.transform);
            _graphicsInstance.transform.position = _building.transform.position;
        }
    }
}