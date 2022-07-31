using System;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarTunerBoxController {
        
        private readonly CarTunerBoxView _tunerBoxView;
        
        private CarPropertySetting _propertySetting;
        private int _currentItemIndex;
        private int _currentItemIndexBorder;

        private const int START_UPGRADE_COAST = 500;

        public event Action<CarTunerBoxController> OnBuyUpgrade;

        public int UpgradePrice => START_UPGRADE_COAST * (_currentItemIndexBorder + 1);
        
        public CarTunerBoxController(CarTunerBoxView tunerBoxView, CarPropertySetting setting) {
            _tunerBoxView = tunerBoxView;
            _propertySetting = setting;
        }

        public void ChangeCarPropertySetting(CarPropertySetting setting) {
            _propertySetting = setting;
            UpdateView();
        }

        public void Init() {
            UpdateView();
            _tunerBoxView.OnUpgradeDownClick += OnUpgradeDown;
            _tunerBoxView.OnUpgradeUpClick += OnUpgradeUp;
            _tunerBoxView.OnUpgradeBuy += OnUpgradeBuyHandler;
        }

        public void Dispose() {
            _tunerBoxView.OnUpgradeDownClick -= OnUpgradeDown;
            _tunerBoxView.OnUpgradeUpClick -= OnUpgradeUp;
            _tunerBoxView.OnUpgradeBuy -= OnUpgradeBuyHandler;
        }

        public void IncreaseUpgradeAbility() {
            if (_currentItemIndexBorder < _tunerBoxView.BoxItemsCount) {
                _currentItemIndexBorder++;
                _propertySetting.ValueBorder = _currentItemIndexBorder;
                _tunerBoxView.SetPrice(UpgradePrice);
                OnUpgradeUp();
            }
            if (_currentItemIndexBorder == _tunerBoxView.BoxItemsCount) {
                _tunerBoxView.HidePriceBox();
            }
        }

        private void UpdateView() {
            for (int i = 0; i < _tunerBoxView.BoxItemsCount; i++) {
                _tunerBoxView.GetBoxItemByIndex(i).SetItemActivityStatus(i <= _propertySetting.Value - 1);
            }
            _currentItemIndex = _propertySetting.Value;
            _currentItemIndexBorder = _propertySetting.ValueBorder;
            _tunerBoxView.SetItemsCountText(_currentItemIndex);
            _tunerBoxView.SetPrice(UpgradePrice);
        }
        
        private void OnUpgradeBuyHandler() {
            if (_currentItemIndexBorder < _tunerBoxView.BoxItemsCount) {
                OnBuyUpgrade?.Invoke(this);
            }
        }

        private void OnUpgradeUp() {
            if (_currentItemIndex == _currentItemIndexBorder) return;
            _currentItemIndex++;
            _propertySetting.Value = _currentItemIndex;
            _tunerBoxView.SetItemsCountText(_currentItemIndex);
            _tunerBoxView.GetBoxItemByIndex(_currentItemIndex - 1).SetItemActivityStatus(true);
        }

        private void OnUpgradeDown() {
            if (_currentItemIndex == 0) return;
            _currentItemIndex--;
            _propertySetting.Value = _currentItemIndex;
            _tunerBoxView.SetItemsCountText(_currentItemIndex);
            _tunerBoxView.GetBoxItemByIndex(_currentItemIndex).SetItemActivityStatus(false);
        }
        
    }
    
}