using System;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class CurrencyBox : MonoBehaviour {

        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _diamontsText;
         
        public int CoinsAmount => Int32.Parse(_coinsText.text);
        public int DiamontsAmount => Int32.Parse(_diamontsText.text);
        
        public void AddCoins(float value) {
            _coinsText.text = (CoinsAmount + value).ToString();
        }
        
        public void AddDiamants(float value) {
            _diamontsText.text = (DiamontsAmount + value).ToString();
        }
        
    }
    
}