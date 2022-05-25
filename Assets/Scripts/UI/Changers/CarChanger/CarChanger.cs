using System;
using System.Collections.Generic;
using UI.Changers.LevelChanger;
using UnityEngine;

namespace UI.Changers.CarChanger {
    
    public class CarChanger {

        private readonly ChangerItemView _view;
        private readonly CarsModel _model;
        private readonly SnapScroller _carsScroller;
        private readonly MessageBox _messageBox;
        private readonly CurrencyBox _currencyBox;
        
        private List<ChangerItemView> _changerItemViews;
        private Dictionary<ScrollerPanel, int> _changerDataIndexes;
        private int _messageBoxCarIndex;
        
        public event Action<int> OnCarChanged;

        public CarChanger(ChangerItemView view, CarsModel model, SnapScroller scroller, MessageBox messageBox, CurrencyBox currencyBox) {
            _changerDataIndexes = new Dictionary<ScrollerPanel, int>();
            _changerItemViews = new List<ChangerItemView>();
            _view = view;
            _model = model;
            _carsScroller = scroller;
            _messageBox = messageBox;
            _currencyBox = currencyBox;
        }

        public void Init(int focusPanelIndex) {
            _carsScroller.AddPanels(_model.ItemsCount);

            for (int i = 0; i < _model.ItemsCount; i++) {
                ChangerItemView view =  GameObject.Instantiate(_view, _carsScroller.GetPanelAt(i).Rect);
                view.SetBodyImage(_model.GetDescriptorAt(i).CarImage);
                view.SetHeadText(_model.GetDescriptorAt(i).CarName);
                view.SetItemPrice(_model.GetDescriptorAt(i).CarCost.ToString());
                view.SetLockedBoxActivity(!_model.GetAvaliabilityStatusAt(i));
                _carsScroller.GetPanelAt(i).OnPanelClick += OnMapClick;
                
                _changerDataIndexes.Add(_carsScroller.GetPanelAt(i), i);
                _changerItemViews.Add(view);
            }
            
            _carsScroller.SetFocusAtPanel(focusPanelIndex);
            
            _messageBox.OnTryBuyClick += OnBuyMap;
            _model.OnModelChanged += UpdateView;
            _carsScroller.OnContentChanged += SelectMap;
        }

        public void Dispose() {
            for (int i = 0; i < _model.ItemsCount; i++) {
                _carsScroller.GetPanelAt(i).OnPanelClick -= OnMapClick;
            }
            _messageBox.OnTryBuyClick -= OnBuyMap;
            _model.OnModelChanged -= UpdateView;
            _carsScroller.OnContentChanged -= SelectMap;
        }
        
        
        private void OnMapClick(ScrollerPanel panel) {
            if (_carsScroller.IsScrolling || _carsScroller.ActivePanel != panel) return;
            if (_model.GetAvaliabilityStatusAt(_changerDataIndexes[panel])) return;
            
            CarStorageDescriptor descr = _model.GetDescriptorAt(_changerDataIndexes[panel]);
            _messageBoxCarIndex = _changerDataIndexes[panel];
           
            _messageBox.SetImage(descr.CarImage);
            _messageBox.SetPrice(descr.CarCost.ToString());
            _messageBox.SetTitle(descr.CarName);
            _messageBox.SetContent(descr.Description);
                
            _messageBox.ShowMessageBox();
        }

        private void OnBuyMap(int price) {
            if (_currencyBox.TryTakeCoins(price)) {
                _model.SetAvaliabilityStatusAt(_messageBoxCarIndex, true);
                _messageBox.HideMessageBox();
                OnCarChanged.Invoke(_changerDataIndexes[_carsScroller.GetPanelAt(_messageBoxCarIndex)]);
            }
        }

        private void UpdateView() {
            for (int i = 0; i < _model.ItemsCount; i++) {
                _changerItemViews[i].SetLockedBoxActivity(!_model.GetAvaliabilityStatusAt(i));
            }
        }
        
        private void SelectMap() {
            if (!_model.GetAvaliabilityStatusAt(_changerDataIndexes[_carsScroller.ActivePanel])) return;
            OnCarChanged.Invoke(_changerDataIndexes[_carsScroller.ActivePanel]);
        }
        
    }
    
}