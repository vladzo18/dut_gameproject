using System;
using System.Collections.Generic;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarPropertiesChanger {

        private readonly IEnumerable<CarTunerBoxView> _carTunerBoxViews;
        private readonly BuyMessageBox _buyMessageBox;
        private readonly CurrencyBox _currencyBox;
        
        private readonly List<CarTunerBoxController> _tunerBoxControllers;
        
        private CarPropertySettings _propertySettings;
        private CarTunerBoxController _buyOperableController;

        public event Action OnPointsChanged;
        public event Action OnUpgradeBought;
        
        public CarPropertiesChanger(IEnumerable<CarTunerBoxView> carTunerBoxViews, CarPropertySettings settings, BuyMessageBox buyMessageBox, CurrencyBox currencyBox) {
            _tunerBoxControllers = new List<CarTunerBoxController>();
            _carTunerBoxViews = carTunerBoxViews;
            _propertySettings = settings;
            _buyMessageBox = buyMessageBox;
            _currencyBox = currencyBox;
        }

        public void ChangeCarPropertySettings(CarPropertySettings settings) {
            _propertySettings = settings;
            bool isLoadedSettings = _propertySettings.TryLoadState();
            
            int counter = 0;
            foreach (var view in _carTunerBoxViews) {
                if (isLoadedSettings) {
                    CarPropertySetting setting = _propertySettings.GetSettingByType(view.Type);
                    _tunerBoxControllers[counter].ChangeCarPropertySetting(setting);
                } else {
                    CarPropertySetting setting = _propertySettings.AddSetting(view.Type);
                    _tunerBoxControllers[counter].ChangeCarPropertySetting(setting);
                }
              
                counter++;
            }
        }

        public void Init() {
            bool isLoadedSettings = _propertySettings.TryLoadState();
                
            foreach (var boxView in _carTunerBoxViews) {
                CarPropertySetting setting;
                if (!isLoadedSettings) {
                    setting = _propertySettings.AddSetting(boxView.Type);
                } else {
                    setting = _propertySettings.GetSettingByType(boxView.Type);
                }
              
                CarTunerBoxController controller = new CarTunerBoxController(boxView, setting);
                controller.Init();
                _tunerBoxControllers.Add(controller);
                controller.OnBuyUpgrade += OnBuyUpgradeHandler;
                boxView.OnUpgradeDownClick += PointsChangedClickHandler;
                boxView.OnUpgradeUpClick += PointsChangedClickHandler;
                _buyMessageBox.OnClose += OnCloseMSBoxHandler;
            }
        }
        
        public void Dispose() {
            _propertySettings.SaveState();
            
            foreach (var boxController in _tunerBoxControllers) {
               boxController.Dispose();
               boxController.OnBuyUpgrade -= OnBuyUpgradeHandler;
            }
            foreach (var boxView in _carTunerBoxViews) {
                boxView.OnUpgradeDownClick -= PointsChangedClickHandler;
                boxView.OnUpgradeUpClick -= PointsChangedClickHandler;
            }
            _buyMessageBox.OnClose -= OnCloseMSBoxHandler;
        }
        
        private void OnBuyButtonClickHandler(int price) {
            if (!_currencyBox.TryTakeCoins(price)) return;
            _buyOperableController.IncreaseUpgradeAbility();
            _buyMessageBox.HideMessageBox();
            OnUpgradeBought?.Invoke();
        }
        
        private void OnBuyUpgradeHandler(CarTunerBoxController controller) {
            _buyOperableController = controller;
            _buyMessageBox.ShowMessageBox();
            _buyMessageBox.Clear();
            _buyMessageBox.SetTitle("Upgrade Car Property");
            _buyMessageBox.SetPrice(controller.UpgradePrice.ToString());
            _buyMessageBox.OnBuyButtonClick += OnBuyButtonClickHandler;
        }
        
        private void PointsChangedClickHandler() => OnPointsChanged?.Invoke();
        
        private void OnCloseMSBoxHandler() =>_buyMessageBox.OnBuyButtonClick -= OnBuyButtonClickHandler;

    }
    
}