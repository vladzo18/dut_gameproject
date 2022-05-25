using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarTunerBoxView : MonoBehaviour {

        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _tunePropertyImage;
        [SerializeField] private List<BoxItem> _boxItems;
        [SerializeField] private TMP_Text _itemCountText;
        [SerializeField] private UpgradeButton _upgradeUpButton;
        [SerializeField] private UpgradeButton _upgradeDownButton;

        public event Action OnUpgradeUpCick;
        public event Action OnUpgradeDownCick;

        private void Start() {
            _upgradeUpButton.OnUpgradeButtonClick += UpgradeHandler;
            _upgradeDownButton.OnUpgradeButtonClick += DisupgradeHandler;
        }
        
        private void OnDestroy() {
            _upgradeUpButton.OnUpgradeButtonClick -= UpgradeHandler;
            _upgradeDownButton.OnUpgradeButtonClick -= DisupgradeHandler;
        }

        public IEnumerable<BoxItem> BoxItems => _boxItems;
        public int BoxItemsCount => _boxItems.Count;
        public void SetItemsCountText(int value) => _itemCountText.text = $"{value}/{_boxItems.Count}";
        public void SetPrice(int value) => _priceText.text = value.ToString();

        public BoxItem GetBoxItemByIndex(int index) {
            if (index < 0 || index >= BoxItemsCount) {
                throw new NullReferenceException();
            }
            return _boxItems[BoxItemsCount - (index + 1)];
        }

        private void UpgradeHandler() => OnUpgradeUpCick?.Invoke();
        private void DisupgradeHandler() => OnUpgradeDownCick?.Invoke();

    }
    
}