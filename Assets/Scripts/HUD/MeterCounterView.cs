using Car;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class MeterCounterView : MonoBehaviour {

        [SerializeField] private CarDistanceCounter _distanceCounter;
        [SerializeField] private TMP_Text _text;

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
