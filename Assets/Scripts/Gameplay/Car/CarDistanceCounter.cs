using System;
using UnityEngine;

namespace Car {
    
    public class CarDistanceCounter : MonoBehaviour {

        [SerializeField] private Transform _moveingObjectTransform;
        
        private const int METERS_DIVIDER = 2;
        
        public int MeterCount { get; set; }
        public event Action OnMeterCountChanged;
        
        private void Update() {
            if (_moveingObjectTransform.position.x < METERS_DIVIDER && _moveingObjectTransform.position.x % METERS_DIVIDER != 0) {
                return;
            }
            
            int newCount = (int) (_moveingObjectTransform.position.x / METERS_DIVIDER);
            
            MeterCount = newCount;
            OnMeterCountChanged?.Invoke();
        }
        
    }
    
}