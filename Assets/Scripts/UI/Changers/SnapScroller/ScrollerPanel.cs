using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    
    public class ScrollerPanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler{

        public RectTransform Rect { get; private set; }

        public event Action<ScrollerPanel> OnPanelClick;
    
        private void OnEnable() {
            Rect = this.transform as RectTransform;
        }

        public float GetWidth() => Rect.rect.width;
        
        public Vector2 GetPanelPossition() {
            return Rect.localPosition;
        }
        
        public void SetPanelPossition(Vector2 possition) {
            Rect.localPosition = possition;
        }

        public Vector2 GetPanelScale() {
            return Rect.localScale;
        }
        
        public void SetPanelScale(Vector2 scale) {
            Rect.localScale = scale;
        }
        
        public void OnPointerUp(PointerEventData eventData) {
            OnPanelClick?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData) { }
        
    }

}

