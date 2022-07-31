using System;
using UnityEngine;
using UnityEngine.UI;

namespace HUD {
    
    public class FuelBar : MonoBehaviour {

        [SerializeField] private Gradient _gradient;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;

        public float MaxFuelAmount {
            set  => _slider.maxValue  = value;
            private get => _slider.maxValue;
        }
        
        private void Start() {
            _fill.color = _gradient.Evaluate(1);
        }
        
        public void SetFuelValue(float value) {
            if (MaxFuelAmount == 0) {
                throw new InvalidOperationException($"You must set {nameof(MaxFuelAmount)}");
            }
            if (value > MaxFuelAmount || value < 0) {
                throw new IndexOutOfRangeException($"{value} isn't fuel amount that can draw, max is {MaxFuelAmount}, min is 0");
            }
            _slider.value = value;
            _fill.color = _gradient.Evaluate(value /  MaxFuelAmount);
        }
        
    }
    
}
