using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarPropertiesChanger {

        private readonly IEnumerable<CarTunerBoxView> _carTunerBoxViews;
        private readonly BuyMessageBox _buyMessageBox;
        private readonly CurrencyBox _currencyBox;
        
        private CarPropertySettings _propertySettings;
        private List<CarTunerBoxController> _tunerBoxControllers;
        private CarTunerBoxController _buyOperableController;

        public event Action OnPointsChanged;
        
        public CarPropertiesChanger(IEnumerable<CarTunerBoxView> carTunerBoxViews, CarPropertySettings settings, BuyMessageBox buyMessageBox, CurrencyBox currencyBox) {
            _tunerBoxControllers = new List<CarTunerBoxController>();
            _carTunerBoxViews = carTunerBoxViews;
            _propertySettings = settings;
            _buyMessageBox = buyMessageBox;
            _currencyBox = currencyBox;
        }

        public void ChangeCarPropertySettings(CarPropertySettings settings) {
            _propertySettings = settings;
            bool isLoadedSetings = _propertySettings.TryLoadState();
            
            int counter = 0;
            foreach (var view in _carTunerBoxViews) {
                if (isLoadedSetings) {
                    CarPropertySetting setting = _propertySettings.GetSettingByType(view.Type);
                    _tunerBoxControllers[counter].ChangeCarPropertySetting(setting);
                } else {
                    CarPropertySetting setting = _propertySettings.AddSetting(view.Type);
                    _tunerBoxControllers[counter].ChangeCarPropertySetting(setting);
                }
              
                counter++;
            }
        }

        public void Init(int saveIndex) {
            bool isLoadedSetings = _propertySettings.TryLoadState();
                
            foreach (var boxView in _carTunerBoxViews) {
                CarPropertySetting setting = new CarPropertySetting();
                if (!isLoadedSetings) {
                    setting = _propertySettings.AddSetting(boxView.Type);
                } else {
                    setting = _propertySettings.GetSettingByType(boxView.Type);
                }
              
                CarTunerBoxController controller = new CarTunerBoxController(boxView, setting);
                controller.Init();
                controller.OnBuyUpgrade += OnBuyUpgradeHandler;
                _tunerBoxControllers.Add(controller);
                boxView.OnUpgradeDownCick += PointsChangedClickHandler;
                boxView.OnUpgradeUpCick += PointsChangedClickHandler;
                _buyMessageBox.OnClose += OnClloceHandler;
            }
        }

        private void OnBuyUpgradeHandler(CarTunerBoxController controller) {
            _buyOperableController = controller;
            _buyMessageBox.ShowMessageBox();
            _buyMessageBox.Clear();
            _buyMessageBox.SetTitle("Upgrade Car Property");
            _buyMessageBox.SetPrice(controller.UpgradePrice.ToString());
            _buyMessageBox.OnBuyButtonClick += OnBuyButtonClicHandler;
        }

        private void OnClloceHandler() {
            _buyMessageBox.OnBuyButtonClick -= OnBuyButtonClicHandler;
        }

        private void OnBuyButtonClicHandler(int price) {
            if (_currencyBox.TryTakeCoins(price)) {
                _buyOperableController.IncreaseUpgradeAbility();
                _buyMessageBox.HideMessageBox();
            }
        }

        public void Dispose() {
            foreach (var boxController in _tunerBoxControllers) {
               boxController.Dispose();
               boxController.OnBuyUpgrade -= OnBuyUpgradeHandler;
            }
            foreach (var boxView in _carTunerBoxViews) {
                boxView.OnUpgradeDownCick -= PointsChangedClickHandler;
                boxView.OnUpgradeUpCick -= PointsChangedClickHandler;
            }
            _buyMessageBox.OnClose -= OnClloceHandler;
        }

        private void PointsChangedClickHandler() => OnPointsChanged?.Invoke();

    }
    
}