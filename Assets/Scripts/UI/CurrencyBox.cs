using System;
using TMPro;
using UnityEngine;

namespace UI {
    
    public class CurrencyBox : MonoBehaviour {
        
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _diamontsText;

        public int CurrentCoins => Int32.Parse(_coinsText.text);
        public int CurrentDiamonts => Int32.Parse(_diamontsText.text);

        public void AddCoins(int value) => _coinsText.text = (CurrentCoins + value).ToString();
        public void AddDiamonts(int value) => _diamontsText.text = (CurrentDiamonts + value).ToString();

        public bool TryTakeCoins(int value) {
            if (CurrentCoins < value) return false;
            _coinsText.text = (CurrentCoins - value).ToString();
            return true;
        }
        
        public bool TryTakeDiamonts(int value) {
            if (CurrentDiamonts < value) return false;
            _diamontsText.text = (CurrentDiamonts - value).ToString();
            return true;
        }

        private void Start() {
            _coinsText.text = "0";
            _diamontsText.text = "0";
        }
        
    }
}