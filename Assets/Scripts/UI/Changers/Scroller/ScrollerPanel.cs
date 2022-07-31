using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Changers.Scroller {
    
    public class ScrollerPanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler{

        public RectTransform Rect { get; private set; }

        public event Action<ScrollerPanel> OnPanelClick;
    
        private void OnEnable() {
            Rect = this.transform as RectTransform;
        }

        public float GetWidth() => Rect.rect.width;
        
        public Vector2 GetPanelPosition() {
            return Rect.localPosition;
        }
        
        public void SetPanelPosition(Vector2 position) {
            Rect.localPosition = position;
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

