using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Changers.CarPropertyTuner {
    
    public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _pressedButton;

        private Sprite _notPressedButton;
        
        public event Action OnUpgradeButtonClick;
        
        private void Start() {
            _notPressedButton = _image.sprite;
        }
        
        public void OnPointerDown(PointerEventData eventData) {
           OnUpgradeButtonClick?.Invoke();
           _image.sprite = _pressedButton;
        }

        public void OnPointerUp(PointerEventData eventData) => _image.sprite = _notPressedButton;
        
    }
    
}