using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    public interface IDataProvider<T>
    {
        public T Value { get; }
    }
}
