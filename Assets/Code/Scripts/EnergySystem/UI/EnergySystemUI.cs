using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SustainTheStrain.EnergySystem.UI
{
    public class EnergySystemUI : MonoBehaviour
    {
        [field: SerializeField] public EnergySystemControllButton ControllButton { get; private set; }
        
        [SerializeField] private Image _imagePrefab;
        [SerializeField] private Image _icon;
        [SerializeField] private RectTransform _barsHolder;
        [SerializeField] private Color _filledColor;
        [SerializeField] private TMP_Text _tipText;
        
        private List<Image> _images = new();
        private int _coloredCount = 0;
        private int _enabledCount = 0;

        public TMP_Text Tip => _tipText;
        
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
        
        // private void SetMax(int value)
        // {
        //     MaxBarsCount = value;
        // }
        
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
            _images.Add(Instantiate(_imagePrefab, _barsHolder));
            _enabledCount++;
        }

        public void ChangeEnergy(IEnergySystem system)
        {
            if (system.CurrentEnergy < 0 || system.CurrentEnergy > _images.Count) return;
            _coloredCount = system.CurrentEnergy;
            ReColorBars();
        }

        public void SetIcon(Sprite sprite)
        {
            if (sprite == null) return;

            _icon.sprite = sprite;
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
            img.transform.Activate();
        }

        private void UnloadBar(Image img)
        {
            img.transform.Deactivate();
        }
        
    }
}
