using System;
using System.Collections.Generic;
using UI.Changers.Scroller;
using Object = UnityEngine.Object;

namespace UI.Changers.MapChanger {
    
    public class MapsChanger {

        private readonly ChangerItemView _view;
        private readonly MapsModel _model;
        private readonly CustomScroller _levelsScroller;
        private readonly BuyMessageBox _buyMessageBox;
        private readonly CurrencyBox _currencyBox;
        
        private readonly List<ChangerItemView> _changerItemViews;
        private readonly Dictionary<ScrollerPanel, int> _changerIndexes;
        private readonly Dictionary<MapType, ScrollerPanel> _mapTypes;
        
        private int _messageBoxMapIndex;

        public event Action<int> OnMapChanged;
        public event Action OnMapBuy;
        
        public MapType CurrentMapType => _model.CurrentMapType;
        
        public MapsChanger(ChangerItemView view, MapsModel model, CustomScroller scroller, BuyMessageBox buyMessageBox, CurrencyBox currencyBox) {
            _changerIndexes = new Dictionary<ScrollerPanel, int>();
            _mapTypes = new Dictionary<MapType, ScrollerPanel>();
            _changerItemViews = new List<ChangerItemView>();
            _view = view;
            _model = model;
            _levelsScroller = scroller;
            _buyMessageBox = buyMessageBox;
            _currencyBox = currencyBox;
        }

        public void Init() {
            _model.LoadModel();
            _levelsScroller.AddPanels(_model.ItemsCount);

            for (var i = 0; i < _model.ItemsCount; i++) {
                ChangerItemView view = Object.Instantiate(_view, _levelsScroller.GetPanelAt(i).Rect);
                MapStorageDescriptor mapDescriptor = _model.GetDescriptorAt(i);
                view.SetBodyImage(mapDescriptor.MapImage);
                view.SetHeadText(mapDescriptor.MapName);
                view.SetItemPrice(mapDescriptor.MapCost.ToString());
                view.SetLockedBoxActivity(!_model.GetAvailabilityStatusAt(i));
                _levelsScroller.GetPanelAt(i).OnPanelClick += OnMapClick;
                
                _changerIndexes.Add(_levelsScroller.GetPanelAt(i), i);
                _mapTypes.Add( mapDescriptor.Type, _levelsScroller.GetPanelAt(i));
                _changerItemViews.Add(view);
            }

            _levelsScroller.SetFocusAtPanel(_changerIndexes[_mapTypes[_model.CurrentMapType]]);
            
            _buyMessageBox.OnClose += OnCloseBuyMessageBoxHandler;
            _model.OnModelChanged += UpdateView;
            _levelsScroller.OnContentChanged += SelectMap;
        }
        
        public void Dispose() {
            _model.SaveModel();
            
            for (var i = 0; i < _model.ItemsCount; i++) {
                _levelsScroller.GetPanelAt(i).OnPanelClick -= OnMapClick;
            }
            
            _buyMessageBox.OnClose -= OnCloseBuyMessageBoxHandler;
            _model.OnModelChanged -= UpdateView;
            _levelsScroller.OnContentChanged -= SelectMap;
        }
        
        private void OnMapClick(ScrollerPanel panel) {
            if (_levelsScroller.IsScrolling || _levelsScroller.ActivePanel != panel) return;
            if (_model.GetAvailabilityStatusAt(_changerIndexes[panel])) return;
                
            MapStorageDescriptor descr = _model.GetDescriptorAt(_changerIndexes[panel]);
            _messageBoxMapIndex = _changerIndexes[panel];
            
            _buyMessageBox.SetImage(descr.MapImage);
            _buyMessageBox.SetPrice(descr.MapCost.ToString());
            _buyMessageBox.SetTitle(descr.MapName);
            _buyMessageBox.SetContent(descr.Description);
                
            _buyMessageBox.ShowMessageBox();
            _buyMessageBox.OnBuyButtonClick += OnBuyButtonMap;
        }

        private void OnCloseBuyMessageBoxHandler() => _buyMessageBox.OnBuyButtonClick -= OnBuyButtonMap;
        
        private void OnBuyButtonMap(int price) {
            if (!_currencyBox.TryTakeCoins(price)) return;
            _model.SetAvailabilityStatusAt(_messageBoxMapIndex, true);
            _buyMessageBox.HideMessageBox();
            _model.SetCurrentMapType(_model.GetDescriptorAt(_changerIndexes[_levelsScroller.ActivePanel]).Type);
            OnMapChanged?.Invoke(_changerIndexes[_levelsScroller.GetPanelAt(_messageBoxMapIndex)]);
            OnMapBuy?.Invoke();
            _buyMessageBox.OnBuyButtonClick -= OnBuyButtonMap;
        }

        private void UpdateView() {
            for (var i = 0; i < _model.ItemsCount; i++) {
                _changerItemViews[i].SetLockedBoxActivity(!_model.GetAvailabilityStatusAt(i));
            }
        }
        
        private void SelectMap() {
            if (!_model.GetAvailabilityStatusAt(_changerIndexes[_levelsScroller.ActivePanel])) return;
            _model.SetCurrentMapType(_model.GetDescriptorAt(_changerIndexes[_levelsScroller.ActivePanel]).Type);
            OnMapChanged?.Invoke(_changerIndexes[_levelsScroller.ActivePanel]);
        }
        
    }
    
}