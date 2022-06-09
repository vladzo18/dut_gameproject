using TMPro;
using UnityEngine;

namespace UI {
    
    public class CurrencyBox : MonoBehaviour {
        
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _diamontsText;

        public int CurrentCoins => int.Parse(_coinsText.text);
        public int CurrentDiamonds => int.Parse(_diamontsText.text);

        public void AddCoins(int value) => _coinsText.text = (CurrentCoins + value).ToString();
        public void AddDiamonds(int value) => _diamontsText.text = (CurrentDiamonds + value).ToString();

        public bool TryTakeCoins(int value) {
            if (CurrentCoins < value) return false;
            _coinsText.text = (CurrentCoins - value).ToString();
            return true;
        }
        
        public bool TryTakeDiamonds(int value) {
            if (CurrentDiamonds < value) return false;
            _diamontsText.text = (CurrentDiamonds - value).ToString();
            return true;
        }
        
    }
}