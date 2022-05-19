using System;
using UnityEngine;

namespace Car {
    
    public class CarDistanceCounter : MonoBehaviour {
        
        private const int METERS_DIVIDER = 2;
        
        public int MeterCount { get; set; }
        public event Action OnMeterCountChanged;
        
        private void Update() {
            if (this.transform.position.x < METERS_DIVIDER && this.transform.position.x % METERS_DIVIDER != 0) {
                return;
            }
            
            int newCount = (int) (this.transform.position.x / METERS_DIVIDER);
            
            MeterCount = newCount;
            OnMeterCountChanged?.Invoke();
        }
        
    }
    
}