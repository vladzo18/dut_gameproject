using Car;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class CoinsView : MonoBehaviour {

        [SerializeField] private TMP_Text _text;
        [SerializeField] private CarCollector _carCollector;

        private void Start() {
            _carCollector.OnCoinCollect += OnCoinTake;
        }

        private void OnDestroy() {
            _carCollector.OnCoinCollect -= OnCoinTake;
        }
        
        private void OnCoinTake(float amount) {
            _text.text = (int.Parse(_text.text) + amount).ToString();
        }
        
    }
    
}