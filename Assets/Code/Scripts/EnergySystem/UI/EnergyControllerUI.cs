using System.Collections.Generic;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI
{
    public class EnergyControllerUI : MonoBehaviour
    {
        [SerializeField] private Image _imagePrefab;
        [SerializeField] private Color _filledColor;
        [SerializeField] private EnergyCellBuyButton _buyButton;
        [SerializeField] private RectTransform _energyHolder;

        private EnergyController _energyController;
        private List<Image> _images = new();
        
        private int _coloredCount = 0;
        private int _enabledCount = 0;
        
        [Inject] 
        public void Bind(EnergyController controller)
        {
            _energyController = controller;
            var manager = _energyController.Manager;
            MaxBarsCount = manager.MaxCount;
            manager.OnEnergyChanged += ChangeEnergy;
            manager.OnMaxEnergyChanged += SetMax;
            manager.OnUpgradeCostChanged += _buyButton.SetUpgradeCost;
        }

        [Inject]
        public void BindResourceManager(ResourceManager manager)
        {
            EnergyController.CheckGoldDelegate delegat = manager.TrySpend;
            _energyController.SetCheckGoldAction(delegat);
            _buyButton.MButton.onClick.AddListener(_energyController.BuyMaxEnergy);
        }
        
        public int MaxBarsCount
        {
            get => _enabledCount;
            set
            {
                if (value <= 0) return;
                if (_images.Count == 0)
                {
                    for (int i = 0; i < value; i++)
                    {
                        SpawnNewBar();
                    }
                }
                else {SetMaxBarsCount(value);}
            }
        }

        
        private void SetMax(int value)
        {
            MaxBarsCount = value;
        }

        private void SetMaxBarsCount(int value)
        {
            if (value > _enabledCount)
            {
                AddBars(value-_enabledCount);
            }
            else
            {
                DeleteBars(_enabledCount-value);
            }
        }

        private void SpawnNewBar()
        {
            _images.Add(Instantiate(_imagePrefab, _energyHolder));
            _enabledCount++;
        }

        private void ChangeEnergy(int count)
        {
            if (count < 0 || count > _images.Count) return;
            _coloredCount = count;
            ReColorBars();
        }

        private void AddBars(int count)
        {
            int i = 0;
            for (; i+_enabledCount < _images.Count; i++)
            {
                _images[i+_enabledCount].enabled = true;
            }

            _enabledCount += i;
            for (; i < count; i++)
            {
                SpawnNewBar();
            }
            
        }

        private void DeleteBars(int count)
        {
            int i = _enabledCount-1;
            for (; i >= _enabledCount - count; i--)
            {
                _images[i].enabled = false;
            }
            _enabledCount = i+1;
        }

        private void ReColorBars()
        {
            int ind = 0;
            for(; ind < _coloredCount; ind++)
            {
                LoadBar(_images[ind]);
            }
            for (; ind < _images.Count; ind++)
            {
                UnloadBar(_images[ind]);
            }
        }

        private void LoadBar(Image img)
        {
            img.color = _filledColor;
        }

        private void UnloadBar(Image img)
        {
            img.color = _imagePrefab.color;
        }

        private void OnDisable()
        {
            var manager = _energyController.Manager;
            manager.OnEnergyChanged -= ChangeEnergy;
            manager.OnMaxEnergyChanged -= SetMax;
            manager.OnUpgradeCostChanged -= _buyButton.SetUpgradeCost;
        }
    }
}