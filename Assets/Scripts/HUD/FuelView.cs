using System;
using System.Collections.Generic;
using Car;
using UnityEngine;
using UnityEngine.UI;

namespace HUD {
    
    public class FuelView : MonoBehaviour {

        [SerializeField] private List<FuelMark> _fuelMarks;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;
        [SerializeField] private CarTank _carTank;

        private void Start() {
            _carTank.OnFuelAmountChanged += UpdateFuelBar;
            _slider.maxValue = _carTank.FuelMaxAmount;
            _slider.value = _carTank.FuelMaxAmount;
        }

        private void OnDestroy() {
            _carTank.OnFuelAmountChanged -= UpdateFuelBar;
        }

        private void UpdateFuelBar(float value) {
            float currentPercent = value / (_carTank.FuelMaxAmount / 100);
            FuelMark fuelMark = _fuelMarks.Find(mark => currentPercent <= mark.BelowPercent);
            _slider.value = value;
            _fill.color = fuelMark.Color;
        }
        
        [Serializable]
        private struct FuelMark {
            [Range(0, 100)] public float BelowPercent;
            public Color Color;
        }
        
    }
    
}
