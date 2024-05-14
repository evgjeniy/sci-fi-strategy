using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private RectTransform _activeElement;

        public void Off()
        {
            _activeElement.Deactivate();
        }

        public void On()
        {
            _activeElement.Activate();
        }
    }
}
