using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SustainTheStrain
{
    public class EnergySystemUI : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnPlace;

        [SerializeField] private Image _imagePrefab;

        [SerializeField] private List<Image> _images = new();

        [SerializeField] private Color _filledColor;

        public int BarsCount
        {
            get => _images.Count;
            set
            {
                if (value <= 0) return;
                for (int i = 0; i < value; i++)
                {
                    SpawnNewBar();
                }
            }
        }

        private int _coloredCount = 0;

        public void SpawnNewBar()
        {
            _images.Add(Instantiate(_imagePrefab, _spawnPlace.transform));
        }

        public void AddEnergy(int count)
        {
            if (_coloredCount + count <= _images.Count)
            {
                _coloredCount += count;
            }
            ReColorBars();
        }

        public void DeleteEnergy(int count)
        {
            if (_coloredCount - count >= 0)
            {
                _coloredCount -= count;
            }

            ReColorBars();
        }

        public void ReColorBars()
        {
            int ind = 0;
            for(; ind < _coloredCount; ind++)
            {
                var img = _images[ind];
                img.color = _filledColor;
            }

            for (; ind < _images.Count; ind++)
            {
                _images[ind].color = _imagePrefab.color;
            }
        }
        
    }
}
