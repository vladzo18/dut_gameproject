using System;
using General;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class CurrencyBox : MonoBehaviour, IResetable {

        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _diamontsText;
         
        public int CoinsAmount => Int32.Parse(_coinsText.text);
        public int DiamontsAmount => Int32.Parse(_diamontsText.text);
        
        private void Start() => GameReset.Register(this);
        private void OnDestroy() => GameReset.Unregister(this);
        
        public void AddCoins(float value) {
            _coinsText.text = (CoinsAmount + value).ToString();
        }
        
        public void AddDiamants(float value) {
            _diamontsText.text = (DiamontsAmount + value).ToString();
        }

        public void Reset() {
            _coinsText.text = _diamontsText.text = "0";
        }
        
    }
    
}