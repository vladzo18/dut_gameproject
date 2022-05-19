using System;
using Scripts;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class CoinsPresenter : MonoBehaviour {

        [SerializeField] private TMP_Text _text;
        [SerializeField] private CarCollector _carCollector;
        
        public int CoinsAmount => Int32.Parse(_text.text);

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