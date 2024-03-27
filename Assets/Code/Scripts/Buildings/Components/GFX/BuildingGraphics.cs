using System.Collections.Generic;
using System.Linq;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.Components
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

            var renderer = _graphicsInstance.GetComponentInChildren<MeshRenderer>();
            renderer.renderingLayerMask = 0x10;
        }
    }
}