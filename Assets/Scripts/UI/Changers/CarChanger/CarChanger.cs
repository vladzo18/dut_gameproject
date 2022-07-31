using System;
using System.Collections.Generic;
using UI.Changers.Scroller;
using Object = UnityEngine.Object;

namespace UI.Changers.CarChanger {
    
    public class CarChanger {

        private readonly ChangerItemView _view;
        private readonly CarsModel _model;
        private readonly CustomScroller _carsScroller;
        private readonly BuyMessageBox _buyMessageBox;
        private readonly CurrencyBox _currencyBox;
        
        private readonly List<ChangerItemView> _changerItemViews;
        private readonly Dictionary<ScrollerPanel, int> _changerIndexes;
        private readonly Dictionary<CarType, ScrollerPanel> _carTypes;
        
        private int _messageBoxCarIndex;
        
        public event Action<int> OnCarChanged;
        public event Action OnCarBuy;

        public CarType CurrentCarType => _model.CurrentCarType;

        public CarChanger(ChangerItemView view, CarsModel model, CustomScroller scroller, BuyMessageBox buyMessageBox, CurrencyBox currencyBox) {
            _changerIndexes = new Dictionary<ScrollerPanel, int>();
            _carTypes = new Dictionary<CarType, ScrollerPanel>();
            _changerItemViews = new List<ChangerItemView>();
            _view = view;
            _model = model;
            _carsScroller = scroller;
            _buyMessageBox = buyMessageBox;
            _currencyBox = currencyBox;
        }

        public void Init() {
            _model.LoadModel();
            
            _carsScroller.AddPanels(_model.ItemsCount);

            for (int i = 0; i < _model.ItemsCount; i++) {
                ChangerItemView view =  Object.Instantiate(_view, _carsScroller.GetPanelAt(i).Rect);
                view.SetBodyImage(_model.GetDescriptorAt(i).CarImage);
                view.SetHeadText(_model.GetDescriptorAt(i).CarName);
                view.SetItemPrice(_model.GetDescriptorAt(i).CarCost.ToString());
                view.SetLockedBoxActivity(!_model.GetAvailabilityStatusAt(i));
                _carsScroller.GetPanelAt(i).OnPanelClick += OnCarClick;
                
                _changerIndexes.Add(_carsScroller.GetPanelAt(i), i);
                _carTypes.Add(_model.GetDescriptorAt(i).CarType, _carsScroller.GetPanelAt(i));
                _changerItemViews.Add(view);
            }
            
            _carsScroller.SetFocusAtPanel(_changerIndexes[_carTypes[_model.CurrentCarType]]);
            
            _buyMessageBox.OnClose += OnCloseBuyMessageBoxHandler;
            _model.OnModelChanged += UpdateView;
            _carsScroller.OnContentChanged += SelectCar;
        }

        private void OnCloseBuyMessageBoxHandler() {
            _buyMessageBox.OnBuyButtonClick -= OnBuyButtonCar;
        }
        
        public void Dispose() {
            _model.SaveModel();
            for (int i = 0; i < _model.ItemsCount; i++) {
                _carsScroller.GetPanelAt(i).OnPanelClick -= OnCarClick;
            }
            _buyMessageBox.OnClose -= OnCloseBuyMessageBoxHandler;
            _model.OnModelChanged -= UpdateView;
            _carsScroller.OnContentChanged -= SelectCar;
        }
        
        
        private void OnCarClick(ScrollerPanel panel) {
            if (_carsScroller.IsScrolling || _carsScroller.ActivePanel != panel) return;
            if (_model.GetAvailabilityStatusAt(_changerIndexes[panel])) return;
            
            CarStorageDescriptor descr = _model.GetDescriptorAt(_changerIndexes[panel]);
            _messageBoxCarIndex = _changerIndexes[panel];
           
            _buyMessageBox.SetImage(descr.CarImage);
            _buyMessageBox.SetPrice(descr.CarCost.ToString());
            _buyMessageBox.SetTitle(descr.CarName);
            _buyMessageBox.SetContent(descr.Description);
                
            _buyMessageBox.ShowMessageBox();
            _buyMessageBox.OnBuyButtonClick += OnBuyButtonCar;
        }

        private void OnBuyButtonCar(int price) {
            if (!_currencyBox.TryTakeCoins(price)) return;
            _model.SetAvailabilityStatusAt(_messageBoxCarIndex, true);
            _buyMessageBox.HideMessageBox();
            _model.SetCurrentCarType(_model.GetDescriptorAt(_changerIndexes[_carsScroller.ActivePanel]).CarType);
            OnCarChanged?.Invoke(_changerIndexes[_carsScroller.GetPanelAt(_messageBoxCarIndex)]);
            OnCarBuy?.Invoke();
            _buyMessageBox.OnBuyButtonClick -= OnBuyButtonCar;
        }

        private void UpdateView() {
            for (int i = 0; i < _model.ItemsCount; i++) {
                _changerItemViews[i].SetLockedBoxActivity(!_model.GetAvailabilityStatusAt(i));
            }
        }
        
        private void SelectCar() {
            if (!_model.GetAvailabilityStatusAt(_changerIndexes[_carsScroller.ActivePanel])) return;
            _model.SetCurrentCarType(_model.GetDescriptorAt(_changerIndexes[_carsScroller.ActivePanel]).CarType);
            OnCarChanged?.Invoke(_changerIndexes[_carsScroller.ActivePanel]);
        }
        
    }
    
}