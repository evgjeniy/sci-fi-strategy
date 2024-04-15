using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain
{
    [RequireComponent(typeof(Button))]
    public class EnergyCellBuyButton : MonoBehaviour
    {
        [field : SerializeField]public Button MButton { get; private set; }
        [field : SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        
        public void SetUpgradeCost(int cost)
        {
            _textMeshProUGUI.text = cost.ToString();
        }
    }
}
