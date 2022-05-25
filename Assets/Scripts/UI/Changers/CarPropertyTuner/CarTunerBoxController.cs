namespace UI.Changers.CarPropertyTuner {
    
    public class CarTunerBoxController {
        
        private readonly CarTunerBoxView _tunerBoxView;

        private int _currentItemIndex;
        
        public CarTunerBoxController(CarTunerBoxView tunerBoxView) {
            _tunerBoxView = tunerBoxView;
        }

        public void Init() {
            foreach (var item in _tunerBoxView.BoxItems) {
                item.SetItemActivityStatus(false);
            }
            _tunerBoxView.SetPrice(1000);
            _tunerBoxView.OnUpgradeDownCick += OnUpgradeDown;
            _tunerBoxView.OnUpgradeUpCick += OnUpgradeUp;
        }

        public void Dispose() {
            _tunerBoxView.OnUpgradeDownCick -= OnUpgradeDown;
            _tunerBoxView.OnUpgradeUpCick -= OnUpgradeUp;
        }
        
        private void OnUpgradeUp() {
            _tunerBoxView.GetBoxItemByIndex(_currentItemIndex).SetItemActivityStatus(true);
            _tunerBoxView.SetItemsCountText(_currentItemIndex + 1);
            if (_currentItemIndex == _tunerBoxView.BoxItemsCount - 1) return;
            _currentItemIndex++;
        }

        private void OnUpgradeDown() {
            _tunerBoxView.GetBoxItemByIndex(_currentItemIndex).SetItemActivityStatus(false);
            _tunerBoxView.SetItemsCountText(_currentItemIndex + 1);
            if (_currentItemIndex == 0) return;
            _currentItemIndex--;
        }
        
    }
    
}