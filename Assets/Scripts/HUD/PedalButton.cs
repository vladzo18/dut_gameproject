using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts {
    
    public class PedalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _pedal;
        [SerializeField] private Sprite _putedPedal;

        public event Action OnPutPedal;
        public event Action OnUnputPedal;
    
        public void OnPointerDown(PointerEventData eventData) {
            _image.sprite = _putedPedal;
            OnPutPedal?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData) {
            _image.sprite = _pedal;
            OnUnputPedal?.Invoke();
        }

    }

}
