using TMPro;
using UnityEngine;

namespace HUD {
    
    public class MeterCountBar : MonoBehaviour {
        
        [SerializeField] private TMP_Text _text;
        
        public int CurrentMetersCount { get; private set; }
        
        public void AddOneMeter(int value) {
            CurrentMetersCount = value;
            _text.text = $"{CurrentMetersCount} m";
        }
        
    }
    
}
