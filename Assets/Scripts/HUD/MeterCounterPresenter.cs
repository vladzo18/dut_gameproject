using Car;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class MeterCounterPresenter : MonoBehaviour {

        [SerializeField] private CarDistanceCounter _distanceCounter;
        [SerializeField] private TMP_Text _text;
        
        public void SetCarDistanceCounter(CarDistanceCounter counter) => _distanceCounter = counter;

        private void Start() {
            _distanceCounter.OnMeterCountChanged += UpdateCounter;
        }

        private void OnDestroy() {
            _distanceCounter.OnMeterCountChanged -= UpdateCounter;
        }

        private void UpdateCounter() {
            _text.text = $"{_distanceCounter.MeterCount} m";
        }
        
    }
    
}
