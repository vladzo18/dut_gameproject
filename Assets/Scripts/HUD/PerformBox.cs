using TMPro;
using UnityEngine;

namespace HUD {
    
    public class PerformBox : MonoBehaviour {
        
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Animator _animator;
        
        private const string SHOW_ANIMATIO_KEY = "Show";
        
        private void Start() {
            this.gameObject.SetActive(false);
        } 

        public void SetPerformBox(int amount) {
            string symbol = amount > 0 ? "+" : "";
            _amountText.text = $"{symbol}{amount}";
        }

        public void Show() {
            this.gameObject.SetActive(true);
            _animator.Play(SHOW_ANIMATIO_KEY);
        }
        
    }
    
}