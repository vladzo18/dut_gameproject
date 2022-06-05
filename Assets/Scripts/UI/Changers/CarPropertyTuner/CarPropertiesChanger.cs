using System;
using System.Collections.Generic;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarPropertiesChanger {

        private readonly IEnumerable<CarTunerBoxView> _carTunerBoxViews;
        private readonly MessageBox _messageBox;
        private readonly CurrencyBox _currencyBox;
        
        private CarPropertySettings _propertySettings;
        private List<CarTunerBoxController> _tunerBoxControllers;
        private CarTunerBoxController _buyOperableController;

        public event Action OnPointsChanged;
        
        public CarPropertiesChanger(IEnumerable<CarTunerBoxView> carTunerBoxViews, CarPropertySettings settings, MessageBox messageBox, CurrencyBox currencyBox) {
            _tunerBoxControllers = new List<CarTunerBoxController>();
            _carTunerBoxViews = carTunerBoxViews;
            _propertySettings = settings;
            _messageBox = messageBox;
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
                _messageBox.OnClose += OnClloceHandler;
            }
        }

        private void OnBuyUpgradeHandler(CarTunerBoxController controller) {
            _buyOperableController = controller;
            _messageBox.ShowMessageBox();
            _messageBox.Clear();
            _messageBox.SetTitle("Upgrade Car Property");
            _messageBox.SetPrice(controller.UpgradePrice.ToString());
            _messageBox.OnTryBuyClick += OnTryBuyClicHandler;
        }

        private void OnClloceHandler() {
            _messageBox.OnTryBuyClick -= OnTryBuyClicHandler;
        }

        private void OnTryBuyClicHandler(int price) {
            if (_currencyBox.TryTakeCoins(price)) {
                _buyOperableController.IncreaseUpgradeAbility();
                _messageBox.HideMessageBox();
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
            _messageBox.OnClose -= OnClloceHandler;
        }

        private void PointsChangedClickHandler() => OnPointsChanged?.Invoke();

    }
    
}