using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameInput {
    
    public class PedalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _putedPedal;
        
        private Sprite _notPressedPedal;

        public event Action OnPutPedal;
        public event Action OnReleasePedal;

        private void Start() {
            _notPressedPedal = _image.sprite;
        }

        public void OnPointerDown(PointerEventData eventData) {
            _image.sprite = _putedPedal;
            OnPutPedal?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData) {
            _image.sprite = _notPressedPedal;
            OnReleasePedal?.Invoke();
        }

    }

}
