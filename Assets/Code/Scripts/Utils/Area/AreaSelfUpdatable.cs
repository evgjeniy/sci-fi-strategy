using System;
using Zenject;

namespace SustainTheStrain
{
    public class AreaSelfUpdatable<TComponent> : Area<TComponent>, ITickable
    {
        private readonly IAreaDataProvider _areaDataProvider;

        public AreaSelfUpdatable(IAreaDataProvider areaDataProvider, int bufferMaxSize = 32, params Func<TComponent, bool>[] conditions) : base(bufferMaxSize, conditions)
        {
            _areaDataProvider = areaDataProvider;
        }

        public void Tick() => Update
        (
            _areaDataProvider.Center,
            _areaDataProvider.Radius,
            _areaDataProvider.Mask
        );
    }
}