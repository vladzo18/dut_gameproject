using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items {
    
    public class PedalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _putedPedal;
        
        private Sprite _unputtedPedal;

        public event Action OnPutPedal;
        public event Action OnUnputPedal;

        private void Start() {
            _unputtedPedal = _image.sprite;
        }

        public void OnPointerDown(PointerEventData eventData) {
            _image.sprite = _putedPedal;
            OnPutPedal?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData) {
            _image.sprite = _unputtedPedal;
            OnUnputPedal?.Invoke();
        }

    }

}
