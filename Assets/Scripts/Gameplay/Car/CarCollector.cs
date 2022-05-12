using System;
using Items;
using UnityEngine;

namespace Scripts {
    
    public class CarCollector : MonoBehaviour {

        public event Action<float> OnFuelCollect;
        public event Action<float> OnCoinCollect;
        public event Action<float> OnDiamontCollect;
        
        private void OnTriggerEnter2D(Collider2D collider) {
            var item = collider.GetComponent<Item.Item>();
            
            if (item != null) {
                switch (item.ItemType) {
                   case ItemType.Fuel:
                       OnFuelCollect?.Invoke(item.ItemAmount);
                       break;
                   case ItemType.Coin:
                       OnCoinCollect?.Invoke(item.ItemAmount);
                       break;
                   case ItemType.Diamant:
                       OnDiamontCollect?.Invoke(item.ItemAmount);
                       break;
                }
                item.Take();
            }
        }

    }

}
