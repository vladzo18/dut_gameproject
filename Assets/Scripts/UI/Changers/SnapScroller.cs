using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    
    [RequireComponent(typeof(ScrollRect))]
    public class SnapScroller : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

        [SerializeField, Range(10, 30)] private int _moveSpeed;
        [SerializeField, Range(1f, 2f)] private float _scaleOffset;
        [SerializeField, Range(0, 1)] private float _defaultSize;
        [SerializeField] private float _panelsOfset;
        [SerializeField] private ScrollerPanel _scrollerPanelPrefsb;
        
        private ScrollRect _scrollRect;
        private RectTransform _contentContainer;
        private List<ScrollerPanel> _scrollerPanels;
        private bool _isScrollibg;
        private int _selectedItemIndex;
        private float _lastContentPossitionX ;
        
        private Vector2 _contentPossition;
        private Vector2 _contentScale;

        public IEnumerable<ScrollerPanel> ScrollerPanels => _scrollerPanels;

        public event Action OnContentChanged;

        public void AddPanels(int count) {
            for (int i = 0; i < count; i++) {
                ScrollerPanel panel = Instantiate(_scrollerPanelPrefsb, _contentContainer.transform);
                Vector2 newPoss = new Vector2(
                    (panel.GetWidth() * i) + _panelsOfset,
                    -_contentContainer.anchoredPosition.y);
                
                panel.SetPanelPossition(newPoss);
                _scrollerPanels.Add(panel);
                panel.OnPanelClick += OnPanelClick;
            }
            
            _contentContainer.anchoredPosition = -_scrollerPanels[0].GetPanelPossition();
            _contentPossition = -_scrollerPanels[0].GetPanelPossition();
            _scrollerPanels[0].SetPanelScale(Vector3.one);
        }

        public ScrollerPanel GetPanelAt(int index) {
            if (index < 0 || index > _scrollerPanels.Count - 1) {
                throw new NullReferenceException($"There's no panel at {index} index");
            }

            return _scrollerPanels[index];
        }
        
        private void Start() {
            _scrollRect = this.GetComponent<ScrollRect>();
            _contentContainer = _scrollRect.content;
            _scrollerPanels = new List<ScrollerPanel>();
            
            _scrollRect.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnPanelClick(ScrollerPanel scrollerPanel) {
            if (!_isScrollibg) {
                _selectedItemIndex = _scrollerPanels.IndexOf(scrollerPanel);
            }
        }

        private void OnDestroy() {
            _scrollRect.onValueChanged.RemoveListener(OnValueChanged);
            foreach (var panel in _scrollerPanels) {
                panel.OnPanelClick -= OnPanelClick;
            }
        }
        
        private void OnValueChanged(Vector2 vector2) {
            float deltaContentPossitionX = Mathf.Abs(_contentContainer.localPosition.x - _lastContentPossitionX);
            _lastContentPossitionX = _contentContainer.localPosition.x;
            
            if (deltaContentPossitionX < 0.5f) {
                _scrollRect.inertia = false;
                _selectedItemIndex = CurentCenterPanelIndex();
            }
        }
        
        private void Update() {
            if (_scrollerPanels.Count == 0) return;
            
            if (_contentContainer.anchoredPosition.x <= -_scrollerPanels[_scrollerPanels.Count - 1].GetPanelPossition().x) {
                _contentContainer.anchoredPosition = new Vector2(-_scrollerPanels[_scrollerPanels.Count - 1].GetPanelPossition().x,  _contentContainer.anchoredPosition.y);
            }  
            if (_contentContainer.anchoredPosition.x >= -_scrollerPanels[0].GetPanelPossition().x) {
                _contentContainer.anchoredPosition = new Vector2(-_scrollerPanels[0].GetPanelPossition().x,  _contentContainer.anchoredPosition.y);
            }  
            
            UpdateScales();
            
            if (_isScrollibg || _scrollRect.inertia) return;

            _contentPossition.x = Mathf.SmoothStep( _contentContainer.anchoredPosition.x, -_scrollerPanels[_selectedItemIndex].GetPanelPossition().x, 25f * Time.deltaTime);
            _contentContainer.anchoredPosition = _contentPossition;
        }
        
        public void OnBeginDrag(PointerEventData eventData) {
            _isScrollibg = true;
            _scrollRect.inertia = true;
        }
        
        public void OnEndDrag(PointerEventData eventData) {
            _isScrollibg = false;
        }

        private void UpdateScales() {
            for (int i = 0; i < _scrollerPanels.Count; i++) {
                float distance = Mathf.Abs(_contentContainer.anchoredPosition.x + _scrollerPanels[i].GetPanelPossition().x);
                float scale = Mathf.Clamp(1f / (distance / _panelsOfset) * _scaleOffset, _defaultSize, 1f);
                
                _contentScale.x = Mathf.SmoothStep(_scrollerPanels[i].GetPanelScale().x, scale,_moveSpeed * Time.deltaTime);
                _contentScale.y = Mathf.SmoothStep(_scrollerPanels[i].GetPanelScale().y, scale,_moveSpeed * Time.deltaTime);
                _scrollerPanels[i].SetPanelScale(_contentScale);
            }
        }
        
        private int CurentCenterPanelIndex() {
            float nearestPossition = float.MaxValue;
            int selectedItemIndex = -1;
            
            for (int i = 0; i < _scrollerPanels.Count; i++) {
                float distance = Mathf.Abs(_contentContainer.anchoredPosition.x + _scrollerPanels[i].GetPanelPossition().x);
                
                if (distance < nearestPossition) {
                    nearestPossition = distance;
                    selectedItemIndex = i;
                }
            }

            return selectedItemIndex;
        }
    }

}

