using General;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class MeterCountBar : MonoBehaviour, IResetable {
        
        [SerializeField] private TMP_Text _text;
        
        public int CurrentMetersCount { get; private set; }
        
        private void Start() => GameReset.Register(this);
        private void OnDestroy() => GameReset.Unregister(this);
        
        public void SetMeters(int value) {
            CurrentMetersCount = value;
            _text.text = $"{CurrentMetersCount} m";
        }

        public void Reset() {
            SetMeters(0);
        }
        
    }
    
}
