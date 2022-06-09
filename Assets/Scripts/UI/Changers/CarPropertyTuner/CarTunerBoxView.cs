using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Changers.CarPropertyTuner {
    
    public class CarTunerBoxView : MonoBehaviour, IPointerDownHandler{

        [SerializeField] private CarProrertyType _type;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private GameObject _priceBox;
        [SerializeField] private List<BoxItem> _boxItems;
        [SerializeField] private TMP_Text _itemCountText;
        [SerializeField] private UpgradeButton _upgradeUpButton;
        [SerializeField] private UpgradeButton _upgradeDownButton;

        public event Action OnUpgradeUpClick;
        public event Action OnUpgradeDownClick;
        public event Action OnUpgradeBuy;

        private void Start() {
            _upgradeUpButton.OnUpgradeButtonClick += UpgradeHandler;
            _upgradeDownButton.OnUpgradeButtonClick += DisupgradeHandler;
        }
        
        private void OnDestroy() {
            _upgradeUpButton.OnUpgradeButtonClick -= UpgradeHandler;
            _upgradeDownButton.OnUpgradeButtonClick -= DisupgradeHandler;
        }

        public CarProrertyType Type => _type;
        public IEnumerable<BoxItem> BoxItems => _boxItems;
        public int BoxItemsCount => _boxItems.Count;
        public void SetItemsCountText(int value) => _itemCountText.text = $"{value}/{_boxItems.Count}";
        public void SetPrice(int value) => _priceText.text = value.ToString();
        public void HidePriceBox() => _priceBox.gameObject.SetActive(false);

        public BoxItem GetBoxItemByIndex(int index) {
            if (index < 0 || index >= BoxItemsCount) {
                throw new NullReferenceException();
            }
            return _boxItems[BoxItemsCount - (index + 1)];
        }

        private void UpgradeHandler() => OnUpgradeUpClick?.Invoke();
        private void DisupgradeHandler() => OnUpgradeDownClick?.Invoke();

        public void OnPointerDown(PointerEventData eventData) {
            OnUpgradeBuy?.Invoke();
        }
        
    }
    
}