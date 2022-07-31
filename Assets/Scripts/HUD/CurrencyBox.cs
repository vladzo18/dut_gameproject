using General;
using TMPro;
using UnityEngine;

namespace HUD {
    
    public class CurrencyBox : MonoBehaviour, IResettable {

        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _diamontsText;
         
        public int CoinsAmount => int.Parse(_coinsText.text);
        public int DiamondsAmount => int.Parse(_diamontsText.text);
        
        private void Start() => GameReset.Register(this);
        private void OnDestroy() => GameReset.Unregister(this);
        
        public void AddCoins(float value) {
            _coinsText.text = (CoinsAmount + value).ToString();
        }
        
        public void AddDiamonds(float value) {
            _diamontsText.text = (DiamondsAmount + value).ToString();
        }

        public void Reset() {
            _coinsText.text = _diamontsText.text = "0";
        }
        
    }
    
}