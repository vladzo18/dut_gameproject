using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Changers.CarPropertyTuner {
    
    public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _puttedButton;

        private Sprite _unputtedButton;
        
        public event Action OnUpgradeButtonClick;
        
        private void Start() {
            _unputtedButton = _image.sprite;
        }
        
        public void OnPointerDown(PointerEventData eventData) {
           OnUpgradeButtonClick?.Invoke();
           _image.sprite = _puttedButton;
        }

        public void OnPointerUp(PointerEventData eventData) => _image.sprite = _unputtedButton;
        
    }
    
}