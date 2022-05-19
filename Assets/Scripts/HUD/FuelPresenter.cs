using Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace HUD {
    
    public class FuelPresenter : MonoBehaviour {

        [SerializeField] private Gradient _gradient;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;
        [SerializeField] private CarTank _carTank;

        private void Start() {
            _carTank.OnFuelAmountChanged += UpdateFuelBar;
            _slider.maxValue = _carTank.FuelMaxAmount;
            _slider.value = _carTank.FuelMaxAmount;
            _fill.color = _fill.color = _gradient.Evaluate(1);
        }

        private void OnDestroy() {
            _carTank.OnFuelAmountChanged -= UpdateFuelBar;
        }

        private void UpdateFuelBar(float value) {
            _slider.value = value;
            _fill.color = _gradient.Evaluate(value / _carTank.FuelMaxAmount);
        }
        
    }
    
}
