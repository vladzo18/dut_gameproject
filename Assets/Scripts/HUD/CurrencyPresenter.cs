using System;
using Scripts;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class CurrencyPresenter : MonoBehaviour {

        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _diamontsText;
        [SerializeField] private CarCollector _carCollector;
        
        public int CoinsAmount => Int32.Parse(_coinsText.text);
        public int DiamontsAmount => Int32.Parse(_diamontsText.text);
        
        public void SetCarCollector(CarCollector carCollector) => _carCollector = carCollector;

        private void Start() {
            _carCollector.OnCoinCollect += OnCoinTake;
            _carCollector.OnDiamontCollect += OnDiamontTake;
        }
        
        private void OnDestroy() {
            _carCollector.OnCoinCollect -= OnCoinTake;
            _carCollector.OnDiamontCollect -= OnDiamontTake;
        }
        
        private void OnDiamontTake(float amount) {
            _diamontsText.text = (DiamontsAmount + amount).ToString();
        }
        
        private void OnCoinTake(float amount) {
            _coinsText.text = (CoinsAmount + amount).ToString();
        }
        
    }
    
}