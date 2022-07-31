using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Changers.Scroller  {
    
    [RequireComponent(typeof(ScrollRect))]
    public class CustomScroller : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

        [SerializeField, Range(10, 30)] private int _scaleSpeed;
        [SerializeField, Range(1f, 2f)] private float _scaleOffset;
        [SerializeField, Range(0, 1)] private float _unfocusedPanelsSize;
        [SerializeField] private float _panelsOffset;
        [SerializeField] private ScrollerPanel _scrollerPanelPrefab;
        
        private ScrollRect _scrollRect;
        private RectTransform _contentContainer;
        private List<ScrollerPanel> _scrollerPanels;
        private int _selectedItemIndex;
        private float _lastContentPositionX;
        private int _activeItemIndex;
        private bool _contentWasChanged;
        
        private Vector2 _contentPosition;
        private Vector2 _contentScale;
        
        public event Action OnContentChanged;
        
        public bool IsScrolling { get; private set; }
        public ScrollerPanel ActivePanel => _scrollerPanels[_activeItemIndex];
        
        private void Awake() {
            _scrollRect = this.GetComponent<ScrollRect>();
            _contentContainer = _scrollRect.content;
            _scrollerPanels = new List<ScrollerPanel>();
        }

        private void Start() {
            _scrollRect.onValueChanged.AddListener(OnValueChanged);
            StartCoroutine(PanelToCenterRoutine());
            StartCoroutine(UpdateScalesRoutine());
        }

        private void OnDestroy() {
            _scrollRect.onValueChanged.RemoveListener(OnValueChanged);
            foreach (var panel in _scrollerPanels) {
                panel.OnPanelClick -= OnPanelClick;
            }
            StopAllCoroutines();
        }
        
        public void AddPanels(int count) {
            for (var i = 0; i < count; i++) {
                ScrollerPanel panel = Instantiate(_scrollerPanelPrefab, _contentContainer.transform);
                var newPoss = new Vector2(
                    (panel.GetWidth() * i) + _panelsOffset,
                    -_contentContainer.anchoredPosition.y);
                
                panel.SetPanelPosition(newPoss);
                _scrollerPanels.Add(panel);
                panel.OnPanelClick += OnPanelClick;
            }
            
            SetFocusAtPanel(0);
        }

        public ScrollerPanel GetPanelAt(int index) {
            if (index < 0 || index > _scrollerPanels.Count - 1) {
                throw new NullReferenceException($"There's no panel at {index} index");
            }

            return _scrollerPanels[index];
        }

        public void SetFocusAtPanel(int index) {
            if (index < 0 || index > _scrollerPanels.Count - 1) {
                throw new NullReferenceException($"There's no panel at {index} index");
            }
            _contentContainer.anchoredPosition = -_scrollerPanels[index].GetPanelPosition();
            _contentPosition = -_scrollerPanels[index].GetPanelPosition();
            _scrollerPanels[index].SetPanelScale(Vector3.one);
            SetPanelsScale(index);
        }
        
        public void OnBeginDrag(PointerEventData eventData) {
            IsScrolling = true;
            _scrollRect.inertia = true;
        }
        
        public void OnEndDrag(PointerEventData eventData) {
            IsScrolling = false;
            _contentWasChanged = false;
        }
        
        private void OnValueChanged(Vector2 vector2) {
            var containerLocalPosition = _contentContainer.localPosition;
            var deltaContentPositionX = Mathf.Abs(containerLocalPosition.x - _lastContentPositionX);
            _lastContentPositionX = containerLocalPosition.x;
            
            if (deltaContentPositionX < 2f) {
                _scrollRect.inertia = false;
                _selectedItemIndex = CurrentCenterPanelIndex();
                
                if (!_contentWasChanged) {
                    _contentWasChanged = true;
                    OnContentChanged?.Invoke();
                }
            }

            _activeItemIndex = _selectedItemIndex;
        }
        
        private void ForbidMovementFromBorders() {
            if (_contentContainer.anchoredPosition.x <= -_scrollerPanels[_scrollerPanels.Count - 1].GetPanelPosition().x) {
                _scrollRect.StopMovement();
                _contentContainer.anchoredPosition = new Vector2(-_scrollerPanels[_scrollerPanels.Count - 1].GetPanelPosition().x,  _contentContainer.anchoredPosition.y);
            }  
            if (_contentContainer.anchoredPosition.x >= -_scrollerPanels[0].GetPanelPosition().x) {
                _scrollRect.StopMovement();
                _contentContainer.anchoredPosition = new Vector2(-_scrollerPanels[0].GetPanelPosition().x,  _contentContainer.anchoredPosition.y);
            }  
        }
        
        private void OnPanelClick(ScrollerPanel scrollerPanel) {
            if (IsScrolling) return;
            _contentWasChanged = false;
            _selectedItemIndex = _scrollerPanels.IndexOf(scrollerPanel);
        }

        private void UpdateScales() {
            foreach (var panel in _scrollerPanels) {
                var distance = Mathf.Abs(_contentContainer.anchoredPosition.x + panel.GetPanelPosition().x);
                var scale = Mathf.Clamp(1f / (distance / _panelsOffset) * _scaleOffset, _unfocusedPanelsSize, 1f);
                
                _contentScale.x = Mathf.SmoothStep(panel.GetPanelScale().x, scale,_scaleSpeed * Time.deltaTime);
                _contentScale.y = Mathf.SmoothStep(panel.GetPanelScale().y, scale,_scaleSpeed * Time.deltaTime);
                panel.SetPanelScale(_contentScale);
            }
        }

        private IEnumerator UpdateScalesRoutine() {
            while (true) {
                UpdateScales();
                if (!IsScrolling || _scrollRect.inertia) {
                    ForbidMovementFromBorders();
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator PanelToCenterRoutine() {
            while (true) {
                yield return new WaitWhile(() => (IsScrolling || _scrollRect.inertia));
                UpdatePanelToCenter();
                yield return new WaitForEndOfFrame();
            }
        }

        private void UpdatePanelToCenter() {
            _contentPosition.x = Mathf.SmoothStep( _contentContainer.anchoredPosition.x, -_scrollerPanels[_selectedItemIndex].GetPanelPosition().x, 25f * Time.deltaTime);
            _contentContainer.anchoredPosition = _contentPosition;
        }

        private void SetPanelsScale(int omitPanelIndex) {
            for (int i = 0; i < _scrollerPanels.Count; i++) {
                if (i == omitPanelIndex) continue;
                _scrollerPanels[i].SetPanelScale(new Vector2(_unfocusedPanelsSize, _unfocusedPanelsSize));
            }
        }
        
        private int CurrentCenterPanelIndex() {
            var nearestPosition = float.MaxValue;
            var selectedItemIndex = -1;
            
            for (var i = 0; i < _scrollerPanels.Count; i++) {
                var distance = Mathf.Abs(_contentContainer.anchoredPosition.x + _scrollerPanels[i].GetPanelPosition().x);

                if (!(distance < nearestPosition)) continue;
                nearestPosition = distance;
                selectedItemIndex = i;
            }

            return selectedItemIndex;
        }
    }

}

