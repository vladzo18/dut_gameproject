using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.LevelChanger {
    
    public class MapsChanger {

        private readonly ChangerItemView _view;
        private readonly MapsModel _model;
        private readonly SnapScroller _levelsScroller;
        private readonly BuyMessageBox _buyMessageBox;
        private readonly CurrencyBox _currencyBox;
        
        private List<ChangerItemView> _changerItemViews;
        private Dictionary<ScrollerPanel, int> _changerDataIndexes;
        private int _messageBoxMapIndex;

        public event Action<int> OnMapChanged;
        public event Action OnMapBuy;
        
        public MapsChanger(ChangerItemView view, MapsModel model, SnapScroller scroller, BuyMessageBox buyMessageBox, CurrencyBox currencyBox) {
            _changerDataIndexes = new Dictionary<ScrollerPanel, int>();
            _changerItemViews = new List<ChangerItemView>();
            _view = view;
            _model = model;
            _levelsScroller = scroller;
            _buyMessageBox = buyMessageBox;
            _currencyBox = currencyBox;
        }

        public void Init(int focusPanelIndex) {
            _levelsScroller.AddPanels(_model.ItemsCount);

            for (int i = 0; i < _model.ItemsCount; i++) {
                ChangerItemView view = GameObject.Instantiate(_view, _levelsScroller.GetPanelAt(i).Rect);
                MapStorageDescriptor mapDescriptor = _model.GetDescriptorAt(i);
                view.SetBodyImage(mapDescriptor.MapImage);
                view.SetHeadText(mapDescriptor.MapName);
                view.SetItemPrice(mapDescriptor.MapCost.ToString());
                view.SetLockedBoxActivity(!_model.GetAvaliabilityStatusAt(i));
                _levelsScroller.GetPanelAt(i).OnPanelClick += OnMapClick;
                
                _changerDataIndexes.Add(_levelsScroller.GetPanelAt(i), i);
                _changerItemViews.Add(view);
            }

            _levelsScroller.SetFocusAtPanel(focusPanelIndex);
            
            _buyMessageBox.OnClose += OnCloseBuyMessageBoxHandler;
            _model.OnModelChanged += UpdateView;
            _levelsScroller.OnContentChanged += SelectMap;
        }

        private void OnCloseBuyMessageBoxHandler() {
            _buyMessageBox.OnBuyButtonClick -= OnBuyButtonMap;
        }

        public void Dispose() {
            for (int i = 0; i < _model.ItemsCount; i++) {
                _levelsScroller.GetPanelAt(i).OnPanelClick -= OnMapClick;
            }
            _buyMessageBox.OnClose -= OnCloseBuyMessageBoxHandler;
            _model.OnModelChanged -= UpdateView;
            _levelsScroller.OnContentChanged -= SelectMap;
        }
        
        private void OnMapClick(ScrollerPanel panel) {
            if (_levelsScroller.IsScrolling || _levelsScroller.ActivePanel != panel) return;
            if (_model.GetAvaliabilityStatusAt(_changerDataIndexes[panel])) return;
                
            MapStorageDescriptor descr = _model.GetDescriptorAt(_changerDataIndexes[panel]);
            _messageBoxMapIndex = _changerDataIndexes[panel];
            
            _buyMessageBox.SetImage(descr.MapImage);
            _buyMessageBox.SetPrice(descr.MapCost.ToString());
            _buyMessageBox.SetTitle(descr.MapName);
            _buyMessageBox.SetContent(descr.Description);
                
            _buyMessageBox.ShowMessageBox();
            _buyMessageBox.OnBuyButtonClick += OnBuyButtonMap;
        }

        private void OnBuyButtonMap(int price) {
            if (_currencyBox.TryTakeCoins(price)) {
               _model.SetAvaliabilityStatusAt(_messageBoxMapIndex, true);
               _buyMessageBox.HideMessageBox();
               OnMapChanged.Invoke(_changerDataIndexes[_levelsScroller.GetPanelAt(_messageBoxMapIndex)]);
               OnMapBuy?.Invoke();
               _buyMessageBox.OnBuyButtonClick -= OnBuyButtonMap;
            }
        }

        private void UpdateView() {
            for (int i = 0; i < _model.ItemsCount; i++) {
                _changerItemViews[i].SetLockedBoxActivity(!_model.GetAvaliabilityStatusAt(i));
            }
        }
        
        private void SelectMap() {
            if (!_model.GetAvaliabilityStatusAt(_changerDataIndexes[_levelsScroller.ActivePanel])) return;
            OnMapChanged.Invoke(_changerDataIndexes[_levelsScroller.ActivePanel]);
        }
        
    }
    
}